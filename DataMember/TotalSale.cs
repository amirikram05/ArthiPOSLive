namespace DataMember
{
    public class TotalSale
    {

        public int total_chalan;
        public int total_quantity;
        public int total_sale;

        public int total_services;
        public Expense expense;

        /*public int total_munshiana;
        public int total_rent;
        public int total_labour;
        public int total_expense;
        */
        private float total_commission;
        private int total_chongi;
        private int remaining_amount;


        public int RemainingAmount
        {
            get
            {
                return remaining_amount;
            }
            set
            {
                remaining_amount = (value);
            }
        }

        public float Total_Commission
        {
            get
            {
                return total_commission;
            }
            set
            {
                total_commission = (float)/*Math.Ceiling*/(value);
            }
        }
        public float Total_Chongi
        {
            get
            {
                return total_chongi;
            }
            set
            {
                total_chongi = (int)/*Math.Ceiling*/(value);
            }
        }

        public float SUM_GTotal_Sale
        {
            get
            {
                return total_sale + total_commission + total_chongi;
            }

        }

        public TotalSale()
        {
            expense = new Expense();
        }





    }
}
