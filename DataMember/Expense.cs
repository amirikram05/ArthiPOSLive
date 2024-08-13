using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMember
{
    public class Expense
    {

        public string labour_name="";
        public string rent_name = "";
        public string advance = "";
        public string clerk_name = "";
        public string extra_amount_name = "";
        public string category = "";
        public Expense()
        {
            total_advance_amount = 0;
            total_expense = 0;
            total_labour = 0;
            total_munshiana = 0;
            total_rent = 0;
        }


        public int total_marketfee;
        public int total_munshiana;
        public int total_rent;
        public int total_labour;
        public int total_expense;
        public int total_advance_amount;



        
    }
}
