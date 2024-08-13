using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace DataMember
{
    public class BillKey
    {

        public enum EnumUser
        {
            Client, Customer, LandLoard, ClientInvest, ClientInvestRec, CustInvest,
            Admin, Cash, PaymentSale, FoodUtillity, Expense,
            Discount,
            Shop,PaidOut
        }

        
        
        public static EnumUser e_User = EnumUser.Client;
        // return its first 20 values (= 40 characters) as a final result
        public static string ReturnUniqueValue(DateTime date, string ID)
        {
            var result = default(byte[]);

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(date.Ticks);
                    writer.Write(ID);
                }

                stream.Position = 0;

                using (var hash = SHA256.Create())
                {
                    result = hash.ComputeHash(stream);
                }
            }

            var text = new string[20];

            for (var i = 0; i < text.Length; i++)
            {
                text[i] = result[i].ToString("x2");
            }

            return string.Concat(text);
        }
        public static string getDateKey(string date)
        {
            
            return date.Replace("-", "");
        }
        public static string getDB_BillID(string catid, string date, string userid, int multiplebill_id)
        {
            return string.Format("{0}{1}{2}{3}",catid, date, userid, multiplebill_id);
        }
        public static string getBillID(EnumUser euser, string date, string userid, int multiplebill_id)
        {
            //string ms = DateTime.Now.ToString("mmss");
            string cdate = date.Replace("-", "");
            if (EnumUser.Client == euser)
            {
                return string.Format("1{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.LandLoard == euser)
            {
                return string.Format("2{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Customer == euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.CustInvest == euser)
            {
                return string.Format("4{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.ClientInvest == euser)
            {
                return string.Format("5{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Admin == euser)
            {
                return string.Format("Ad{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Cash == euser)
            {
                return string.Format("C{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Shop == euser)
            {
                return string.Format("S{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.PaymentSale == euser)
            {
                return string.Format("PS{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.FoodUtillity == euser)
            {
                return string.Format("FU{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else
            {
                string type = nameof(euser);
                return string.Format("{0}{1}{2}{3}", (type[0]+ type[1]).ToString().ToUpper(), cdate, userid, multiplebill_id);
            }
            /*string id = new BLogic().p_getInvoiceID()+ multiplebill_id;
            return id;*/
            return "";
        }
        /*public enum EnumUser
        {
            Client,Customer,LandLoard, ClientInvest,CustInvest,
            Admin,Cash
        }
        public static EnumUser e_User = EnumUser.Client;
        public static string getDateKey(string date)
        {
            return date.Replace("-", "");
        }
        public static string getBillID(EnumUser euser,string date,string userid,int multiplebill_id)
        {
            //string ms = DateTime.Now.ToString("mmss");
            string cdate = date.Replace("-","");
            if(EnumUser.Client== euser)
            {
                return string.Format("1{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if(EnumUser.LandLoard == euser)
            {
                return string.Format("2{0}{1}{2}", cdate, userid, multiplebill_id);
            }else if (EnumUser.Customer==euser)
            {
                return string.Format("3{0}{1}{2}", cdate, userid, multiplebill_id);
            }else if (EnumUser.CustInvest == euser)
            {
                return string.Format("4{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else
            if (EnumUser.ClientInvest == euser)
            {
                return string.Format("5{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Admin == euser)
            {
                return string.Format("Ad{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            else if (EnumUser.Cash == euser)
            {
                return string.Format("C{0}{1}{2}", cdate, userid, multiplebill_id);
            }
            return "";
        }*/

    }
}
