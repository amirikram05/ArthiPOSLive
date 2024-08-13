using DAL;
using DataMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataMember.BillKey;

namespace BAL
{
    public class ProfilesBL
    {
        public ProfilesDB pdb;
        public ProfilesBL()
        {
            pdb = new ProfilesDB();
        }
        public bool delete_CC(string tbl,string val)
        {
            return pdb.p_profile_CRUD("Delete",tbl,int.Parse(val),"","","","",0,"","","","");
        }
        public bool insert_CC_OldRecord(string tbl, string text1, string text2, string text3, string text4, int oldamount,string date)
        {
            return pdb.p_profile_CRUD("OldInsert", tbl, 0, text1, text2, text3, text4, oldamount, "",date, "","");
        }
       
        public bool insert_CC(string tbl, string text1, string text2, string text3, string text4, int text5)
        {
            return pdb.p_profile_CRUD("Insert",tbl,0, text1, text2, text3, text4,text5,"","","","");
        }
        /* public bool insert_CC(string tbl,string text1)
         {
             return pdb.insert_CC(tbl, text1);
         }*/
        public bool insert_oldRecord(string action,string id, string name, string date, int amount,string address)
        {
            return pdb.p_old_reacord(action, id,name,date,amount, address);
        }
        public bool update_CC(string tbl, int iD, string text1, string text2, string text3, string text4,string amount,string type)
        {
            return pdb.p_profile_CRUD("Update", tbl, iD, text1, text2, text3, text4, int.Parse(amount), "","","",type);
        }
        public bool updateAddAmount(string tbl,string key, int iD, string text1, 
            string text2, string text3, string text4,int amount,string date,string detail,string type)
        {
            if (key=="")
            {
                return false;
            }
            bool chk = false;
            if (pdb.p_profile_CRUD("AddClAmount", tbl, iD, text1, text2, text3, text4, amount,key,date,detail,type))
            {
                //investmentInsert(""+iD, clientkey, amount, date);
                pdb.addBalanceSheetExpense(text1, "" + amount, date,
                    nameof(BillKey.EnumUser.ClientInvest), key, "credit", "Insert", "0");
                chk = true;
            }
            return chk;
        }

        

        public bool AddClReceiveAmount(string action, string tbl, int id, string uname, string cname, string cphone, 
            string caddress, int amount,string date,string key,string detail,string type,int disocunt,string directionForm)
        {
            bool chk = false;
            BLogic bal = new BLogic();
            //string skey = bal.p_getInvoiceID("Zam");

            //if (bal.addExtraAmountClient("PaidOutAmount", date, ""+id, amount, skey, uname, disocunt))

            if (directionForm == "Admin")
            {
                if (pdb.p_profile_CRUD("AddClReceiveAmountAdvance", tbl, id, uname, cname, cphone, caddress, amount
                , key, date, detail, type))
                {
                    pdb.addBalanceSheetExpense(detail, "" + amount, date,
                            nameof(BillKey.EnumUser.ClientInvest), key, "credit", "Insert", "0");
                    chk = true;
                }

            }
            else
            {
                if (pdb.p_profile_CRUD("AddClReceiveAmount", tbl, id, uname, cname, cphone, caddress, amount
               , key, date, detail, type))
                {
                    pdb.addBalanceSheetExpense(detail, "" + amount, date,
                            nameof(BillKey.EnumUser.ClientInvest), key, "debit", "Insert", "0");
                    chk = true;
                }
            }


             
            return chk;
        }
        public bool AddClAmount(string action, string tbl, int id, string uname, string cname, string cphone, string caddress, int amount, string date, string key,string detail,string type)
        {
            bool chk = false;
            BLogic bal = new BLogic();

            if (pdb.p_profile_CRUD("AddClAmount", tbl, id, uname,
                cname, cphone, caddress, amount, key, date,detail,type))
            {
                //investmentReceive(clientkey, ""+ id, amount, date);

                //pdb.addBalanceSheetExpense(uname, "" + amount, date,"client", key, "credit", "Insert", "0");
                chk = true;
            }
            return chk;
        }

        public DataTable getCC(string tbl)
        {
            return (DataTable)pdb.p_profile_CRUD("Update", tbl);
        }

        public bool investmentInsert(string id,string key,int amount,string amount_date)
        {
            
            return pdb.p_investment("Insert",key, id, amount, amount_date, 0, "");
        }
        public bool investmentReceive(string key,string id, int receive_amount, string receive_date)
        {
            return pdb.p_investment("Receive",key, id,0, "", receive_amount, receive_date);
        }

        private bool p_product_CRUD(string @action,string @Code, string @UName, string @EName, string @Freight,
            string @Labour, string @Commi, string @Pcode, string @Pack, string @Commi1, string @Location, string @Laga, string @Chongi,string @Munshiana,string @MarketFee)
        {
            return pdb.p_product_CRUD(action,@Code, @UName, @EName, @Freight, @Labour, @Commi, @Pcode, @Pack, @Commi1,@Location, @Laga, @Chongi,@Munshiana, @MarketFee);
        }

        public bool p_product_Delete(string code)
        {
            return p_product_CRUD("DELETE",code,"","","","","","","","","","","","","");
        }
        public bool p_product_Update(string @Code, string @UName, string @EName, string @Freight,
            string @Labour, string @Commi, string @Pcode, string @Pack,
            string @Commi1, string @Location, string @Laga, string @Chongi, string @Munshiana, string @marketFee)
        {
            return p_product_CRUD("UPDATE", @Code, @UName, @EName, @Freight, @Labour, @Commi,@Pcode, @Pack, @Commi1,@Location, @Laga, @Chongi,@Munshiana, @marketFee);
        }
        public bool p_product_Insert(string @Code, string @UName, string @EName, string @Freight,
            string @Labour, string @Commi, string @Pcode, string @Pack,
            string @Commi1,string @Location,string @Laga,string @Chongi,string @Munshiana,string @marketFee)
        {
            return p_product_CRUD("INSERT", @Code, @UName, @EName, @Freight, @Labour, @Commi, @Pcode, @Pack, @Commi1,@Location,Laga,@Chongi, @Munshiana, @marketFee);
        }

        public bool p_weight_Delete(string code)
        {
            return p_weight_CRUD("DELETE", code, "", "");
        }
        public bool p_weight_Update(string @Code, string @UName, string @EName)
        {
            return p_weight_CRUD("UPDATE", @Code, @UName, @EName);
        }
        public bool p_weight_Insert(string @Code, string @UName, string @EName)
        {
            return p_weight_CRUD("INSERT", @Code, @UName, @EName);
        }
        private bool p_weight_CRUD(string action,string code,string uname,string ename)
        {
            return pdb.p_weigt_CRUD(action,code,uname,ename);
        }

        
    }
}
