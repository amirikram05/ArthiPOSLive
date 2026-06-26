namespace ArthiPOS.Utilllll
{
    public class BillKey
    {


        public enum EnumUser
        {
            Client, Customer, LandLoard, ClientInvest, CustInvest,
            Admin, Cash, PaymentSale, Food, ClientExpense
        }
        public static EnumUser e_User = EnumUser.Client;
        public static string getDateKey(string date)
        {
            return date.Replace("-", "");
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
            /*string id = new BLogic().p_getInvoiceID()+ multiplebill_id;
            return id;*/
            return "";
        }

    }
}
