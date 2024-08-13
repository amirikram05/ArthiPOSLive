using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember
{
    public class Account
    {

        public string username="";
        public string shop_name = "";
        public string business_type = "";
        public string address = "";
        public string phone = "";
        public string name1 = "";
        public string name2 = "";
        public string phone1 = "";
        public string phone2 = "";
        public string propriters_name = "";
        public string email = "";
        public string password = "";
        public string license_no = "";
        public string license_exp_date = "";
        public string api_key = "";
        public string api_key_exp_date = "";
        public int debit=0;
        public int credit=0;
        public int capital_Cash=0;
        public string role = "";
        public string local = "";
        public string isdb="0";
        public string accountclosing = "0";
        public string trade_mark="";
        public Account()
        {
            username =shop_name=address=phone=propriters_name=email=password=license_exp_date=license_no=
                api_key = api_key_exp_date = name1=name2=phone1=phone2=role= local= accountclosing="";
            debit =credit=capital_Cash =0;
        }

    }
}
