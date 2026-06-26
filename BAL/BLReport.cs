using DAL;
using System.Collections.Generic;
using System.Data;

namespace BAL
{
    public class BLReport
    {
        DBReporting db;
        public BLReport()
        {
            db = new DBReporting();
        }
        public object p_balance_sheet_read(string @starDate, string @lastdate, int pageIndex, int PageSize)
        {
            return db.p_reporting_CRUD("ReadBalanceSheet", starDate, lastdate, pageIndex, PageSize, "");
        }
        public object p_AugraiDateDetail(string @stardate, string @lastdate, int pageIndex, int PageSize)
        {
            return db.p_reporting_CRUD("AugraiDateDetail", stardate, lastdate, pageIndex, PageSize, "");
        }
        public object p_CustBillsandReceivings(string @stardate, string @lastdate, string search)
        {
            return db.p_reporting_CRUD("CustBillsandReceivings", stardate, lastdate, 0, 0, search);
        }

        public DataTable getRpName(string action)
        {
            return db.p_report_data_all(action, "", "", "", "", "");
        }
        public DataTable getReportDataAll(string action, string reportno, string search, string stardate, string lastdate, string filter)
        {
            return db.p_report_data_all(action, reportno, search, stardate, lastdate, filter);

        }
        public DataTable GetShopExpenseReport(string stardate, string lastdate, int section)
        {
            return db.GetShopExpenseReport(stardate, lastdate, section);

        }

        public object p_DetailReport(string @stardate, string @lastdate, string search)
        {
            return db.p_reporting_CRUD("DetailReport", stardate, lastdate, 0, 0, search);
        }
        public object landlordSaleDetail(string @stardate, string @lastdate, string search)
        {
            return db.p_reporting_CRUD("LandlordSaleDetail", stardate, lastdate, 0, 0, search);
        }
        public object p_ClientSaleDetail(string @stardate, string @lastdate, string search)
        {
            return db.p_reporting_CRUD("ClientSaleDetail", stardate, lastdate, 0, 0, search);
        }

        public object p_expenseCashReceive(string stardate, string lastdate, int index, int pageSize)
        {
            return db.p_reporting_CRUD("ExpenseCashReceive", stardate, lastdate, index, pageSize, "");

        }
        public object cashReceving(string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return db.p_reporting_CRUD("CashReceive", stardate, lastdate, pageIndex, PageSize, search);
        }
        public object expenseDetails(string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return db.p_reporting_CRUD("SalesExpense", stardate, lastdate, pageIndex, PageSize, search);
        }
        public object getSalesLandlord(string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return db.p_reporting_CRUD("GetLandloard", stardate, lastdate, pageIndex, PageSize, search);
        }

        public DataTable getProfiftLossDetails(string sdate, string ldate)
        {
            return (DataTable)db.p_all_sale_profit_details("Date", sdate, ldate);
        }

        public object getSalesClient(string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return db.p_reporting_CRUD("BSales", stardate, lastdate, pageIndex, PageSize, search);
        }

        public object customersales(string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return db.p_reporting_CRUD("CustomerSales", stardate, lastdate, pageIndex, PageSize, search);
        }

        public List<object> p_dailyProfitSalesExpense(string action, string sdate, string ldate, int index, int pageSize)
        {
            return db.p_dailyProfitSalesExpense(action, sdate, ldate, index, pageSize);
        }
        public List<object> p_reporting_CRUD(string action, string @stardate, string @lastdate, int pageIndex, int PageSize, string search)
        {
            return (List<object>)db.p_reporting_CRUD(action, stardate, lastdate, pageIndex, PageSize, search);
        }


    }
}
