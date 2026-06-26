namespace DataMember
{
    /*public class SeasonSetting
        {
            public bool labour_per_product;
            public bool rent_per_product;
            public bool commission_per_product;
            public bool clerk_per_bill;
            public bool chongi_per_product;
        }*/
    public class Product
    {

        public string _product_id;
        private string pro_name;
        public string _product_name
        {
            get
            {
                return string.Format("{0}", pro_name);
            }
            set
            {
                this.pro_name = value;
            }
        }
        public string _type;
        public string _weight;
        public string _weight_id;
        public int sale_remaining_product;
        public int total_Quantity;
        public string marka;
        /*
        public int total_rent;
        public int total_labour;
        public int total_munshiana;*/
        public Product() { }

        public Product(string _product_id, string _product_name, string _type,
            string _weight_id, string _weight, int _quantity, string marka/*,int _rent,int _labour,int _munshiana*/)
        {
            this._product_id = _product_id;
            this._product_name = _product_name;
            this._type = _type;
            this._weight = _weight;
            this._weight_id = _weight_id;
            this.total_Quantity = _quantity;
            this.sale_remaining_product = _quantity;
            this.marka = marka;
            /*this.total_rent = _rent;
            this.total_labour = _labour;
            this.total_munshiana = _munshiana;*/
        }
    }
}
