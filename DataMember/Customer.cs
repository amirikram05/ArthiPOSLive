using System;

namespace DataMember
{
    public class Customer : TotalSale
    {
        public string date = "";
        public string tag_Action = "";
        private float grand_Total_cust;
        private float grand_Total_landlord;
        public string cust_bill_id;

        //public int total_chalan;
        //public int total_quantity;
        //public float total_sale;
        //public string product_name;
        //public string product_packing;
        public Product product;
        public Person customer_profile;
        public Sale sale;
        private Services services;
        private Person landlordProfile;
        public bool isCustomerBill = false;
        public string status = "0";

        public Person _LandlordProfile
        {
            get
            {
                return landlordProfile;
            }
            set
            {
                landlordProfile = value;
            }
        }
        public Services _Services
        {
            get
            {
                return services;
            }
            set
            {
                services = value;
            }
        }


        public Customer(string date, Services services, bool isCustomerBill, Sale sale, Person landlordProfile)
        {
            customer_profile = new Person();
            this.sale = sale;
            product = new Product();
            this.isCustomerBill = isCustomerBill;
            this.services = services;
            this.landlordProfile = landlordProfile;
            this.date = date;
        }



        /* public float GetExtraCustomer
         {
             get
             {
                 return sale._sale_quantity * sale.add_extra_amount_Customer;
             }
         }
         public float GetExtraLandlord
         {
             get
             {
                 return sale._sale_quantity * sale.add_extra_amount_Landlord;
             }
         }*/
        /* public float GetTotalSaleCustomer
         {
             get
             {
                 if (isCustomerBill)
                 {
                     return (sale.getTotalSale())+sale.getTotalExtraAmountCustomer();
                 }
                 else
                 {
                     return (sale.getTotalSale()) +sale.getTotalExtraAmountLandlord();
                 }
             }

         }*/
        public int GrandTotalCustomer
        {
            get
            {
                return (int)grand_Total_cust;
            }
            set
            {
                grand_Total_cust = value;
            }
        }
        public int GrandTotalLandlord
        {
            get
            {
                return (int)grand_Total_landlord;
            }
            set
            {
                grand_Total_landlord = value;
            }
        }


        public int getGrandTotalCustomer()
        {
            return GrandTotalCustomer = (int)(sale.getTotalSale() + sale.getTotalExtraAmountCustomer() + getCommission() + getChongi());

        }
        public int getGrandTotalLandlord()
        {
            return GrandTotalLandlord = (int)(sale.getTotalSale()
                + sale.getTotalExtraAmountLandlord()
                //+ getCommission() + getChongi())
                );
        }
        public float getCommission()
        {
            int totsale = sale.getTotalSale() + sale.getTotalExtraAmountCustomer();
            Total_Commission = ((totsale) * services.commission_customer_product) / 100;
            return (float)Math.Ceiling(Total_Commission);
        }
        public float getChongi()
        {
            Total_Chongi = (int)(services.customer_chongi * sale._sale_quantity);
            return Total_Chongi;
        }


        public void updateTotal()
        {
            sale.getTotalSale();
            getCommission();
            getChongi();
            getGrandTotalCustomer();
            getGrandTotalLandlord();
        }





    }
}
