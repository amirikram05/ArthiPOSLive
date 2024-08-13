using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember.memberlog
{
    public class AccountLog : AppSettings<AccountLog>
    {

        public string Name1;
        public string Name2;
        public string Phone1;
        public string Phone2;
        public string username;
        public string shop_name;
        public string address;
        public string phone;
        public string propriters_name;
        public int capital_Cash;
        public int loginCount = 0;
        public string api_key;
        public string sortmostBy;
        public string sortType;
    }

}
