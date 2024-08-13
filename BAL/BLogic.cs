using DAL;
using DataMember;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCalls.firebase;
using System.Net.Http;
namespace BAL
{
    public class BLogic
    {
        DBHandler db;
        public BLogic()
        {
            db = new DBHandler();
        }
        
        public bool backupDb(string path,int localcheck)
        {
            return db.backupDB(path, localcheck);
        }
        public bool restoreDB(string path)
        {
            return db.restoreDB(path);
        }

        public DataTable p_chatha(string startdate, string lastdate)
        {
            return db.p_chatha(startdate,lastdate);
        }

        public DataTable p_bs_read(string action, string sdate, string ldate)
        {
            return db.p_balancesheet_Read(action,sdate,ldate);
        }

        public DataTable getCashInout(string action, string date)
        {
            return db.getCashInout(action,date);
        }
        public bool p_cashinout_Crud(string aciton, string keyid, string date, string catename, int cateid, int transactionid, int account_transaction_id
            , int typeid, string cash_type, int uid, string uname,
            string detialdesp, int amount, int discount, string entrytype,string categoryid,string action_type)
        {
            return db.p_cashinout_Crud( aciton,  keyid,  date,  catename,  cateid,  transactionid,  account_transaction_id
            ,  typeid,  cash_type,  uid,  uname,  detialdesp,
            amount,  discount,  entrytype, categoryid,action_type);
        }

        public DataTable getCashInoutAccount()
        {
            
            return db.p_accountCrud("RCInout", new Account());
        }
        public bool updateCashInoutAccount(Account ac)
        {
            return db.updateAccount("CashInout", ac);
        }


        public object createSeason(string sdate, string ldate)
        {
            return db.createSeason("Create",sdate,ldate);
        }
        public object deleteSeason(string id)
        {
            return db.createSeason("Delete", id, "");
        }
        public DataTable seasonList(string sdate, string ldate)
        {
            List<object> obj = (List<object>)db.createSeason("Read", sdate,ldate);
            if (obj == null)
            {
                return null;
            }
            DataTable dt = (DataTable)obj[1];
            return dt;
        }

        public DataTable getExpenseTypes(string search)
        {
           return getCategory("Read", search);
        }
        public DataTable getLedgerRead(string action ,string sdate,string ldate)
        {
            return db.p_ledger_Read(action,sdate,ldate);
        }

        public DataTable p_pagetSetting(string action)
        {
            return db.p_pagetSettingLoad(action);
        }


        public DataTable getListLandlordBill(string action,string clientid,string sdate,string ldate,string status,string desc)
        {
            return db.p_BillingPayingDetail(action, clientid, sdate, ldate, status,desc);
        }

        public string[] getID(string type)
        {
            return db.p_getID(type);
        }
        public AutoCompleteStringCollection getCustomeName(BillKey.EnumUser myNum)
        {
            if(myNum==BillKey.EnumUser.Client)
                return db.suggestionCustClient("ClientSuggestion");
            else if (myNum == BillKey.EnumUser.Customer)
                return db.suggestionCustClient("CustomerSuggestion");

            return null;
        }

        public DataTable searchBillDetail(string isCustomer, string idname, string sdate, string ldate,string status)
        {
            return db.p_billingDetailPaid(isCustomer, idname, sdate, ldate, status);
        }

