namespace DataMember
{
    public class Sale
    {
        #region Sale
        public int _sale_quantity = 0;
        public int add_extra_amount_Landlord = 0;
        public int add_extra_amount_Customer = 0;
        private int amount = 0;
        private int tsale = 0;
        private int _total_extra_amount_cust;
        private int _total_extra_amount_land;


        public Sale(int quantity, int amount)
        {
            this._sale_quantity = quantity;
            this.amount = amount;
            updateSale();
        }

        public void updateSale()
        {
            getTotalSale();
            getTotalExtraAmountCustomer();
            getTotalExtraAmountLandlord();
        }

        public float _sale_amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = (int)value;
            }
        }

        #endregion
        public int _TotalSaleAmount
        {
            get
            {
                return tsale;
            }
            set
            {
                this.tsale = value;
            }
        }

        public int _TotalExtraAmountCustomer
        {
            get
            {
                return _total_extra_amount_cust;
            }
            set
            {
                _total_extra_amount_cust = value;
            }
        }
        public int _TotalExtraAmountLandlord
        {
            get
            {
                return _total_extra_amount_land;
            }
            set
            {
                _total_extra_amount_land = value;
            }

        }



        #region Total Amounts
        public int getTotalSale()
        {
            _TotalSaleAmount = (_sale_quantity * (int)_sale_amount);
            return _TotalSaleAmount;
        }

        public int getTotalExtraAmountCustomer()
        {
            return _TotalExtraAmountCustomer = _sale_quantity * add_extra_amount_Customer;

        }
        public int getTotalExtraAmountLandlord()
        {
            return _TotalExtraAmountLandlord = _sale_quantity * add_extra_amount_Landlord;
        }





        #endregion
    }
}
