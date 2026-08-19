USE [live_db_pt]
GO

/*
    Deletes one p_daily_sale row and rebuilds the affected customer/landlord
    summaries from the remaining source rows.

    Important: this procedure does NOT delete tbl_customer/tbl_client master rows
    and does NOT delete cash/payment history. Cash/payment rows are checkpoints.
*/
CREATE OR ALTER PROCEDURE dbo.sp_DeleteCustomerSale
    @daily_sale_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @p_daily_id INT,
        @bill_key NVARCHAR(50),
        @cust_bill_key NVARCHAR(50),
        @cust_id INT,
        @client_id INT,
        @deleted_quantity INT,
        @sale_datetime DATETIME;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT
            @p_daily_id = s.p_daily_id,
            @bill_key = s.bill_key,
            @cust_bill_key = s.cust_bill_key,
            @cust_id = s.cust_id,
            @client_id = s.client_id,
            @deleted_quantity = ISNULL(s._quantity, 0),
            @sale_datetime = s.[datetime]
        FROM dbo.p_daily_sale s WITH (UPDLOCK, HOLDLOCK)
        WHERE s.id = @daily_sale_id;

        IF @p_daily_id IS NULL
            THROW 50001, 'Customer sale was not found.', 1;

        DELETE FROM dbo.p_daily_sale
        WHERE id = @daily_sale_id;

        /* -------------------------------------------------------------
           CUSTOMER BILL
           p_customer_sale is the bill summary. Rebuild it from the
           remaining p_daily_sale rows for the same cust_bill_key.
           ------------------------------------------------------------- */
        IF NULLIF(@cust_bill_key, '') IS NOT NULL
        BEGIN
            IF EXISTS
            (
                SELECT 1
                FROM dbo.p_daily_sale
                WHERE cust_bill_key = @cust_bill_key
                  AND cust_id = @cust_id
            )
            BEGIN
                UPDATE cs
                SET
                    cs.quantity = x.quantity,
                    cs.chalan = x.chalan,
                    cs.total_sale = x.total_sale,
                    cs.commission = x.commission,
                    cs.chongi = x.chongi,
                    cs.grand_total = x.grand_total
                FROM dbo.p_customer_sale cs
                CROSS APPLY
                (
                    SELECT
                        ISNULL(SUM(ISNULL(ds._quantity, 0)), 0) AS quantity,
                        COUNT(*) AS chalan,
                        ISNULL(SUM(ISNULL(ds.sale_amount_customer, 0)), 0) AS total_sale,
                        ISNULL(SUM(ISNULL(ds.commission, 0)), 0) AS commission,
                        ISNULL(SUM(ISNULL(ds.chongi, 0)), 0) AS chongi,
                        ISNULL(SUM(ISNULL(ds.grand_total, 0)), 0) AS grand_total
                    FROM dbo.p_daily_sale ds
                    WHERE ds.cust_bill_key = @cust_bill_key
                      AND ds.cust_id = @cust_id
                ) x
                WHERE cs.bill_key = @cust_bill_key
                  AND cs.cust_id = @cust_id;
            END
            ELSE
            BEGIN
                DELETE FROM dbo.p_customer_sale
                WHERE bill_key = @cust_bill_key
                  AND cust_id = @cust_id;
            END
        END

        /* -------------------------------------------------------------
           LANDLORD BILL / STOCK
           Rebuild sales and available quantity from remaining allocations.
           Landlord expenses/advance remain untouched.
           ------------------------------------------------------------- */
        DECLARE
            @sold_qty INT = 0,
            @landlord_sales INT = 0,
            @customer_sales INT = 0,
            @landlord_commission_chongi FLOAT = 0,
            @landlord_expenses FLOAT = 0,
            @landlord_grand_total FLOAT = 0;

        SELECT
            @sold_qty = ISNULL(SUM(ISNULL(ds._quantity, 0)), 0),
            @landlord_sales = ISNULL(SUM(ISNULL(ds.sale_amount_landlord, 0)), 0),
            @customer_sales = ISNULL(SUM(ISNULL(ds.sale_amount_customer, 0)), 0),
            @landlord_commission_chongi = ISNULL(SUM(ISNULL(ds.bipari_grand_total, 0)), 0)
        FROM dbo.p_daily_sale ds
        WHERE ds.p_daily_id = @p_daily_id;

        SELECT
            @landlord_expenses =
                ISNULL(d.total_rent, 0)
              + ISNULL(d.total_labour, 0)
              + ISNULL(d.total_munshiana, 0)
              + ISNULL(d.total_market_fee, 0)
              + ISNULL(d.total_advance, 0),
            @landlord_grand_total =
                @landlord_sales
              - (
                    ISNULL(d.total_rent, 0)
                  + ISNULL(d.total_labour, 0)
                  + ISNULL(d.total_munshiana, 0)
                  + ISNULL(d.total_market_fee, 0)
                  + ISNULL(d.total_advance, 0)
                  + @landlord_commission_chongi
                )
        FROM dbo.p_daily d
        WHERE d.id = @p_daily_id;

        UPDATE dbo.p_daily
        SET
            sale_remaining_product = CASE
                WHEN ISNULL(product_quantity, 0) - @sold_qty < 0 THEN 0
                ELSE ISNULL(product_quantity, 0) - @sold_qty
            END,
            total_sale_amount = @landlord_sales,
            client_sales_total = @customer_sales,
            grand_total = CONVERT(INT, ROUND(@landlord_grand_total, 0))
        WHERE id = @p_daily_id;

        /* -------------------------------------------------------------
           CUSTOMER RUNNING ACCOUNT
           Rebuild cust_augrai chronologically. p_cash_receiving rows are
           authoritative checkpoints when they occur between customer bills.
           ------------------------------------------------------------- */
        IF @cust_id IS NOT NULL
        BEGIN
            DECLARE @customer_balance FLOAT = ISNULL((
                SELECT old_augrai_amount
                FROM dbo.tbl_customer
                WHERE cust_id = @cust_id
            ), 0);

            DECLARE @event_type CHAR(1), @event_id INT,
                    @event_bill FLOAT, @event_remaining FLOAT;

            DECLARE customer_events CURSOR LOCAL FAST_FORWARD FOR
                SELECT event_type, event_id, bill_amount, remaining_amount
                FROM
                (
                    SELECT
                        'B' AS event_type,
                        cs.id AS event_id,
                        CONVERT(FLOAT, ISNULL(cs.grand_total, 0)) AS bill_amount,
                        CONVERT(FLOAT, NULL) AS remaining_amount,
                        cs.[datetime] AS event_datetime,
                        1 AS event_order
                    FROM dbo.p_customer_sale cs
                    WHERE cs.cust_id = @cust_id

                    UNION ALL

                    SELECT
                        'C', cr.id, 0,
                        CONVERT(FLOAT, ISNULL(cr.remaining_credit_amount, 0)),
                        cr.[datetime], 2
                    FROM dbo.p_cash_receiving cr
                    WHERE cr.customer_id = @cust_id
                ) e
                ORDER BY event_datetime, event_order, event_id;

            OPEN customer_events;
            FETCH NEXT FROM customer_events INTO @event_type, @event_id, @event_bill, @event_remaining;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @event_type = 'C'
                    SET @customer_balance = @event_remaining;
                ELSE
                BEGIN
                    SET @customer_balance = @customer_balance + ISNULL(@event_bill, 0);
                    UPDATE dbo.p_customer_sale
                    SET cust_augrai = @customer_balance
                    WHERE id = @event_id;
                END

                FETCH NEXT FROM customer_events INTO @event_type, @event_id, @event_bill, @event_remaining;
            END
            CLOSE customer_events;
            DEALLOCATE customer_events;

            DECLARE
                @last_customer_bill_id NVARCHAR(50) = NULL,
                @last_customer_bill_amount FLOAT = 0,
                @last_customer_bill_date NVARCHAR(50) = NULL,
                @second_customer_bill_id NVARCHAR(50) = NULL,
                @second_customer_bill_amount INT = 0,
                @second_customer_bill_date NVARCHAR(50) = NULL;

            ;WITH b AS
            (
                SELECT cs.*,
                       ROW_NUMBER() OVER (ORDER BY cs.[datetime] DESC, cs.id DESC) AS rn
                FROM dbo.p_customer_sale cs
                WHERE cs.cust_id = @cust_id
            )
            SELECT
                @last_customer_bill_id = MAX(CASE WHEN rn = 1 THEN bill_key END),
                @last_customer_bill_amount = MAX(CASE WHEN rn = 1 THEN grand_total END),
                @last_customer_bill_date = MAX(CASE WHEN rn = 1 THEN t_date END),
                @second_customer_bill_id = MAX(CASE WHEN rn = 2 THEN bill_key END),
                @second_customer_bill_amount = CONVERT(INT, ISNULL(MAX(CASE WHEN rn = 2 THEN grand_total END), 0)),
                @second_customer_bill_date = MAX(CASE WHEN rn = 2 THEN t_date END)
            FROM b
            WHERE rn <= 2;

            UPDATE dbo.tbl_customer
            SET
                remaining_amount = @customer_balance,
                billid = @last_customer_bill_id,
                bill_amount = ISNULL(@last_customer_bill_amount, 0),
                bill_date = @last_customer_bill_date,
                seclast_bill_id = @second_customer_bill_id,
                seclast_bill_amount = ISNULL(@second_customer_bill_amount, 0),
                seclast_bill_date = @second_customer_bill_date
            WHERE cust_id = @cust_id;
        END

        /* -------------------------------------------------------------
           LANDLORD / CLIENT ACCOUNT
           p_daily bills add payable; p_daily_expense rem_amount is used as
           the authoritative checkpoint when a matching client transaction
           exists. This keeps later payments intact after an older sale edit.
           ------------------------------------------------------------- */
        IF @client_id IS NOT NULL
        BEGIN
            DECLARE @client_balance FLOAT = 0;
            DECLARE @client_event_type CHAR(1), @client_event_id INT,
                    @client_bill FLOAT, @client_remaining FLOAT;

            DECLARE client_events CURSOR LOCAL FAST_FORWARD FOR
                SELECT event_type, event_id, bill_amount, remaining_amount
                FROM
                (
                    SELECT
                        'B' AS event_type,
                        d.id AS event_id,
                        CONVERT(FLOAT, ISNULL(d.grand_total, 0)) AS bill_amount,
                        CONVERT(FLOAT, NULL) AS remaining_amount,
                        d.[datetime] AS event_datetime,
                        1 AS event_order
                    FROM dbo.p_daily d
                    WHERE d.client_id = @client_id

                    UNION ALL

                    SELECT
                        'P', de.id, 0,
                        CONVERT(FLOAT, ISNULL(de.rem_amount, 0)),
                        de.[datetime], 2
                    FROM dbo.p_daily_expense de
                    WHERE (de.ccid = @client_id OR de.eid = @client_id)
                      AND de.rem_amount IS NOT NULL
                      AND (de.ccid_type IS NULL OR de.ccid_type IN ('Client', 'Bipari', 'Landlord', 'Zamidar'))
                ) e
                ORDER BY event_datetime, event_order, event_id;

            OPEN client_events;
            FETCH NEXT FROM client_events INTO @client_event_type, @client_event_id, @client_bill, @client_remaining;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @client_event_type = 'P'
                    SET @client_balance = @client_remaining;
                ELSE
                BEGIN
                    UPDATE dbo.p_daily
                    SET last_total_bill = CONVERT(INT, ROUND(@client_balance, 0))
                    WHERE id = @client_event_id;

                    SET @client_balance = @client_balance + ISNULL(@client_bill, 0);
                END

                FETCH NEXT FROM client_events INTO @client_event_type, @client_event_id, @client_bill, @client_remaining;
            END
            CLOSE client_events;
            DEALLOCATE client_events;

            UPDATE dbo.tbl_client
            SET
                remaining_bill_amount = CONVERT(INT, ROUND(@client_balance, 0)),
                remainging_bill_date = CONVERT(NVARCHAR(50), GETDATE(), 23)
            WHERE client_id = @client_id;
        END

        COMMIT TRANSACTION;

        SELECT
            CAST(1 AS BIT) AS success,
            @daily_sale_id AS deleted_sale_id,
            @p_daily_id AS p_daily_id,
            @cust_id AS cust_id,
            @client_id AS client_id,
            @deleted_quantity AS restored_quantity;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