        public bool p_ledger_CRUD(string action, string transaction_id, string acc_trans_id, string entry_type, 
            int amount, int userid, string usertype, string date,string key,string expenseid,string entry_action,string category_id)
        {
            return db.p_ledger_CRUD("Insert", transaction_id, acc_trans_id, entry_type, amount, userid, usertype, date, key, expenseid, entry_action, category_id);
        }
        public bool p_insert_CapitalCash(string date, string password, string cash,
            string key,string type,string desc,string account_transaction_id,string category_id)
        {
            int id=db.p_cashamount_CRUD("CashInsert",date,"1", cash,type, desc,key);
            db.update_today_sales(date);
            return db.addBalanceSheetExpense(desc, cash, date, nameof(BillKey.EnumUser.Admin),
                ""+id, "debit", "Insert", "0", account_transaction_id, category_id);

        }
        public bool p_addCash(string action, string date, int id, string desc,
            int amount, int discount,int cashtype,string key,string expenseid,
            string transactionid, string name,string acctransid,
            string datetime,string entry_action,string category_id)
        {
            bool chekc=db.p_addCash(action, date, id, desc,amount,discount, cashtype,key, acctransid, transactionid, name, datetime, category_id, expenseid);
            if (!chekc) return false;
            bool chk = false;
            if (chekc)
            {
                chk = db.update_today_sales(date);
                if (action == "Customer")
                {
                    p_ledger_CRUD("Insert", transactionid, acctransid, 
                        "D", amount, id, action, date,key, expenseid, entry_action,category_id);
                    db.addBalanceSheetExpense(desc, "" + amount, date, action, key, "debit", "Insert", "0",acctransid,category_id);
                }
                else if (action == "Expense")
                {
                    p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, action, date, key, expenseid, entry_action,category_id);
                    db.addBalanceSheetExpense(desc, "" + amount, date, action, key, "credit", "Insert", "0",acctransid, category_id);
                }
                else if (action == "Client" || action == "ClientInvest" || action == "Admin" || action == "ClientRemReceive")
                {
                    if (cashtype == 7 || cashtype == 5 || cashtype == 3 || cashtype == 15)
                    {
                        p_ledger_CRUD("Insert", transactionid, acctransid, "D", amount, id, action, date, key, expenseid, entry_action,category_id);
                        db.addBalanceSheetExpense(desc, "" + amount, date, action, key, "debit", "Insert", "0",acctransid, category_id);
                    }
                    else if (cashtype == 8 || cashtype == 4 || cashtype == 1)
                    {
                        p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, action, date, key, expenseid, entry_action, category_id);
                        db.addBalanceSheetExpense(desc, "" + amount, date, action, key, "credit", "Insert", "0",acctransid, category_id);

                    }
                }
                else if (action == "Delete")
                {
                    if (cashtype == 1) { p_ledger_CRUD("Insert", transactionid, acctransid, "D", amount, id, "Client", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 15) { p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, "ClientRemReceive", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 5) { p_ledger_CRUD("Insert", transactionid, acctransid, "D", amount, id, "ClientInvest", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 7) { p_ledger_CRUD("Insert", transactionid, acctransid, "D", amount, id, "Admin", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 12) { p_ledger_CRUD("Insert", expenseid, acctransid, "D", amount, id, "Expense", date, key,  transactionid, "D", category_id); }
                    else if (cashtype == 14) { p_ledger_CRUD("Insert", transactionid, acctransid, "D", amount, id, "ShopExpense", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 8) { p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, "Admin", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 4) { p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, "ClientInvest", date, key, expenseid, "D", category_id); }
                    else if (cashtype == 2) { p_ledger_CRUD("Insert", transactionid, acctransid, "C", amount, id, "Customer", date, key, expenseid, "D", category_id); }

                }

            }
            return chk;
        }

        public DataTable getDates()
        {
            return db.p_getDates();
        }
        public bool p_insert_date(string date)
        {
            return db.p_insert_date(date);
        }

        public bool testConnection()
        {
            return db.ConnectionTesting();
        }

        public string getDBLive()
        {
            DataTable dt = db.p_accountCrud("DB",new Account());
            DataRow dr = dt.Rows[0];
            string dbt = dr[0].ToString();
            if (dbt == "")
                return "0";
            else
                return "1";

        }
        public void updateCategory(string id, string name, string key)
        {
            p_CategoryCreateDelete("Update", name, id,key);
        }

        public DataTable getCategory(string action,string search)
        {
            List<object> obj = (List<object>)db.p_Category_CRUD(action, search, "","");
            if (obj == null)
            {
                return null;
            }
            DataTable dt = (DataTable)obj[1];

            return dt;
        }

        public bool p_updateALLIDS()
        {
            return db.p_updateALLIDS();
        }

        public int p_CategoryCreateDelete(string action,string name,string id,string key)
        {
            List<object> obj = (List<object>)db.p_Category_CRUD(action, name, id,key);
            if (obj == null)
            {
                return 0;
            }
            int row = (int)obj[0];

            return row;
        }

        public bool passwordChange(string key, string oldpass, string newpass)
        {
            return db.passwordChange(key,oldpass,newpass);
        }

        public void p_pagesetting(string action,int labour, int rent, int munshiana,
            int bip_commission, int bip_laga, int cust_commission,int cust_chongi)
        {
            db.p_pagetSetting(action, labour, rent, munshiana, bip_commission,
                bip_laga, cust_commission, cust_chongi);
        }

        internal void addBalanceSheetAddBill(string inout, int update, 
            string uname, string type, string action, string key, string detail,string date,int amount,string account_transaction_id)
        {
            db.addAmountBalanceSheet(inout, update,uname,type,action,key,detail,date,amount,account_transaction_id);
        }

        public string accountActivation(string registration)
        {
            // Instanciating with base URL 
            try {  




                FirebaseDB firebaseDB = new FirebaseDB("https://arthiapp-5d72b-default-rtdb.firebaseio.com/test/");
                // Referring to Node with name "Teams"  
                 FirebaseDB firebaseDBTeams = firebaseDB.Node(registration);
                 FirebaseResponse getResponse = firebaseDBTeams.Get();
                 FirebaseResponse patchResponse = firebaseDBTeams
                     // Use of NodePath to refer path lnager than a single Node  
                     .Get();

                 if (getResponse.Success)
                 //WriteLine(patchResponse.JSONContent);
                 {
                     //var data = (JObject)JsonConvert.DeserializeObject();
                    // string name = data["name"].Value<string>();

                     return patchResponse.getJson();
                 }
                return "";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return "";
        }
        public DataTable p_cashflow_SP(string sdate, string ldate)
        {
            return db.p_cashflow_SP(sdate, ldate);

        }
        public DataTable readFardHisab(string type, string id, string sdate, string ldate)
        {
            return db.readFardHisab(type, id, sdate, ldate);
        }

        public void billPaidOut(string key, string clientid, string date, string amount,string desc)
        {
            db.p_BillingPayingDetail("AddBill",clientid,key,date,amount,desc);
        }

        public bool p_customer_Delete(string @action, string date, string name, 
            string key, int amount,int discount, int recID, string type, string category,string account_transaction_id,string category_id)
        {
            string[] chk = db.p_customer_CRUD(action, key, date, amount, 0, recID, 0, discount, 0, type, "");
            if (chk[0] == "false")
                return false;
            {
                //db.p_expense_CRUD("Delete", date, "", 0, key, new Expense(), category);
                //db.p_expensenew_CRUD("Delete", date, "", 0, key);

                db.addBalanceSheetExpense(name, "" + amount, date, nameof(BillKey.EnumUser.Customer), key, "credit", "deleted", "1", account_transaction_id, category_id);
                //db.addBalanceSheetExpense(name, "" + discount, date, nameof(BillKey.EnumUser.Discount), key, "credit", "deleted", "1");
                return true;
            }
            return false;
        }

        public void closeConnection()
        {
            db.CloseConnection(db.GetConnection());
        }

        public DataTable getCapitalCashIN(string key)
        {
            return db.getCapitalCash(key, "CapitalIn","","");
        }
        public string getCapitalPreviousDayCash(string key,string sdate,string ldate)
        {
            DataTable dt = db.getCapitalCash(key, "PreviousDayCash", sdate,ldate);
            string capital = "";
            foreach (DataRow dr in dt.Rows)
            {
                capital = dr[0].ToString();
            }
            if (capital == "")
            {
                capital = "0000";
            }
            return capital;
        }

        public Account check_User(Account account)
        {
            DataTable dt=new DBHandler().p_accountCrud("UserCheck", account);
            if (dt==null)
            {
                return null;
            }
            if (dt.Rows.Count>0)
            {
                Account acc = new Account();
                DataRow dr = dt.Rows[0];
                acc.shop_name = dr[0].ToString();
                acc.address = dr[1].ToString();
                acc.phone = dr[2].ToString();
                acc.propriters_name = dr[3].ToString();
                acc.username = dr[4].ToString();
                acc.email = dr[5].ToString();
                acc.password = dr[6].ToString();
                acc.license_no = dr[7].ToString();
                acc.license_exp_date = dr[8].ToString();
                acc.api_key = dr[9].ToString();
                acc.api_key_exp_date= dr[10].ToString();
                acc.debit = int.Parse(dr[11].ToString());
                acc.credit = int.Parse(dr[12].ToString());
                acc.capital_Cash = int.Parse(dr[13].ToString());
                acc.name1 = (dr[14].ToString());
                acc.phone1= (dr[15].ToString());
                acc.name2= (dr[16].ToString());
                acc.phone2=(dr[17].ToString());
                acc.role = (dr[18].ToString());
                acc.business_type = (dr[19].ToString());
                acc.local = (dr[20].ToString());
                acc.accountclosing = (dr[21].ToString());
                acc.trade_mark = dr[23].ToString();
                return acc;
            }else
            {
                return null;
            }

        }

        public DataTable p_today_totalDetails(string sdate,string ldate)
        {
            return db.p_today_totalDetails(sdate,ldate);
        }

        public Account accountActivationAdd(string regid,bool isEnable)
        {
            string json = accountActivation(regid);
            if (!string.IsNullOrEmpty(json) || json!=null)
            {
                try
                {


                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (dict == null)
                    {
                        return null;
                    }
                    Account acc = new Account();
                    acc.username = dict["name"];
                    acc.shop_name = dict.ContainsKey("trade_mark") ? dict["trade_mark"] : "";
                    acc.email = dict.ContainsKey("email") ? dict["email"] : "";
                    acc.phone = dict.ContainsKey("phone") ? dict["phone"] : "";
                    acc.password = dict.ContainsKey("password") ? dict["password"] : "";
                    acc.password = dict.ContainsKey("password") ? dict["password"] : "";
                    acc.propriters_name = dict.ContainsKey("propreietors") ? dict["propreietors"] : "";
                    acc.api_key = dict.ContainsKey("registrationid") ? regid : "";
                    acc.api_key_exp_date = dict.ContainsKey("registration_exp_date") ? dict["registration_exp_date"] : "";
                    acc.license_no = dict.ContainsKey("license") ? dict["license"] : "";
                    acc.license_exp_date = dict.ContainsKey("license_date") ? dict["license_date"] : "";
                    acc.address = dict.ContainsKey("address") ? dict["address"] : "";
                      


                    if (acc.api_key==regid)
                        return acc;
                    else
                        return null;
                
                }
                catch (JsonReaderException e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public string getLiveDB()
        {
            return db.getLiveDB();
        }
        public string getBackupLiveDB()
        {
            return db.getBackupLiveDB();
        }

        

        public bool account_update(Account acc,string action)
        {
            if (db.updateAccount(action, acc))
                return true;
            else
                return false;
        }
        public bool account_update_Local(Account acc)
        {
            if (db.updateAccount("Local", acc))
                return true;
            else
                return false;
        }
        public string getCapitalCash(string key)
        {
            DataTable dt = db.getCapitalCash(key, "Capital","","");
            string capital = "";
            foreach (DataRow dr in dt.Rows)
            {
                capital = dr[0].ToString();
            }
            if (capital=="")
            {
                capital = "0000";
            }
            return capital;
        }
        public string p_getInvoiceID(string action,string id,string date)
        {
            return db.p_getInvoiceID(action,id,date);
        }

        public DataTable getDashBoardCustSales(string @date)
        {
            return db.p_dashboard("CustSales", date,"","");
        }
        public DataTable getDashBoardSales(string @date)
        {
            return db.p_dashboard("Sales", date,"","");
        }
        public DataTable getDashboardSales20(string sdate,string ldate)
        {
            return db.p_dashboard("Sales30Days", "",sdate,ldate);
        }
        public DataTable getDashboardCash20(string sdate, string ldate)
        {
            return db.p_dashboard("Cash30Days", "", sdate, ldate);
        }

        #region Invoicing

        

        public CustomerSales readCustomerSale(string tbl,string key,string date,string type)
        {
            // type=All
            // type=Single
            db = new DBHandler();
            DataTable dt = null;
            if (type=="All")
            {
                dt=getp_customer_sale_CRUD(tbl, key, date);
            }else if (type=="Single")
            {
                dt = getp_customer_sale_CRUD(tbl, key, "");
            }


            return getCustomerSales(dt, date);
        }

        public List<Landlord> updateLocalToDB(string date, List<Landlord> data,bool isFreshData)
        {
            int j = 0;
            int i = 5;
            int naqdi = 0;
            

            #region Add Vendor Stock
            int bipCount = 0;
            for (j=0;j<data.Count;j++)
            {
                Landlord land = data[j];
                //if (!land.isRecordSaleInserted)
                {
                    naqdi += land.expense.total_expense;

                    string check = addClient_Landlord(land);// Insert sales first

                    if (check != "" && check != "key-dup")
                    {
                        bipCount++;
                        string key = i++ + date.Replace("-", "");
                        string desc =string.Format("{0}, {1}, {2}", land.land_person.pname, land.land_person.pname, (int)land.GetTotalService - land.expense.total_munshiana- land.expense.total_marketfee);

                        /*
                        db.addBalanceSheet("credit", 0, land, nameof(BillKey.EnumUser.Expense), "insert", land.bill_key, desc);

                        db.update_today_sales(date);// its update only expenses only in tblsale
                        addExpense_IUExpense(date,land.expense.category);// rent,labour and other epenses are updated
                        */
                        data[j].record_id = check;

                    }
                    else if(check== "key-dup")
                    {
                        data[j].land_person.pkey = p_getInvoiceID("Zam", data[j].land_person.pid, date);
                        j = j - 1;
                    }
                }
            }
            if (data.Count< bipCount)
            {
                return null;
            }
            #endregion
            #region Stock Buyed 
            foreach (Landlord landlord in data)
            {
                int cust_index = landlord.customers.Count;
                if (landlord.land_product.sale_remaining_product>0)
                {
                    landlord.status = EStatus.InComplete;

                }
                else
                {
                    landlord.status = EStatus.UnPaid;
                }
                if (isFreshData)
                    cust_index = 0;
                bool check = insertCustomerSale(landlord, cust_index);
                if (check)
                {
                    //db.p_addsaleclient("Update", landlord.date, landlord.land_person.pid, (int)landlord.GetGrandTotal);
                    new DBHandler().addSaleLandlord("updateClientAmount", landlord.date, 
                        landlord.land_person.pid,
                        (int)landlord.GetGrandTotal,
                        landlord.land_person.pkey,"",0);
                    new DBHandler().update_today_sales(date);
                    string desc = string.Format("{0}, {1}, {2}", landlord.land_person.pname, landlord.land_person.pname, (int)landlord.GetGrandTotal);
                    //addExpense_IUSales(landlord.date, landlord.land_person.pkey, landlord.land_person.pname, (int)landlord.GetGrandTotal, landlord.category);
                    //addBalanceSheet("credit", 0, landlord, landlord.category, "insert", landlord.land_person.pkey, desc);
                }
            }
            return data;
            #endregion

            /*


                            //if (!land.isRecordSaleInserted)
                            {
                                land.date = date;
                                bool checkSale = insertCustomerSale(land, j);
                                if (checkSale)
                                {

                                    db.p_expense_CRUD("SalesInsert", date, land.land_person.pname, (int)land.GetGrandTotal, land.land_person.pkey, new Expense());
                                    //db.p_expensenew_CRUD("SalesInsert", date, land.land_person.pname,(int)land.GetGrandTotal, land.land_person.pkey);
                                    db.addBalanceSheet("credit", 0, land, "bipari", "insert", land.land_person.pkey);
                                }
                            }
                        }
                        //db.p_update_daily_table_product(templandlord);
                        int count = 0;
                        if (db.p_customer_CRUD("SaleIn", "", date, 0, 0, 0, 0, 0, 1))
                        {
                            count++;

                        }
                        if (db.p_customer_sale_CRUD("Update", "", date, ""))
                        {
                            count++;
                        }
                        if (db.update_today_sales(date))
                        {
                            count++;
                        }
                        #endregion

                        if (count==3)
                        {
                            return data;

                        }*/
            return null;

            
        }

        public bool dataBackupMoveCreate(string action, string sdate, string ldate, string detail)
        {
            return db.dataBackupMove_Create(action, sdate, ldate, detail);
        }
        public DataTable dataBackupMove(string action, string sdate, string ldate, string detail)
        {
            return db.dataBackupMove(action,sdate,ldate,detail);
        }

        public bool updateStatusSale(string date,int billamount,string key, string category,int discount,string clid)
        {
            return db.addSaleLandlord("UpdateExpense", date, clid, billamount, key, category, discount);
        }

        public DataTable readLandlordDailySale(string sdate, string ldate, string id)
        {
            List<object> obj = (List<object>)new BLReport().p_ClientSaleDetail(sdate, ldate, id);
            if (obj == null)
            {
                return null;
            }
            DataTable dt = (DataTable)obj[1];
            return dt;
        }

        public object getCustomerSales(string action,string cust_id,string key,int pageIndex,int pageSize)
        {
            return db.p_customersbills_augrai(action,cust_id,key, pageIndex, pageSize);
        }
        public List<CustomerSales> getCustomerBills(string date)
        {
            try
            {

                //DataTable sales = (DataTable)db.getCustomerBills("Sales",date);
                DataTable dtc = (DataTable)db.p_customer_sale_record("Sales", date,"");




                List<CustomerSales> custs = new List<CustomerSales>();

                foreach (DataRow row in dtc.Rows)
                {
                    CustomerSales cs = new CustomerSales(date);
                    cs.person.pkey = row[0].ToString();
                    cs.person.pid = row[1].ToString();
                    cs.person.pname = row[2].ToString();
                    cs.total_quantity = int.Parse(row[3].ToString());
                    cs.total_sale = int.Parse(row[4].ToString());
                    
                    cs.Total_Commission = float.Parse(row[5].ToString());
                    cs.Total_Chongi = int.Parse(row[6].ToString());
                    cs.RemainingAmount= int.Parse(row[8].ToString());
                    custs.Add(cs);
                }
                return custs;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }

            return null;
        }

        public List<Customer> getCustomerBills(string date,bool isCustomerSale)
        {
            try
            {

                //DataTable sales = (DataTable)db.getCustomerBills("Sales",date);
                DataTable dtc = (DataTable)db.p_customer_sale_record("SaleDetail", date,"");




                List<Customer> custs = new List<Customer>();

                foreach (DataRow row in dtc.Rows)
                {
                    Services s = new Services();
                    s.commission_customer_product = float.Parse(row[11].ToString());
                    s.customer_chongi = int.Parse(row[12].ToString());

                    Customer cust = new Customer(date,s,true,new Sale(0,0),new Person());
                    cust.customer_profile.pkey = row[0].ToString();
                    cust.customer_profile.pid = row[1].ToString();
                    cust.customer_profile.pname = row[2].ToString();
                    cust.total_quantity = int.Parse(row[3].ToString());
                    cust.sale.add_extra_amount_Customer = int.Parse(row[9].ToString());
                    cust.sale.add_extra_amount_Landlord = int.Parse(row[10].ToString());
                    cust.sale._TotalSaleAmount = int.Parse(row[4].ToString());
                    
                    //MessageBox.Show("Change query from db");
                    if (isCustomerSale)
                    {
                        cust.GrandTotalCustomer = int.Parse(row[5].ToString());

                    }
                    else
                    {
                        cust.GrandTotalLandlord = int.Parse(row[5].ToString());

                    }
                    cust.Total_Commission = float.Parse(row[6].ToString());
                    cust.Total_Chongi = int.Parse(row[7].ToString());
                    cust.total_chalan = int.Parse(row[8].ToString());
                    
                    cust.date = date;

                    custs.Add(cust);
                }
                return custs;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }

            return null;
        }


        public List<Customer> getCustomerBillsByKey(string date, string key)
        {
            try
            {

                //DataTable sales = (DataTable)db.getCustomerBills("Sales",date);
                DataTable dtc = (DataTable)db.p_customer_sale_record("SaleDetailByKey", date, key);




                List<Customer> custs = new List<Customer>();

                foreach (DataRow row in dtc.Rows)
                {
                    Services s = new Services();
                    s.commission_customer_product = float.Parse(row[7].ToString());
                    s.customer_chongi = int.Parse(row[8].ToString());

                    Customer cust = new Customer(date, s, true, new Sale(0, 0), new Person());
                    cust.customer_profile.pkey = row[0].ToString();
                    cust.customer_profile.pid = row[1].ToString();
                    cust.customer_profile.pname = row[2].ToString();

                    cust.total_quantity = int.Parse(row[3].ToString());
                    cust.sale._TotalSaleAmount = int.Parse(row[4].ToString());

                    cust.Total_Commission = float.Parse(row[5].ToString());
                    cust.Total_Chongi = int.Parse(row[6].ToString());
                    cust.GrandTotalCustomer = int.Parse(row[7].ToString());

                    cust.sale.add_extra_amount_Customer = int.Parse(row[8].ToString());
                    cust.sale.add_extra_amount_Landlord = int.Parse(row[9].ToString());
                    cust.product._product_name = row[10].ToString();
                    cust.product._weight = row[11].ToString();


                    cust.date = date;

                    custs.Add(cust);
                }
                return custs;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }

            return null;
        }

        public bool deletecashAdmin(string date, string adminid, string cash,string key,string account_transaction_id,string category_id)
        {
            
            if (db.addBalanceSheetExpense("admin", cash, date, nameof(BillKey.EnumUser.Admin), adminid, "credit", "Deleted", "1", account_transaction_id,category_id))
            {
                int chk= db.p_cashamount_CRUD("DeleteCashById", date, adminid, cash, nameof(BillKey.EnumUser.Admin), nameof(BillKey.EnumUser.Admin), key) ;
                return chk>0 ? true : false;
            }
            return false;
        }

        public CustomerSales getCustomerSales(DataTable dt,string date)
        {
            if (dt==null)
            {
                MessageBox.Show("Data Not Found");
                return null;
            }
             CustomerSales cs = new CustomerSales(date);
             List<Customer> customers = new List<Customer>();
             if (dt.Rows.Count==0)
             {
                return null;
             }
            
            DataRow cr = dt.Rows[0];
            cs.person.pkey = cr[0].ToString();
            cs.person.pid = cr[1].ToString();
            cs.person.pname = cr[2].ToString();
            foreach (DataRow row in dt.Rows)
            {
                Sale sale = new Sale(int.Parse(row[5].ToString()), int.Parse(row[6].ToString()));
                Customer cust = new Customer(date,new Services(),true,sale,new Person());
                cust._LandlordProfile.pid = row[3].ToString();
                cust._LandlordProfile.pname = row[4].ToString();
                
                //cust.sale.GetTotalSale= int.Parse(row[7].ToString());
                cust.GrandTotalCustomer = int.Parse(row[8].ToString());
                cust.Total_Commission = float.Parse(row[9].ToString());
                cust.Total_Chongi = int.Parse(row[10].ToString());
                cust.product._product_name = row[11].ToString();
                cust.product._weight = row[12].ToString();
                cust._Services.commission_customer_product = float.Parse(row[13].ToString());
                cust._Services.customer_chongi = int.Parse(row[14].ToString());
                cust.sale.add_extra_amount_Customer= int.Parse(row[15].ToString());

                cs.total_quantity += cust.sale._sale_quantity;
                cs.total_chalan++;
                cs.Total_Commission += cust.Total_Commission;
                cs.Total_Chongi += cust.Total_Chongi;
                cust.isCustomerBill = true;
                cs.total_sale += (int)cust.sale.getTotalSale() + cust.sale.getTotalExtraAmountCustomer();
                customers.Add(cust);
            }
            cs.customers = customers;
            return cs;
        }

        public string checkCustSaleKeyExist(string customerid, string date)
        {
            return db.checkCustSaleKeyExist(date,customerid);
        }

        public bool p_sales_delete(string action, string date, string billkey)
        {
            return db.p_sale_delete(action,date,billkey);
        }
        public bool p_moveSaleDate(string action,string date, string moveto)
        {
            return db.p_moveSaleDate(action,date, moveto);
        }

        public DataTable getRecivedCash(string action,string date,string id,string key)
        {
            return db.getRecivedCash(action,date,id,key);
        }


        public DataTable getp_customer_sale_CRUD(string tbl, string key, string date)
        {
            return db.p_customer_sale_CRUD(tbl, key, date);
        }
        public DataTable getp_customer_sale_CRUD(string tbl, string key, string date,string id)
        {
            bool dbp_customer_sale_CRUD = db.p_customer_sale_CRUD(tbl, key, date, id);
            if (dbp_customer_sale_CRUD)
            {
                return new DataTable();
            }
            return null;
        }


        #endregion

        public DataTable p_maalList(string tdate) 
        {
            try
            {
                DataTable dbp_maalList = (DataTable)db.p_maalList(tdate);
                return dbp_maalList;

            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
            return null;


        }

        public string addExtraAmountClient()
        {
            return db.p_extra_amount("Client");
        }
        public string addExtraAmountCustomer()
        {
            return db.p_extra_amount("Customer");
        }

        public bool changeSaleStatus(string key,string status,string status_date,string refrence)
        {
            
            return db.p_update_bill_status(key, status, status_date, refrence);
        }

        public void addTodaySales(string date)
        {
            db.addTodaySales(date);
        }

        public Account check_User(string username, string password)
        {
            DataTable dt = db.check_User(username, password);
            if (dt.Rows.Count>0) {
                Account account = new Account();
                account.shop_name= dt.Rows[0].Field<string>(0);
                account.username = dt.Rows[0].Field<string>(1);
                account.api_key = dt.Rows[0].Field<string>(2);
                account.address = dt.Rows[0].Field<string>(3);
                account.propriters_name = dt.Rows[0].Field<string>(4);
                account.phone = dt.Rows[0].Field<string>(5);
                account.capital_Cash = dt.Rows[0].Field<int>(6);
                account.name1 = dt.Rows[0].Field<string>(7);
                account.name2= dt.Rows[0].Field<string>(8);
                account.phone1 = dt.Rows[0].Field<string>(9);
                account.phone2 = dt.Rows[0].Field<string>(10);
                account.license_exp_date = dt.Rows[0].Field<string>(11);
                account.license_no = dt.Rows[0].Field<string>(12);
                account.role = dt.Rows[0].Field<string>(13);
                account.business_type = dt.Rows[0].Field<string>(14);
                account.local =""+ dt.Rows[0].Field<int>(15);
                account.trade_mark = "" + dt.Rows[0].Field<string>(16);

                return account;
            }
            return null;
        }

        public bool addClient_Landlord(int objectIndex)
        {
            return db.addClient_Landlord(objectIndex);
        }
        public string addClient_Landlord(Landlord land)
        {
            return db.addClient_Landlord(land);
        }



        public void update_today_sales(string date)
        {
            db.update_today_sales(date);
        }

        public bool checkStatusofSale(string billkey)
        {
            return db.check_StatusDailySales(billkey);
        }

        /* public void p_expense_transport(Landlord land)
         {
             db.p_expense_transport(land);
         }*/

        public DataTable getTodayExpense(string date)
        {
            try
            {
                DataTable dt = (DataTable)db.getTodayExpense(date);
                return dt;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
            return null;
        }

        public DataTable getCashExpenseDetail(string startdate,string lastdate)
        {
            
            try
            {
                DataTable dt = (DataTable)db.p_report_cash_expense(startdate,lastdate);
                return dt;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
            return null;
        }

        public List<Customer> getCustomerBill(string date)
        {
            throw new NotImplementedException();
        }


        #region Sales Logic

        public Landlord getLandlordsListSingle(string date, string id)
        {
            this.db = new DBHandler();

            DataTable data_tbl = null;
            try
            {
                data_tbl = (DataTable)db.p_daily_CRUD("getLandlord", date, id);

                DataRow row = data_tbl.Rows[0];
                string _bill_id = row[0].ToString();
                string _date = row[15].ToString();
                string _vehicle_id = row[19].ToString();
                string _id = row[1].ToString();
                string _clientnameid = row[2].ToString();
                string _key = row[16].ToString();
                string _expense = row[14].ToString();
                string _customer_commission = row[20].ToString();
                string _customer_chongi = row[21].ToString();
                string _labourpp = row[22].ToString();
                string _client_chongi = row[24].ToString();
                string _client_commission = row[25].ToString();
                string _rent_per_product = row[26].ToString();
                string total_bipari_commission = row[27].ToString();
                string total_bipari_chongi = row[28].ToString();
                string total_sale_amount = row[29].ToString();
                string status = "";
                string status_date = "";
                string refrence = "";
                string product_marka = row[33].ToString();
                status = row[30].ToString();
                status_date = row[31].ToString();
                refrence = row[32].ToString();



                Services s = new Services();


                s.commission_client_product = float.Parse(_client_commission);
                s.commission_customer_product = float.Parse(_customer_commission);

                s.client_chongi = float.Parse(_client_chongi);
                s.customer_chongi = float.Parse(_customer_chongi);

                s.labour_per_product = float.Parse(_labourpp);
                s.rent_per_product = float.Parse(_rent_per_product);





                Client temp = new Client();

                temp._vehicle_id = _vehicle_id;
                temp.date = _date;
                temp.record_id = _bill_id;



                Person cl_person = new Person(_id, _key, _clientnameid, "", 0, int.Parse(_expense));
                temp._person_cl = cl_person;
                temp._services = s;





                
                        //        k=j;
                        string _ll_id = row[3].ToString();
                        string _landloardnameid = row[4].ToString(); ;
                        string _product_id = row[5].ToString();
                        string _product_name = row[6].ToString();
                        string _weight_id = row[7].ToString();
                        string _weight = row[8].ToString();
                        string _total_quantity = row[9].ToString();
                        string _total_rent = row[10].ToString();
                        string _total_labour = row[11].ToString();
                        string _total_munshiana = row[12].ToString();
                        string _advance = row[13].ToString();
                        string _ll_key = row[17].ToString();
                        string _type = row[18].ToString();
                        string _remaining_item = row[23].ToString();

                        s.clerk_per_bill = float.Parse(_total_munshiana);


                        Product product = new Product();
                        product._product_id = _product_id;
                        product._product_name = _product_name;
                        product._weight_id = _weight_id;
                        product._weight = _weight;
                        product._type = _type;
                        product.total_Quantity = int.Parse(_total_quantity);
                        product.sale_remaining_product = (_remaining_item == "") ? 0 : int.Parse(_remaining_item);
                        product.marka = product_marka;
                        temp._product = product;






                        Person landperson = new Person(_ll_id, _ll_key, _landloardnameid, "", int.Parse(_advance), 0);

                        Landlord landlord = new Landlord();
                        landlord.tag_Action = "insert";
                        landlord.record_id = _bill_id;
                        landlord.date = _date;
                        landlord.client = temp;
                        landlord.service = s;
                        landlord.land_person = landperson;
                        landlord.land_product = product;

                        Enum.TryParse<EStatus>(status, out landlord.status);

                        landlord.status_date = status_date;
                        landlord.bill_paid_to = refrence;
                        landlord.total_quantity = int.Parse(_total_quantity);

                        landlord.expense.total_rent = int.Parse(_total_rent);
                        landlord.expense.total_labour = int.Parse(_total_labour);

                        landlord.expense.total_munshiana = int.Parse(_total_munshiana);
                        if (total_bipari_commission != "")
                        {
                            landlord.Total_Commission = float.Parse(total_bipari_commission);

                        }
                        if (total_bipari_chongi != "")
                        {
                            landlord.Total_Chongi = int.Parse(total_bipari_chongi);

                        }
                        landlord.total_sale = int.Parse(total_sale_amount);
                        //landlord.isRecordSaleInserted = true;

                        DataTable dt_customer = (DataTable)db.getClient_Sales("ByID", "", _bill_id);
                        if (dt_customer.Rows.Count > 0)
                        {
                            landlord.customers = addCustomer(dt_customer, s, landlord);
                            if (landlord.customers != null)
                            {
                                if (landlord.customers.Count > 0)
                                {
                                    landlord.isRecordSaleInserted = true;
                                }
                                else
                                {
                                    landlord.isRecordSaleInserted = false;
                                }
                            }
                        }
                        else
                        {
                            landlord.customers = new List<Customer>();
                        }
                        landlord.total_sale = total_sale;




                    return landlord;//getLandlordsList(data_tbl);
            }
            catch (NullReferenceException e)
            {
                return null;
            }
            if (data_tbl == null)
            {
                return null;
            }
        }
        public List<Landlord> getLandlordsList(string date, string text)
        {
            this.db = new DBHandler();

            DataTable data_tbl = null;
            try
            {
                data_tbl = (DataTable)db.p_daily_CRUD("getLandlord", date, text);
                return getLandlordsList(data_tbl);
            }
            catch (NullReferenceException e)
            {
                return null;
            }
            if (data_tbl == null)
            {
                return null;
            }
        }
        public object getLandlordsList(string startdate,string lastdate, string search,int page,int pageSize)
        {
            this.db = new DBHandler();

            DataTable data_tbl = null;
            try
            {
                return new BLReport().getSalesLandlord(startdate, lastdate, page, pageSize, search);
            }
            catch (NullReferenceException e)
            {
                return null;
            }
            if (data_tbl == null)
            {
                return null;
            }
        }
        public List<Landlord> getLandlordsList(DataTable data_tbl)
        {

            List<Landlord> clients = new List<Landlord>();

            for (int k = 0; k < data_tbl.Rows.Count; k++)
            {
                DataRow row = data_tbl.Rows[k];
                string _bill_id = row[0].ToString();
                string _date = row[15].ToString();
                string _vehicle_id = row[19].ToString();
                string _id = row[1].ToString();
                string _clientnameid = row[2].ToString();
                string _key = row[16].ToString();
                string _expense = row[14].ToString();
                string _customer_commission = row[20].ToString();
                string _customer_chongi = row[21].ToString();
                string _labourpp = row[22].ToString();
                string _client_chongi = row[24].ToString();
                string _client_commission = row[25].ToString();
                string _rent_per_product = row[26].ToString();
                string total_bipari_commission = row[27].ToString();
                string total_bipari_chongi = row[28].ToString();
                string total_sale_amount = row[29].ToString();
                string status = "";
                string status_date = "";
                string refrence = "";
                string product_marka = row[33].ToString();
                string marketfee = row[34].ToString();
                string gtotal = row[35].ToString();
                string billtype= row[36].ToString();
                string bikri_rate = row[37].ToString();
                string bikri_quantity= row[38].ToString();

                status = row[30].ToString();
                status_date = row[31].ToString();

                refrence = row[32].ToString();



                Services s = new Services();


                s.commission_client_product = float.Parse(_client_commission);
                s.commission_customer_product = float.Parse(_customer_commission);

                s.client_chongi = float.Parse(_client_chongi);
                s.customer_chongi = float.Parse(_customer_chongi);

                s.labour_per_product = float.Parse(_labourpp);
                s.rent_per_product = float.Parse(_rent_per_product);





                Client temp = new Client();

                temp._vehicle_id = _vehicle_id;
                temp.date = _date;
                temp.record_id = _bill_id;



                Person cl_person = new Person(_id, _key, _clientnameid, "", 0, int.Parse(_expense));
                temp._person_cl = cl_person;
                temp._services = s;





                //for (int j= 0;j < data_tbl.Rows.Count;j++)
                {
                    //    DataRow row = data_tbl.Rows[j];

                    //    if (_id== row[1].ToString())
                    {
                        //        k=j;
                        string _ll_id = row[3].ToString();
                        string _landloardnameid = row[4].ToString(); ;
                        string _product_id = row[5].ToString();
                        string _product_name = row[6].ToString();
                        string _weight_id = row[7].ToString();
                        string _weight = row[8].ToString();
                        string _total_quantity = row[9].ToString();
                        string _total_rent = row[10].ToString();
                        string _total_labour = row[11].ToString();
                        string _total_munshiana = row[12].ToString();
                        string _advance = row[13].ToString();
                        string _ll_key = row[17].ToString();
                        string _type = row[18].ToString();
                        string _remaining_item = row[23].ToString();
                        string _total_marketfee = row[34].ToString();

                        s.clerk_per_bill = float.Parse(_total_munshiana);
                        s.marketfee = float.Parse(_total_marketfee);


                        Product product = new Product();
                        product._product_id = _product_id;
                        product._product_name = _product_name;
                        product._weight_id = _weight_id;
                        product._weight = _weight;
                        product._type = _type;
                        product.total_Quantity = int.Parse(_total_quantity);
                        product.sale_remaining_product = (_remaining_item=="")?0:int.Parse(_remaining_item);
                        product.marka = product_marka;
                        temp._product = product;






                        Person landperson = new Person(_ll_id, _ll_key, _landloardnameid, "", int.Parse(_advance), 0);

                        Landlord landlord = new Landlord();
                        landlord.tag_Action = "insert";
                        landlord.record_id = _bill_id;
                        landlord.date = _date;
                        landlord.client = temp;
                        landlord.service = s;
                        landlord.land_person = landperson;
                        landlord.land_product = product;
                        landlord.bill_type = billtype;
                        landlord.bikri_quantity = int.Parse(bikri_quantity==""? "0" : bikri_quantity);
                        landlord.bikri_rate = int.Parse(bikri_rate==""?"0":bikri_rate);

                        Enum.TryParse<EStatus>(status,out landlord.status);

                        landlord.status_date = status_date;
                        landlord.bill_paid_to = refrence;
                        landlord.total_quantity = int.Parse(_total_quantity);

                        landlord.expense.total_rent = int.Parse(_total_rent);
                        landlord.expense.total_labour = int.Parse(_total_labour);
                        landlord.expense.total_advance_amount = int.Parse(_advance);
                        landlord.expense.total_munshiana = int.Parse(_total_munshiana);
                        landlord.expense.total_marketfee = int.Parse(_total_marketfee);
                        if (total_bipari_commission != "")
                        {
                            landlord.Total_Commission = float.Parse(total_bipari_commission);

                        }
                        if (total_bipari_chongi != "")
                        {
                            landlord.Total_Chongi = int.Parse(total_bipari_chongi);

                        }

                        landlord.total_sale = int.Parse(total_sale_amount);
                        //landlord.isRecordSaleInserted = true;
                       
                        DataTable dt_customer = (DataTable)db.getClient_Sales("ByID","",_bill_id);
                        if (dt_customer.Rows.Count > 0)
                        {
                            landlord.customers = addCustomer(dt_customer,s,landlord);
                            if (landlord.customers!=null)
                            {
                                if (landlord.customers.Count>0)
                                {
                                    landlord.isRecordSaleInserted = true;
                                }
                                else
                                {
                                    landlord.isRecordSaleInserted = false;
                                }
                            }
                        }
                        else
                        {
                            landlord.customers = new List<Customer>();
                        }
                        landlord.total_sale = total_sale;
                        clients.Add(landlord);
                        total_sale = 0;


                    }
                }

            }
            return clients;
        }

        public bool updateNameCustomerSales(string date, string saleid, string newcustid)
        {
            //return false;
            return db.p_update_editcustomersales(date, saleid, newcustid);
        }

        public bool customersaleDelete(string _date, string _landkey, string custid,string custkey, string recid)
        {

            //MessageBox.Show("Delete Item Feature nOt Available");
            //return;
            return db.deleteSingleSale(_date, _landkey, custid,custkey, recid);
        }

        public bool addExtraAmountClient(string action,string date, string pid,
            int billAmount,string key,string name,int discount,string account_transaction_id)
        {

            if (db.addSaleLandlord(action, date, pid, billAmount, key, name, discount))
            {
                if (action == "updateClientAmount")
                    return true;
                addBalanceSheetAddBill("credit",0,name, nameof(BillKey.EnumUser.Client), 
                    "Insert",key,name,date,billAmount,account_transaction_id);
                //db.addBalanceSheetExpense(name, "" + billAmount, date,nameof(BillKey.EnumUser.Client), key, "credit", "Insert", "0");
                return true;
            }
            return false;
        }

        public bool p_customer_CRUD(string date, string billid, string id, string name
            , int cust_credit_amount, int amount, int discount, string type,
            string desc,string account_transaction_id,string category_id)
        {
            string[]  chk=db.p_customer_CRUD("CashIn", billid, date,
                        cust_credit_amount, int.Parse(id), 0, amount, discount, 0, type, desc);
            if (chk[0]=="true")
            {
                 db.addBalanceSheetExpense(name, "" + amount, date,
                nameof(BillKey.EnumUser.Client), string.Format("{0}-{1}",
                billid, chk[1]), "debit", "Insert", "0",account_transaction_id, category_id);
                if (discount > 0)
                {
                    db.addBalanceSheetExpense(name, "" + discount, date,
                        nameof(BillKey.EnumUser.Discount), string.Format("{0}-{1}", billid,
                        chk[1]), "debit", "Insert", "0",account_transaction_id, category_id);
                }
                return true ;
            }
            return false;
        }
        public bool addCustomerSales(string date, string billid, string custid, string name
            , int cust_credit_amount, int amount, int discount
            ,string type,string desc,string account_transaction_id,string category_id)
        {
            string[] cus=db.p_customer_CRUD("CashIn", billid, date,
                        cust_credit_amount, int.Parse(custid), 0, amount, discount,0,type, name+", "+desc);
            if (cus[0]=="false")
                return false;
            //in balance sheet we only add given amount
            bool chk=db.addBalanceSheetExpense(name + ", " + desc, "" + amount, date,
                nameof(BillKey.EnumUser.Customer), custid, "debit", "Insert", "0",
                account_transaction_id, category_id);

            // insert discount ,which is given
            /*if (discount > 0)
            {
                chk=db.addBalanceSheetExpense(desc, "" + discount, date,
                    nameof(BillKey.EnumUser.Discount), custid, "debit", "Insert", "0");
                return true;
            }*/
            return chk ;

        }

        public void p_weigt_CRUD(string tableName, string id, string name,string catid,string acc_catid)
        {
            db.p_weigt_CRUD(tableName, id, name,catid, acc_catid);
        }

        public DataRow getLastBalance(string custid, string sdate)
        {
            return db.getLastBlance_p_customer_sale_CRUD("GetLastBalanceCust","",sdate,custid);
        }

        public DataRow getLastCash(string sdate,string ldate)
        {
            return db.getLastBalance(sdate, ldate);
        }

        public bool deletExpenseShop(string billkey,string date,string category)
        {
            return db.p_expense_CRUD("Delete",date,"",0,billkey,new Expense(), category);
            //return db.p_expensenew_CRUD("Delete", date, "", 0, billkey);
        }

        public DataTable getClient_Sales(string action,string date,string _bill_id)
        {
            return (DataTable)db.getClient_Sales(action,date,_bill_id);
        }
        public DataTable getAllSales_ProfitDetail(string sdate,string ldate)
        {
            return (DataTable)db.p_all_sale_profit_details("Date", sdate, ldate);
        }
        

        public List<Customer> addCustomer(DataTable dt_customer,Services service,Landlord landlord)
        {
            List<Customer> cust_list = new List<Customer>();
            for (int i = 0; i < dt_customer.Rows.Count; i++)
            {
                DataRow cust_row = dt_customer.Rows[i];
                if (cust_row[0].ToString() == "")
                {

                }
                else
                {
                    Sale sale = new Sale(int.Parse(cust_row[1].ToString()), int.Parse(cust_row[2].ToString()));
                    Customer c = new Customer(landlord.date,service, false,sale,landlord.land_person);
                    c.cust_bill_id = cust_row[2].ToString();
                    c.tag_Action = "insert";
                    c.cust_bill_id = cust_row[0].ToString();
                    //c.product_name = landlord.land_product._product_name;
                    //c.product_packing = landlord.land_product._type;
                    c.product._product_name = cust_row[12].ToString();
                    c.product._weight = cust_row[14].ToString();
                    c.product.marka = cust_row[21].ToString();
                    //c.sale._sale_quantity = int.Parse(cust_row[1].ToString());
                    //c.sale._sale_amount = int.Parse(cust_row[2].ToString());
                    c.sale.add_extra_amount_Customer = int.Parse(cust_row[17].ToString());
                    c.sale.add_extra_amount_Landlord = int.Parse(cust_row[18].ToString());
                    c.sale._TotalExtraAmountCustomer = int.Parse(cust_row[19].ToString());
                    c.sale._TotalExtraAmountLandlord = int.Parse(cust_row[20].ToString());
                    //c.sale.GetTotalSale = int.Parse(cust_row[3].ToString());
                    string gtotal = cust_row[4].ToString();
                    if (gtotal == "")
                        c.GrandTotalLandlord = 0;
                    else
                        c.GrandTotalLandlord = int.Parse(gtotal);

                    c.total_sale = int.Parse(cust_row[3].ToString());
                    c.Total_Commission = float.Parse(cust_row[5].ToString());
                    c.Total_Chongi = int.Parse(cust_row[6].ToString());

                    c.customer_profile.pid = cust_row[9].ToString();
                    c.customer_profile.pkey = cust_row[15].ToString();
                    c.customer_profile.pname = cust_row[10].ToString();
                    c._LandlordProfile = landlord.land_person;
                    c.product = landlord.land_product;
                    c._Services = landlord.service;
                    c.cust_bill_id= cust_row[22].ToString();
                    c.status = cust_row[23].ToString();
                    cust_list.Add(c);

                    total_sale += (int)c.total_sale + c.sale._TotalExtraAmountLandlord ;
                }
            }
            return cust_list;
        }

        public bool p_changeLandlordName(string date, string landkey, string landid)
        {
            return new DBHandler().p_changeLandlordName(date,landkey,landid);
        }

        public void changeSaleDelete(string date, string landkey, string cust_bill_id, string custid, int delquantity, int delrate, int totsale)
        {
            throw new NotImplementedException();
        }

        private int total_sale;
        public DataTable salesDisplay(string action,string sdate,string ldate,string search,string key)
        {
            return db.salesDisplay(action,sdate,ldate,search,key);
        }

        public DataTable searchRecords(string date, string action, string search, int pageIndex, int PageSize)
        {
            try
            {
                DataTable dt = (DataTable)db.searchRecords(date, action, search,pageIndex,PageSize);
                return dt;
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
            return null;
        }
        public object searchProfile(string date, string check, string search, int pageIndex, int PageSize)
        {
            try
            {
                return db.searchRecords(date, check, search, pageIndex, PageSize);
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.StackTrace);
            }
            return null;
        }
        public string[] p_singlesaleadd(Landlord landlord, Customer customer)
        {
            return db.p_singlesaleadd(customer, landlord.service, landlord.land_product, landlord.land_person, landlord.date, landlord.record_id,"0");

        }

        public bool insertCustomerSale(Landlord templandlord, int cust_index)
        {
            //return false;
            bool check = false;
            int q=templandlord.land_product.total_Quantity, 
                r = templandlord.land_product.sale_remaining_product;
            
            if (new DBHandler().insertCustomerSale(templandlord, cust_index))
            {
                new DBHandler().p_update_daily_table_product(templandlord);
                new DBHandler().update_today_sales(templandlord.date);
                check = true;
            }

            return check;
        }
        public bool updateDailyRecord(Landlord templandlord)
        {
            bool chk = new DBHandler().p_update_salesexpenses(templandlord);
            if (!chk) { return false; }
            return new DBHandler().p_update_daily_table_product(templandlord);
        }

        public DataTable searchByDate(string startdate, string lastdate)
        {
            return db.searchBillHistory("", "Date", startdate, lastdate);
        }

        public DataTable searchByID(string search, string startdate, string lastdate)
        {
            return db.searchBillHistory(search, "ID", startdate, lastdate);
        }
        public DataTable searchByName(string search, string startdate, string lastdate)
        {
            return db.searchBillHistory(search, "Name", startdate, lastdate);
        }
        public DataTable searchByBillID(string search,  string startdate, string lastdate)
        {
            return db.searchBillHistory(search, "BillID", startdate, lastdate);
        }
        public void addOldCustClSales(string @date, string @key, string @name, int amount, string category)
        {
            db.p_expense_CRUD("InsertOldCC", date, name, amount, key, new Expense(), category);
            //db.p_expensenew_CRUD("SalesInsert", date,name,amount, key);

        }
        public void addExpense_IUSales(string @date, string @key,string @name,int amount,string category)
        {
            db.p_expense_CRUD("SalesInsert", date,name,amount, key, new Expense(),category);
            //db.p_expensenew_CRUD("SalesInsert", date,name,amount, key);

        }
        public void addExpense_Discount(string @date, string @key, string @name, int amount,string category)
        {
            db.p_expense_CRUD("Discount", date, name, amount, key, new Expense(), category);
            //db.p_expensenew_CRUD("Discount", date, name, amount, key);

        }
        public void delete_Expense(string @action, string @date, string @billkey, Expense @expense,string category)
        {
            db.p_expense_CRUD(action, date,"",0, billkey, expense, category);
            //db.p_expensenew_CRUD(action, date, "", 0, billkey);
        }
        public void addBalanceSheet(string cr_db, int update, Landlord land, string billtype,
            string action, string key,int oldAmount,string desc,string account_transaction_id)
        {
            db.addBalanceSheet(cr_db, update, land, billtype, action, key,oldAmount,desc, account_transaction_id);
        }
        public void addBalanceSheet(string cr_db, int update, Landlord land,
            string billtype, string action,string key,string desc,string account_transaction_id)
        {
            db.addBalanceSheet(cr_db, update, land, billtype, action,key,desc, account_transaction_id);
        }

        public bool p_update_customerbill(string action, string date, string pkey, string pid, int gTotal,string id)
        {
            return db.p_update_customerbill(action, date, pkey, pid, gTotal,id);
        }
        
        public bool updateCusomerAmountandBalanceShet(string date, string clKey, string custKey, string pid,int check,string custbillid)
        {
            return db.updateCusomerAmountandBalanceShet(date, clKey, custKey, pid,check, custbillid);
        }

        public void addBalanceSheetExpense(string expensename,string total_amount,
            string date,string type,string key, string inout, string crud_action,
            string update,string account_transaction_id,string category_id)
        {
            db.addBalanceSheetExpense(expensename, total_amount, date,
                type,key,inout,crud_action,update, account_transaction_id, category_id) ;
        }
        public int insertDataCPW(int v, string txt)
        {
            return db.insertDataCPW(v, txt,"");
        }
        public int insertDataCPW(int v, string txt,string address)
        {
            return db.insertDataCPW(v, txt, address);
        }

        public bool p_ud_cust_sale_product(string bill_key, Landlord land,string category)
        {
            int count = land.customers.Count;
            for (int i = 0; i < land.customers.Count; i++)
            {
                Customer cust = land.customers[i];
                if (this.db.p_ud_cust_sale_product(bill_key, land.land_person.pid, cust.customer_profile.pid, land.date, cust.sale._sale_quantity, 1, category))
                {
                    getp_customer_sale_CRUD("Delete", cust.customer_profile.pkey, land.date,cust.customer_profile.pid);
                    
                    count--;

                }
            }

            bool chk = false;
            if (count == 0)
            {
                db.updateClientAugrai(land.land_person.pid,(int)land.GetGrandTotal);
                db.update_today_sales(land.date);
                chk = true;
            }
            return chk;
        }


        #endregion


        public void addExpenseName(string expense)
        {
            db.addExpenseName(expense);
        }

        public bool insertTodayExpense(string date, string expense, string amount, 
            string refnum, string expense_loc,string type,string id,string accid,string detail,string trid,string cateid,string expenseid)
        {
            return db.insertTodayExpense(date,expense,amount, refnum, expense_loc,type,id, accid,detail, trid,cateid, expenseid);
        }

        public AutoCompleteStringCollection autoCompleteData()
        {
            try{
                AutoCompleteStringCollection auto= db.autoCompleteData();
                return auto;
            }
            catch(NullReferenceException e)
            {
                Console.Write(e.StackTrace);
                return null;
            }

            return null;
        }
        
        public bool delete_DailyMaal(string pid, string date, string type)
        {
            return db.delete_DailyMaal(pid, date, type);
        }

      
        public void makeCustomerBill()
        {

            /*foreach (Landlord land in Admin.GetInstance.clients)
            {
                List<Customer> customers = land.customers;
                foreach (Customer cust in customers)
                {
                    if (insertCustomerifnotExist(cust.customer_profile.pkey))
                    {
                        TotalSale tsale = calculateCustomerBill(cust.customer_profile.pkey, customers);
                        if (tsale != null)
                        {
                            db.updateCustomerBill(cust, tsale);
                        }
                    }
                }
            }*/




            
        }

        /*private bool insertCustomerifnotExist(string @key)
        {
            return db.insertCustomerifnotExist(@key);
        }*/

        // when customer bill delete or insert update customer bill
        public TotalSale calculateCustomerBill(string @cust_billkey, List<Customer> @customers)
        {
            TotalSale tot = new TotalSale();
            foreach (Customer cust in @customers)
            {
                if (@cust_billkey==cust.customer_profile.pkey)
                {
                    tot.total_quantity += cust.sale._sale_quantity;
                    tot.total_sale +=(int) cust.sale._TotalSaleAmount;
                    tot.total_chalan++;
                    tot.Total_Commission += cust.Total_Commission;
                    tot.Total_Chongi += cust.Total_Chongi;


                }
            }

            return tot;
        }

        public void customer_sale_add(string date)
        {
            db.customer_sale_add(date);
        }

        #region Daily Expense
       
        public Expense getExpenseLable()
        {

            DataTable labels = db.getExpenseLabels();
            DataRow row = labels.Rows[0];
            Expense expense = new Expense();
            expense.extra_amount_name = row[0].ToString();
            expense.rent_name = row[1].ToString();
            expense.labour_name = row[2].ToString();
            expense.clerk_name = row[3].ToString();
            expense.advance = row[4].ToString();
            return expense;
        }
        public void addExpense_IUExpense(string date,string category)
        {
            // get Labels of expense
            DataTable labels = new DBHandler().getExpenseLabels();
            Expense expense =getTotalExpense(date);
            if (expense==null)
            {
                return;
            }
            DataRow row = labels.Rows[0];
            expense.extra_amount_name = row[0].ToString();
            expense.rent_name = row[1].ToString();
            expense.labour_name = row[2].ToString();
            expense.clerk_name = row[3].ToString();
            expense.advance = row[4].ToString();

            new DBHandler().p_expense_CRUD("IUExpense", date,"",0, "" , expense, category);
            //db.p_expensenew_CRUD("IUExpense", date,"",0, "");
            




        }
        public Expense getTotalExpense(string date)
        {
            
            DataTable dt_expenses = (DataTable)db.p_daily_CRUD("TotalSale", date, "");
            if (dt_expenses.Rows.Count > 0)
            { 
                Expense expense = new Expense();

                foreach (DataRow row_expense in dt_expenses.Rows)
                {
                    if (row_expense[0].ToString()==""
                        && row_expense[1].ToString()==""
                        && row_expense[2].ToString() == ""
                        && row_expense[3].ToString() == ""
                        && row_expense[4].ToString() == "")
                    {
                        return expense;
                    }

                    expense.total_expense = int.Parse(row_expense[0].ToString());
                    expense.total_rent = int.Parse(row_expense[1].ToString());
                    expense.total_labour = int.Parse(row_expense[2].ToString());
                    expense.total_munshiana = int.Parse(row_expense[3].ToString());
                    expense.total_advance_amount = int.Parse(row_expense[4].ToString());
                }
                
                return expense;
            }
            
            return null;
        }

#endregion

        public DataTable getClient_TodayRent_Total(string date)
        {
            return (DataTable)db.p_daily_CRUD("getServices", date,"");
        }
        public DataTable getp_DailyCRUD(string @action,string @date,string @text)
        {
            return (DataTable)db.p_daily_CRUD(action, date, text);
        }

        public DataTable getTodayMaalAmad(string date)
        {
            return (DataTable)db.getTodayMaalAmad(date);
        }

        

       public int deleteRecordTransport(string billkey,string date,Landlord land,string category,string account_transaction_id)
        {

           bool check= p_ud_cust_sale_product(billkey, land, category);
            if (!check)
                return 0;
            int chk = db.deleteRecord(billkey, date, "Maal");

            if (chk!= 0)
            {
                addExpense_IUExpense(land.date, category);// rent,labour and other epenses are updated
                //call_CUTA(land);
                db.update_today_sales(land.date);
                db.p_update_daily_table_product(land);
                db.p_expense_CRUD("Delete", date, "", 0, billkey, new Expense(), category);
                //db.p_expensenew_CRUD("Delete", date, "", 0, billkey);
                string key = date.Replace("-", "");
                
                db.addBalanceSheet("debit", 1, land, nameof(BillKey.EnumUser.Expense), "deleted", land.land_person.pkey,"", account_transaction_id);
                if(land.customers!=null && land.customers.Count>0)
                    db.addBalanceSheet("debit", 1, land, nameof(BillKey.EnumUser.Client), "deleted", land.land_person.pkey,"", account_transaction_id);
                db.update_today_sales(date);

            }


            return chk;
        }
        public int deleteRecordTransport(string billkey, string date, Landlord land, int count,string category)
        {
            
            int chk = 0;
            if (count > 0)
            {
                for (int i = 0; i < land.customers.Count; i++)
                {
                    Customer cust = land.customers[i];
                    bool dbp_ud_cust_sale_product = this.p_ud_cust_sale_product(billkey,land, category);
                    //if (dbp_ud_cust_sale_product)
                    {
                        db.p_customer_sale_CRUD("Delete", cust.customer_profile.pkey, land.date);
                        count--;

                    }
                }
            }

                //bool chk = this.db.delete_DailyMaal(id, "", "CustSale");
            if (count == 0)
            {
                //addExpense_IUExpense(land.date, category);// rent,labour and other epenses are updated
                //call_CUTA(land);
                db.update_today_sales(land.date);
                db.p_update_daily_table_product(land);
                //db.p_expense_CRUD("Delete", date,"",0, billkey, new Expense(), category);
                //db.p_expensenew_CRUD("Delete", date, "", 0, billkey);
                string key = date.Replace("-","");
                //db.addBalanceSheet("debit", 1, land, nameof(BillKey.EnumUser.Client), "deleted",land.land_person.pkey,"");


                db.addSaleLandlord("DeleteSale", date,land.land_person.pid,
                    (int)land.GetGrandTotal,land.bill_key,land.land_person.pname,0);

                db.update_today_sales(date);

            }

           // db.addBalanceSheet("debit", 1, land, nameof(BillKey.EnumUser.Expense), "deleted", land.land_person.pkey,"");

            chk = db.deleteRecord(billkey, date, "Maal");

            
            return chk;
        }

        public bool p_customer_CRUD(string @table_name, string @key, string @date, int @bill_amount
            , int @cust_id, int @cash_rec_id, int @cashinout, int @discount,string type,string desc)
        {
            string[] chk = db.p_customer_CRUD(table_name, key, date, bill_amount, cust_id,
                cash_rec_id, cashinout, discount, 0, type, desc);
            if (chk[0] == "true")
                return true;
            else
                return false;
        }
        public bool p_customer_CRUD(string @action, string date, string name,
            string key,int amount,int recID,string type,string category,string account_transaction_id,string cateid)
        {
            string[] chk= db.p_customer_CRUD(action, key, date, amount, 0, recID, 0,0,0,type,"");
            if (chk[0] == "false")
                return false;
            {
                db.p_expense_CRUD("Delete", date, "", 0, key, new Expense(), category);
                //db.p_expensenew_CRUD("Delete", date, "", 0, key);
                db.addBalanceSheetExpense(name,""+amount,date, nameof(BillKey.EnumUser.Customer), key,"credit","deleted","1", account_transaction_id, cateid);
                return true;
            }
            return false;
        }

        public bool delete_CashRecived(string action, string date, string name, string key, 
            int amount,string type,string account_transaction_id,string cateid)
        {
            string[] chk = db.p_customer_CRUD(action, key, date, amount, 0, 0, 0, 0, 0,type,"");
            if (chk[0] == "false")
                return false;

            bool chk1 = db.p_customer_sale_CRUD("Update", key, date, "0");
           // bool chk = db.p_cashreceiving_CRUD("Delete", key) ? db.p_customer_sale_CRUD("Update", key, date, "0") : false;


            {
                db.addBalanceSheetExpense(name, "" + amount, date, nameof(BillKey.EnumUser.Customer), 
                    key, "credit", "deleted", "1", account_transaction_id,cateid);
                return true;
            }
            return false;
        }


        public DataTable p_customer_CRUD(string action)
        {
            return db.p_customer_CRUD(action,"","");
        }
        public DataTable p_customer_CRUD(string action,string printall,string date)
        {
            return db.p_customer_CRUD(action, printall,date);
        }
        public DataTable p_customer_AugraiDiff(string action,string name)
        {
            return db.p_customer_CRUD(action,name,"");
        }

        public DataTable p_report_CustomerClient(string action,string sdate,string ldate)
        {
            return db.p_report_CustomerClient("",sdate,ldate,action);
        }
        public DataTable p_report_CustomerClient(string action, string cl_id, string sdate, string ldate)
        {
            return db.p_report_CustomerClient(cl_id,sdate, ldate, action);
        }
        public bool updateExtraAmount(Landlord templandlord, Customer customer,string action)
        {
            
            if (action=="Client")
            {
                return db.p_daily_update_extraAmount("Client", 
                    templandlord.date,templandlord.land_person.pkey,
                    customer.customer_profile.pid,templandlord.land_product.total_Quantity,
                    customer.sale.add_extra_amount_Landlord,customer.sale._TotalExtraAmountLandlord,
                    templandlord.GetTotalSaleLandLord,
                    (int)templandlord.GetGrandTotal,(int)customer.Total_Commission);
            }
            else if (action=="Customer")
            {
                return db.p_daily_update_extraAmount("Customer",
                    templandlord.date, templandlord.land_person.pkey,
                    customer.customer_profile.pid, templandlord.land_product.total_Quantity,
                    customer.sale.add_extra_amount_Customer, 
                    customer.sale._TotalExtraAmountCustomer,
                    customer.sale._TotalSaleAmount,
                    customer.GrandTotalLandlord, (int)customer.Total_Commission);
            }
            
            return false;

        }
        ///*********************P_Shop_Sales*****************************///
        /// Methods call p_shop_sales_daily 
        /// Type OF Actions Perform 
        /// I=Insert,R=Read 2 months record,RALL=Read All Record
        /// D=Delete,U=Update,RU=Distinct user

        #region p_shop_sales_daily
        public object shopCrud_InsertUpdate(string action, string sdate, string ldate
       , string name, string userid,
       string quantity, string rate, string size, string product
       , string t_date, string total_amount, string ispaid, int sort, int record_id, int labour,string productid)
        {
            List<object> obj = (List<object>)db.p_shop_sales_crud(action, sdate, ldate, name, userid, quantity
                , rate, size, product, t_date, total_amount, ispaid, sort, record_id, labour, productid);
            return obj;
            /*if (obj == null)
            {
                return -1;
            }
            int chk = (int)obj[0];
            return chk;*/
        }
        public DataTable readShopSales(string uid, string sdate, string ldate, string date, int sort, int ispaid, int record_id)
        {
            return new DBHandler().shopSalesRead("R", sdate, ldate, "", uid, "", "", "", "", date, "", ispaid, sort, record_id);
        }

        #endregion
        ///*****************************************************************///

    }
}
