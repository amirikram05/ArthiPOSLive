using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ShopRentManagementSystem.Models;

namespace ShopRentManagementSystem.Services
{
    public class SqlDataService
    {
        private readonly string _connectionString;

        public SqlDataService()
        {
            // Update connection string with your server name and database
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=live_db_pt;Trusted_Connection=True;";
        }

        #region Properties

        public List<Property> LoadProperties()
        {
            var properties = new List<Property>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name, Address, PropertyType, CreatedDate FROM rent_Properties", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        properties.Add(new Property
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Type = (PropertyType)reader.GetInt32(3),
                            CreatedDate = reader.GetDateTime(4)
                        });
                    }
                }
            }
            return properties;
        }

        public void SaveProperties(List<Property> properties)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var property in properties)
                {
                    if (property.Id == 0)
                    {
                        var cmd = new SqlCommand(@"
                            INSERT INTO rent_Properties (Name, Address, PropertyType, CreatedDate) 
                            VALUES (@Name, @Address, @PropertyType, @CreatedDate);
                            SELECT SCOPE_IDENTITY();", conn);
                        cmd.Parameters.AddWithValue("@Name", property.Name);
                        cmd.Parameters.AddWithValue("@Address", property.Address ?? "");
                        cmd.Parameters.AddWithValue("@PropertyType", (int)property.Type);
                        cmd.Parameters.AddWithValue("@CreatedDate", property.CreatedDate);
                        property.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        var cmd = new SqlCommand(@"
                            UPDATE rent_Properties SET 
                                Name = @Name, 
                                Address = @Address, 
                                PropertyType = @PropertyType 
                            WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", property.Id);
                        cmd.Parameters.AddWithValue("@Name", property.Name);
                        cmd.Parameters.AddWithValue("@Address", property.Address ?? "");
                        cmd.Parameters.AddWithValue("@PropertyType", (int)property.Type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion

        #region Portions

        public List<Portion> LoadPortions()
        {
            var portions = new List<Portion>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, PropertyId, Name, Size, IsActive FROM rent_Portions", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        portions.Add(new Portion
                        {
                            Id = reader.GetInt32(0),
                            PropertyId = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Size = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            IsActive = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return portions;
        }

        public void SavePortions(List<Portion> portions)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var portion in portions)
                {
                    if (portion.Id == 0)
                    {
                        var cmd = new SqlCommand(@"
                            INSERT INTO rent_Portions (PropertyId, Name, Size, IsActive) 
                            VALUES (@PropertyId, @Name, @Size, @IsActive);
                            SELECT SCOPE_IDENTITY();", conn);
                        cmd.Parameters.AddWithValue("@PropertyId", portion.PropertyId);
                        cmd.Parameters.AddWithValue("@Name", portion.Name);
                        cmd.Parameters.AddWithValue("@Size", portion.Size ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", portion.IsActive);
                        portion.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        var cmd = new SqlCommand(@"
                            UPDATE rent_Portions SET 
                                PropertyId = @PropertyId, 
                                Name = @Name, 
                                Size = @Size, 
                                IsActive = @IsActive 
                            WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", portion.Id);
                        cmd.Parameters.AddWithValue("@PropertyId", portion.PropertyId);
                        cmd.Parameters.AddWithValue("@Name", portion.Name);
                        cmd.Parameters.AddWithValue("@Size", portion.Size ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", portion.IsActive);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion

        #region Tenants

        public List<Tenant> LoadTenants()
        {
            var tenants = new List<Tenant>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT Id, Name, CNIC, Mobile, SecurityDeposit, 
                           StampPaperDetails, StampPaperDate, TenantType,
                           CommissionPercentage, CommissionFrequency, CustomCommissionDays, CreatedDate
                    FROM rent_Tenants", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tenants.Add(new Tenant
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CNIC = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Mobile = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            SecurityDeposit = reader.GetDecimal(4),
                            StampPaperDetails = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            StampPaperDate = reader.GetDateTime(6),
                            Type = (TenantType)reader.GetInt32(7),
                            CommissionPercentage = reader.IsDBNull(8) ? null : (decimal?)reader.GetDecimal(8),
                            CommissionFrequency = reader.IsDBNull(9) ? null : (CommissionFrequency?)reader.GetInt32(9),
                            CustomCommissionDays = reader.IsDBNull(10) ? null : (int?)reader.GetInt32(10),
                            CreatedDate = reader.GetDateTime(11)
                        });
                    }
                }
            }
            return tenants;
        }

        public void SaveTenants(List<Tenant> tenants)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var tenant in tenants)
                {
                    if (tenant.Id == 0)
                    {
                        var cmd = new SqlCommand(@"
                            INSERT INTO rent_Tenants (Name, CNIC, Mobile, SecurityDeposit, StampPaperDetails, 
                                                StampPaperDate, TenantType, CommissionPercentage, 
                                                CommissionFrequency, CustomCommissionDays, CreatedDate) 
                            VALUES (@Name, @CNIC, @Mobile, @SecurityDeposit, @StampPaperDetails, 
                                   @StampPaperDate, @TenantType, @CommissionPercentage, 
                                   @CommissionFrequency, @CustomCommissionDays, @CreatedDate);
                            SELECT SCOPE_IDENTITY();", conn);
                        cmd.Parameters.AddWithValue("@Name", tenant.Name);
                        cmd.Parameters.AddWithValue("@CNIC", tenant.CNIC ?? "");
                        cmd.Parameters.AddWithValue("@Mobile", tenant.Mobile ?? "");
                        cmd.Parameters.AddWithValue("@SecurityDeposit", tenant.SecurityDeposit);
                        cmd.Parameters.AddWithValue("@StampPaperDetails", tenant.StampPaperDetails ?? "");
                        cmd.Parameters.AddWithValue("@StampPaperDate", tenant.StampPaperDate);
                        cmd.Parameters.AddWithValue("@TenantType", (int)tenant.Type);
                        cmd.Parameters.AddWithValue("@CommissionPercentage", (object)tenant.CommissionPercentage ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CommissionFrequency", (object)(int?)tenant.CommissionFrequency ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CustomCommissionDays", (object)tenant.CustomCommissionDays ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedDate", tenant.CreatedDate);
                        tenant.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        var cmd = new SqlCommand(@"
                            UPDATE rent_Tenants SET 
                                Name = @Name, 
                                CNIC = @CNIC, 
                                Mobile = @Mobile, 
                                SecurityDeposit = @SecurityDeposit,
                                StampPaperDetails = @StampPaperDetails,
                                StampPaperDate = @StampPaperDate,
                                TenantType = @TenantType,
                                CommissionPercentage = @CommissionPercentage,
                                CommissionFrequency = @CommissionFrequency,
                                CustomCommissionDays = @CustomCommissionDays
                            WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", tenant.Id);
                        cmd.Parameters.AddWithValue("@Name", tenant.Name);
                        cmd.Parameters.AddWithValue("@CNIC", tenant.CNIC ?? "");
                        cmd.Parameters.AddWithValue("@Mobile", tenant.Mobile ?? "");
                        cmd.Parameters.AddWithValue("@SecurityDeposit", tenant.SecurityDeposit);
                        cmd.Parameters.AddWithValue("@StampPaperDetails", tenant.StampPaperDetails ?? "");
                        cmd.Parameters.AddWithValue("@StampPaperDate", tenant.StampPaperDate);
                        cmd.Parameters.AddWithValue("@TenantType", (int)tenant.Type);
                        cmd.Parameters.AddWithValue("@CommissionPercentage", (object)tenant.CommissionPercentage ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CommissionFrequency", (object)(int?)tenant.CommissionFrequency ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CustomCommissionDays", (object)tenant.CustomCommissionDays ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion

        #region Products

        public List<Product> LoadProducts()
        {
            var products = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name, Unit, UnitPrice, IsActive, CreatedDate FROM rent_Products", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Unit = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3),
                            IsActive = reader.GetBoolean(4),
                            CreatedDate = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return products;
        }

        public void SaveProducts(List<Product> products)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (var product in products)
                {
                    if (product.Id == 0)
                    {
                        var cmd = new SqlCommand(@"
                            INSERT INTO rent_Products (Name, Unit, UnitPrice, IsActive, CreatedDate) 
                            VALUES (@Name, @Unit, @UnitPrice, @IsActive, @CreatedDate);
                            SELECT SCOPE_IDENTITY();", conn);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Unit", product.Unit ?? "");
                        cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                        cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);
                        product.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        var cmd = new SqlCommand(@"
                            UPDATE rent_Products SET 
                                Name = @Name, 
                                Unit = @Unit, 
                                UnitPrice = @UnitPrice, 
                                IsActive = @IsActive 
                            WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Unit", product.Unit ?? "");
                        cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                        cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Product> GetAllActiveProducts()
        {
            var products = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name, Unit, UnitPrice FROM rent_Products WHERE IsActive = 1", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Unit = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return products;
        }

        public Product GetProductById(int productId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Name, Unit, UnitPrice FROM rent_Products WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Unit = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3)
                        };
                    }
                }
            }
            return null;
        }

        #endregion

        #region RentAgreements

        public List<RentAgreement> LoadAgreements()
        {
            var agreements = new List<RentAgreement>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT Id, PropertyId, PortionId, TenantId, MonthlyRent, StartDate, NextDueDate,
                           IncreaseMode, LastIncreaseDate, IsActive, DailyMinimumTarget, CommissionRate,
                           PaymentFrequency, CustomPaymentDays, LastCommissionPaymentDate
                    FROM rent_RentAgreements", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var agreement = new RentAgreement
                        {
                            Id = reader.GetInt32(0),
                            PropertyId = reader.GetInt32(1),
                            PortionId = reader.GetInt32(2),
                            TenantId = reader.GetInt32(3),
                            MonthlyRent = reader.GetDecimal(4),
                            StartDate = reader.GetDateTime(5),
                            NextDueDate = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            IncreaseMode = (RentIncreaseMode)reader.GetInt32(7),
                            LastIncreaseDate = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8),
                            IsActive = reader.GetBoolean(9),
                            DailyMinimumTarget = reader.IsDBNull(10) ? null : (decimal?)reader.GetDecimal(10),
                            CommissionRate = reader.IsDBNull(11) ? null : (decimal?)reader.GetDecimal(11),
                            PaymentFrequency = reader.IsDBNull(12) ? null : (CommissionFrequency?)reader.GetInt32(12),
                            CustomPaymentDays = reader.IsDBNull(13) ? null : (int?)reader.GetInt32(13),
                            LastCommissionPaymentDate = reader.IsDBNull(14) ? null : (DateTime?)reader.GetDateTime(14),
                            ProductIds = new List<int>()
                        };
                        agreements.Add(agreement);
                    }
                }

                // Load products for each agreement
                foreach (var agreement in agreements)
                {
                    var productCmd = new SqlCommand("SELECT ProductId FROM rent_AgreementProducts WHERE AgreementId = @AgreementId", conn);
                    productCmd.Parameters.AddWithValue("@AgreementId", agreement.Id);
                    using (var reader = productCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agreement.ProductIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return agreements;
        }

        public void SaveAgreements(List<RentAgreement> agreements)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var agreement in agreements)
                        {
                            if (agreement.Id == 0)
                            {
                                var cmd = new SqlCommand(@"
                                    INSERT INTO rent_RentAgreements (PropertyId, PortionId, TenantId, MonthlyRent, StartDate,
                                                               NextDueDate, IncreaseMode, LastIncreaseDate, IsActive,
                                                               DailyMinimumTarget, CommissionRate, PaymentFrequency,
                                                               CustomPaymentDays, LastCommissionPaymentDate) 
                                    VALUES (@PropertyId, @PortionId, @TenantId, @MonthlyRent, @StartDate,
                                            @NextDueDate, @IncreaseMode, @LastIncreaseDate, @IsActive,
                                            @DailyMinimumTarget, @CommissionRate, @PaymentFrequency,
                                            @CustomPaymentDays, @LastCommissionPaymentDate);
                                    SELECT SCOPE_IDENTITY();", conn, transaction);
                                cmd.Parameters.AddWithValue("@PropertyId", agreement.PropertyId);
                                cmd.Parameters.AddWithValue("@PortionId", agreement.PortionId);
                                cmd.Parameters.AddWithValue("@TenantId", agreement.TenantId);
                                cmd.Parameters.AddWithValue("@MonthlyRent", agreement.MonthlyRent);
                                cmd.Parameters.AddWithValue("@StartDate", agreement.StartDate);
                                cmd.Parameters.AddWithValue("@NextDueDate", (object)agreement.NextDueDate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IncreaseMode", (int)agreement.IncreaseMode);
                                cmd.Parameters.AddWithValue("@LastIncreaseDate", agreement.LastIncreaseDate);
                                cmd.Parameters.AddWithValue("@IsActive", agreement.IsActive);
                                cmd.Parameters.AddWithValue("@DailyMinimumTarget", (object)agreement.DailyMinimumTarget ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CommissionRate", (object)agreement.CommissionRate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PaymentFrequency", (object)(int?)agreement.PaymentFrequency ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CustomPaymentDays", (object)agreement.CustomPaymentDays ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LastCommissionPaymentDate", (object)agreement.LastCommissionPaymentDate ?? DBNull.Value);
                                agreement.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            else
                            {
                                var cmd = new SqlCommand(@"
                                    UPDATE rent_RentAgreements SET 
                                        PropertyId = @PropertyId,
                                        PortionId = @PortionId,
                                        TenantId = @TenantId,
                                        MonthlyRent = @MonthlyRent,
                                        StartDate = @StartDate,
                                        NextDueDate = @NextDueDate,
                                        IncreaseMode = @IncreaseMode,
                                        LastIncreaseDate = @LastIncreaseDate,
                                        IsActive = @IsActive,
                                        DailyMinimumTarget = @DailyMinimumTarget,
                                        CommissionRate = @CommissionRate,
                                        PaymentFrequency = @PaymentFrequency,
                                        CustomPaymentDays = @CustomPaymentDays,
                                        LastCommissionPaymentDate = @LastCommissionPaymentDate
                                    WHERE Id = @Id", conn, transaction);
                                cmd.Parameters.AddWithValue("@Id", agreement.Id);
                                cmd.Parameters.AddWithValue("@PropertyId", agreement.PropertyId);
                                cmd.Parameters.AddWithValue("@PortionId", agreement.PortionId);
                                cmd.Parameters.AddWithValue("@TenantId", agreement.TenantId);
                                cmd.Parameters.AddWithValue("@MonthlyRent", agreement.MonthlyRent);
                                cmd.Parameters.AddWithValue("@StartDate", agreement.StartDate);
                                cmd.Parameters.AddWithValue("@NextDueDate", (object)agreement.NextDueDate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IncreaseMode", (int)agreement.IncreaseMode);
                                cmd.Parameters.AddWithValue("@LastIncreaseDate", agreement.LastIncreaseDate);
                                cmd.Parameters.AddWithValue("@IsActive", agreement.IsActive);
                                cmd.Parameters.AddWithValue("@DailyMinimumTarget", (object)agreement.DailyMinimumTarget ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CommissionRate", (object)agreement.CommissionRate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PaymentFrequency", (object)(int?)agreement.PaymentFrequency ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CustomPaymentDays", (object)agreement.CustomPaymentDays ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LastCommissionPaymentDate", (object)agreement.LastCommissionPaymentDate ?? DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }

                            // Save agreement products
                            var deleteCmd = new SqlCommand("DELETE FROM rent_AgreementProducts WHERE AgreementId = @AgreementId", conn, transaction);
                            deleteCmd.Parameters.AddWithValue("@AgreementId", agreement.Id);
                            deleteCmd.ExecuteNonQuery();

                            if (agreement.ProductIds != null)
                            {
                                foreach (var productId in agreement.ProductIds)
                                {
                                    var insertCmd = new SqlCommand("INSERT INTO rent_AgreementProducts (AgreementId, ProductId) VALUES (@AgreementId, @ProductId)", conn, transaction);
                                    insertCmd.Parameters.AddWithValue("@AgreementId", agreement.Id);
                                    insertCmd.Parameters.AddWithValue("@ProductId", productId);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion

        #region Payments

        public List<Payment> LoadPayments()
        {
            var payments = new List<Payment>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT Id, AgreementId, Amount, PaymentDate, MonthYear, Notes, IsDeleted,
                           CreatedDate, DeletedDate, PaymentType, SalesAmount, CommissionEarned,
                           ProductId, ProductName, Quantity, Unit, UnitPrice, LaborAmount, ProductTotal
                    FROM rent_Payments WHERE IsDeleted = 0", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            Id = reader.GetInt32(0),
                            AgreementId = reader.GetInt32(1),
                            Amount = reader.GetDecimal(2),
                            PaymentDate = reader.GetDateTime(3),
                            MonthYear = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Notes = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            IsDeleted = reader.GetBoolean(6),
                            CreatedDate = reader.GetDateTime(7),
                            DeletedDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            PaymentType = (PaymentType)reader.GetInt32(9),
                            SalesAmount = reader.IsDBNull(10) ? null : (decimal?)reader.GetDecimal(10),
                            CommissionEarned = reader.IsDBNull(11) ? null : (decimal?)reader.GetDecimal(11),
                            ProductId = reader.IsDBNull(12) ? null : (int?)reader.GetInt32(12),
                            ProductName = reader.IsDBNull(13) ? null : reader.GetString(13),
                            Quantity = reader.IsDBNull(14) ? null : (decimal?)reader.GetDecimal(14),
                            Unit = reader.IsDBNull(15) ? null : reader.GetString(15),
                            UnitPrice = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                            LaborAmount = reader.IsDBNull(17) ? null : (decimal?)reader.GetDecimal(17),
                            ProductTotal = reader.IsDBNull(18) ? null : (decimal?)reader.GetDecimal(18)
                        });
                    }
                }
            }
            return payments;
        }

        public List<Payment> LoadAllPayments()
        {
            var payments = new List<Payment>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT Id, AgreementId, Amount, PaymentDate, MonthYear, Notes, IsDeleted,
                           CreatedDate, DeletedDate, PaymentType, SalesAmount, CommissionEarned,
                           ProductId, ProductName, Quantity, Unit, UnitPrice, LaborAmount, ProductTotal
                    FROM rent_Payments", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            Id = reader.GetInt32(0),
                            AgreementId = reader.GetInt32(1),
                            Amount = reader.GetDecimal(2),
                            PaymentDate = reader.GetDateTime(3),
                            MonthYear = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Notes = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            IsDeleted = reader.GetBoolean(6),
                            CreatedDate = reader.GetDateTime(7),
                            DeletedDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            PaymentType = (PaymentType)reader.GetInt32(9),
                            SalesAmount = reader.IsDBNull(10) ? null : (decimal?)reader.GetDecimal(10),
                            CommissionEarned = reader.IsDBNull(11) ? null : (decimal?)reader.GetDecimal(11),
                            ProductId = reader.IsDBNull(12) ? null : (int?)reader.GetInt32(12),
                            ProductName = reader.IsDBNull(13) ? null : reader.GetString(13),
                            Quantity = reader.IsDBNull(14) ? null : (decimal?)reader.GetDecimal(14),
                            Unit = reader.IsDBNull(15) ? null : reader.GetString(15),
                            UnitPrice = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                            LaborAmount = reader.IsDBNull(17) ? null : (decimal?)reader.GetDecimal(17),
                            ProductTotal = reader.IsDBNull(18) ? null : (decimal?)reader.GetDecimal(18)
                        });
                    }
                }
            }
            return payments;
        }

        public void SavePayments(List<Payment> payments)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var payment in payments)
                        {
                            if (payment.Id == 0)
                            {
                                var cmd = new SqlCommand(@"
                                    INSERT INTO rent_Payments (AgreementId, Amount, PaymentDate, MonthYear, Notes,
                                                          IsDeleted, CreatedDate, PaymentType, SalesAmount,
                                                          CommissionEarned, ProductId, ProductName, Quantity,
                                                          Unit, UnitPrice, LaborAmount, ProductTotal) 
                                    VALUES (@AgreementId, @Amount, @PaymentDate, @MonthYear, @Notes,
                                            @IsDeleted, @CreatedDate, @PaymentType, @SalesAmount,
                                            @CommissionEarned, @ProductId, @ProductName, @Quantity,
                                            @Unit, @UnitPrice, @LaborAmount, @ProductTotal);
                                    SELECT SCOPE_IDENTITY();", conn, transaction);
                                cmd.Parameters.AddWithValue("@AgreementId", payment.AgreementId);
                                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                                cmd.Parameters.AddWithValue("@MonthYear", payment.MonthYear ?? "");
                                cmd.Parameters.AddWithValue("@Notes", payment.Notes ?? "");
                                cmd.Parameters.AddWithValue("@IsDeleted", payment.IsDeleted);
                                cmd.Parameters.AddWithValue("@CreatedDate", payment.CreatedDate);
                                cmd.Parameters.AddWithValue("@PaymentType", (int)payment.PaymentType);
                                cmd.Parameters.AddWithValue("@SalesAmount", (object)payment.SalesAmount ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CommissionEarned", (object)payment.CommissionEarned ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductId", (object)payment.ProductId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductName", (object)payment.ProductName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Quantity", (object)payment.Quantity ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Unit", (object)payment.Unit ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@UnitPrice", (object)payment.UnitPrice ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LaborAmount", (object)payment.LaborAmount ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductTotal", (object)payment.ProductTotal ?? DBNull.Value);
                                payment.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            else
                            {
                                var cmd = new SqlCommand(@"
                                    UPDATE rent_Payments SET 
                                        AgreementId = @AgreementId,
                                        Amount = @Amount,
                                        PaymentDate = @PaymentDate,
                                        MonthYear = @MonthYear,
                                        Notes = @Notes,
                                        IsDeleted = @IsDeleted,
                                        DeletedDate = @DeletedDate,
                                        PaymentType = @PaymentType,
                                        SalesAmount = @SalesAmount,
                                        CommissionEarned = @CommissionEarned,
                                        ProductId = @ProductId,
                                        ProductName = @ProductName,
                                        Quantity = @Quantity,
                                        Unit = @Unit,
                                        UnitPrice = @UnitPrice,
                                        LaborAmount = @LaborAmount,
                                        ProductTotal = @ProductTotal
                                    WHERE Id = @Id", conn, transaction);
                                cmd.Parameters.AddWithValue("@Id", payment.Id);
                                cmd.Parameters.AddWithValue("@AgreementId", payment.AgreementId);
                                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                                cmd.Parameters.AddWithValue("@MonthYear", payment.MonthYear ?? "");
                                cmd.Parameters.AddWithValue("@Notes", payment.Notes ?? "");
                                cmd.Parameters.AddWithValue("@IsDeleted", payment.IsDeleted);
                                cmd.Parameters.AddWithValue("@DeletedDate", (object)payment.DeletedDate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@PaymentType", (int)payment.PaymentType);
                                cmd.Parameters.AddWithValue("@SalesAmount", (object)payment.SalesAmount ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@CommissionEarned", (object)payment.CommissionEarned ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductId", (object)payment.ProductId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductName", (object)payment.ProductName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Quantity", (object)payment.Quantity ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Unit", (object)payment.Unit ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@UnitPrice", (object)payment.UnitPrice ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LaborAmount", (object)payment.LaborAmount ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ProductTotal", (object)payment.ProductTotal ?? DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Payment> GetPaymentsByAgreement(int agreementId)
        {
            var payments = new List<Payment>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT Id, AgreementId, Amount, PaymentDate, MonthYear, Notes, IsDeleted,
                           CreatedDate, DeletedDate, PaymentType, SalesAmount, CommissionEarned,
                           ProductId, ProductName, Quantity, Unit, UnitPrice, LaborAmount, ProductTotal
                    FROM rent_Payments WHERE AgreementId = @AgreementId AND IsDeleted = 0
                    ORDER BY PaymentDate DESC", conn);
                cmd.Parameters.AddWithValue("@AgreementId", agreementId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            Id = reader.GetInt32(0),
                            AgreementId = reader.GetInt32(1),
                            Amount = reader.GetDecimal(2),
                            PaymentDate = reader.GetDateTime(3),
                            MonthYear = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Notes = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            IsDeleted = reader.GetBoolean(6),
                            CreatedDate = reader.GetDateTime(7),
                            DeletedDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            PaymentType = (PaymentType)reader.GetInt32(9),
                            SalesAmount = reader.IsDBNull(10) ? null : (decimal?)reader.GetDecimal(10),
                            CommissionEarned = reader.IsDBNull(11) ? null : (decimal?)reader.GetDecimal(11),
                            ProductId = reader.IsDBNull(12) ? null : (int?)reader.GetInt32(12),
                            ProductName = reader.IsDBNull(13) ? null : reader.GetString(13),
                            Quantity = reader.IsDBNull(14) ? null : (decimal?)reader.GetDecimal(14),
                            Unit = reader.IsDBNull(15) ? null : reader.GetString(15),
                            UnitPrice = reader.IsDBNull(16) ? null : (decimal?)reader.GetDecimal(16),
                            LaborAmount = reader.IsDBNull(17) ? null : (decimal?)reader.GetDecimal(17),
                            ProductTotal = reader.IsDBNull(18) ? null : (decimal?)reader.GetDecimal(18)
                        });
                    }
                }
            }
            return payments;
        }

        public bool DeletePayment(int paymentId, string reason = "")
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        UPDATE rent_Payments 
                        SET IsDeleted = 1, DeletedDate = GETDATE(), Notes = Notes + @DeleteNote
                        WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", paymentId);
                    cmd.Parameters.AddWithValue("@DeleteNote", $"\n[DELETED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}] Reason: {reason}");
                    cmd.ExecuteNonQuery();

                    // Log deletion
                    var logCmd = new SqlCommand(@"
                        INSERT INTO rent_DeletionLog (PaymentId, Reason, DeletedBy)
                        VALUES (@PaymentId, @Reason, @DeletedBy)", conn);
                    logCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                    logCmd.Parameters.AddWithValue("@Reason", reason);
                    logCmd.Parameters.AddWithValue("@DeletedBy", Environment.UserName);
                    logCmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RestorePayment(int paymentId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(@"
                        UPDATE rent_Payments 
                        SET IsDeleted = 0, DeletedDate = NULL
                        WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", paymentId);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Commission Transactions

        public List<CommissionTransaction> LoadCommissionTransactions()
        {
            var transactions = new List<CommissionTransaction>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM rent_CommissionTransactions", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new CommissionTransaction
                        {
                            Id = reader.GetInt32(0),
                            PaymentId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            ProductName = reader.GetString(3),
                            Quantity = reader.GetDecimal(4),
                            Unit = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            UnitPrice = reader.GetDecimal(6),
                            LaborAmount = reader.GetDecimal(7),
                            ProductTotal = reader.GetDecimal(8),
                            CommissionRate = reader.GetDecimal(9),
                            CommissionAmount = reader.GetDecimal(10),
                            TransactionDate = reader.GetDateTime(11)
                        });
                    }
                }
            }
            return transactions;
        }

        public void SaveCommissionTransactions(List<CommissionTransaction> transactions)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var ct in transactions)
                        {
                            if (ct.Id == 0)
                            {
                                var cmd = new SqlCommand(@"
                                    INSERT INTO rent_CommissionTransactions (PaymentId, ProductId, ProductName, Quantity,
                                                                       Unit, UnitPrice, LaborAmount, ProductTotal,
                                                                       CommissionRate, CommissionAmount, TransactionDate)
                                    VALUES (@PaymentId, @ProductId, @ProductName, @Quantity,
                                            @Unit, @UnitPrice, @LaborAmount, @ProductTotal,
                                            @CommissionRate, @CommissionAmount, @TransactionDate);
                                    SELECT SCOPE_IDENTITY();", conn, transaction);
                                cmd.Parameters.AddWithValue("@PaymentId", ct.PaymentId);
                                cmd.Parameters.AddWithValue("@ProductId", ct.ProductId);
                                cmd.Parameters.AddWithValue("@ProductName", ct.ProductName);
                                cmd.Parameters.AddWithValue("@Quantity", ct.Quantity);
                                cmd.Parameters.AddWithValue("@Unit", ct.Unit ?? "");
                                cmd.Parameters.AddWithValue("@UnitPrice", ct.UnitPrice);
                                cmd.Parameters.AddWithValue("@LaborAmount", ct.LaborAmount);
                                cmd.Parameters.AddWithValue("@ProductTotal", ct.ProductTotal);
                                cmd.Parameters.AddWithValue("@CommissionRate", ct.CommissionRate);
                                cmd.Parameters.AddWithValue("@CommissionAmount", ct.CommissionAmount);
                                cmd.Parameters.AddWithValue("@TransactionDate", ct.TransactionDate);
                                ct.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<CommissionTransaction> GetCommissionTransactionsForPayment(int paymentId)
        {
            var transactions = new List<CommissionTransaction>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM rent_CommissionTransactions WHERE PaymentId = @PaymentId", conn);
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new CommissionTransaction
                        {
                            Id = reader.GetInt32(0),
                            PaymentId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            ProductName = reader.GetString(3),
                            Quantity = reader.GetDecimal(4),
                            Unit = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            UnitPrice = reader.GetDecimal(6),
                            LaborAmount = reader.GetDecimal(7),
                            ProductTotal = reader.GetDecimal(8),
                            CommissionRate = reader.GetDecimal(9),
                            CommissionAmount = reader.GetDecimal(10),
                            TransactionDate = reader.GetDateTime(11)
                        });
                    }
                }
            }
            return transactions;
        }

        public CommissionSummary GetCommissionSummary(int paymentId)
        {
            var transactions = GetCommissionTransactionsForPayment(paymentId);
            if (!transactions.Any()) return null;

            return new CommissionSummary
            {
                TotalQuantity = transactions.Sum(t => t.Quantity),
                TotalProductValue = transactions.Sum(t => t.ProductTotal),
                TotalLabor = transactions.Sum(t => t.LaborAmount),
                TotalSales = transactions.Sum(t => t.ProductTotal + t.LaborAmount),
                TotalCommission = transactions.Sum(t => t.CommissionAmount)
            };
        }

        public CommissionTransaction CalculateCommission(int productId, decimal quantity, decimal laborAmount, decimal commissionRate)
        {
            var product = GetProductById(productId);
            if (product == null) return null;

            decimal productTotal = quantity * product.UnitPrice;
            decimal totalAmount = productTotal + laborAmount;
            decimal commissionAmount = (totalAmount * commissionRate) / 100;

            return new CommissionTransaction
            {
                ProductId = productId,
                ProductName = product.Name,
                Quantity = quantity,
                Unit = product.Unit,
                UnitPrice = product.UnitPrice,
                LaborAmount = laborAmount,
                ProductTotal = productTotal,
                CommissionRate = commissionRate,
                CommissionAmount = commissionAmount,
                TransactionDate = DateTime.Now
            };
        }

        #endregion

        #region Helper Methods

        public List<ProductSelection> GetProductSelectionsForAgreement(int agreementId)
        {
            var selections = new List<ProductSelection>();
            var allProducts = GetAllActiveProducts();
            var agreementProducts = new List<int>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT ProductId FROM rent_AgreementProducts WHERE AgreementId = @AgreementId", conn);
                cmd.Parameters.AddWithValue("@AgreementId", agreementId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        agreementProducts.Add(reader.GetInt32(0));
                    }
                }
            }

            foreach (var product in allProducts)
            {
                selections.Add(new ProductSelection
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Unit = product.Unit,
                    UnitPrice = product.UnitPrice,
                    IsSelected = agreementProducts.Contains(product.Id)
                });
            }

            return selections;
        }

        public List<Product> GetProductsForAgreement(int agreementId)
        {
            var products = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT p.Id, p.Name, p.Unit, p.UnitPrice
                    FROM rent_Products p
                    INNER JOIN rent_AgreementProducts ap ON p.Id = ap.ProductId
                    WHERE ap.AgreementId = @AgreementId AND p.IsActive = 1", conn);
                cmd.Parameters.AddWithValue("@AgreementId", agreementId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Unit = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return products;
        }

        private int GetDaysFromFrequency(CommissionFrequency frequency, int? customDays)
        {
            switch (frequency)
            {
                case CommissionFrequency.Daily:
                    return 1;
                case CommissionFrequency.Every5Days:
                    return 5;
                case CommissionFrequency.Every10Days:
                    return 10;
                case CommissionFrequency.Weekly:
                    return 7;
                case CommissionFrequency.Monthly:
                    return 30;
                case CommissionFrequency.Custom:
                    return customDays ?? 7;
                default:
                    return 7;
            }
        }

        #endregion
    }
}