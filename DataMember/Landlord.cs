using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMember
{
    public enum EStatus
    {
        InComplete = 0, UnPaid = 1, Paid = 2, Initial = 3, Complete = 4, CompleteUpdate = 5
    }
    public class Landlord : TotalSale
    {
        #region Bikri detail
        public string bill_type = "";
        public float bikri_quantity = 0;
        public float bikri_rate = 0;
        public int total_bikri = 0;

        #endregion


        public string tag_Action = "";
        public bool isRecordSaleInserted = false;

        public Client client;
        public string record_id;
        public string date;
        public EStatus status = EStatus.Initial;
        // public string status= "InComplete";// 1:InComplete/2:UnPaid/3:Paid

        public string status_date;
        public string bill_paid_to;
        public string bikri_type = "B";
        //public Sale sale;
        //public Client client;
        public List<Customer> customers { get; set; }
        //public SeasonSetting setting;

        //public TotalSale client_sale;
        public Person land_person;
        public Product land_product;
        //private ExtraAmount extraAmountLandlord;

        public Services service;
        public string bill_key;
        public string category = "";


        /*public ExtraAmount ExtraAmountLandlord
        {
            get
            {
                return extraAmountLandlord;
            }
            set
            {
                if (extraAmountLandlord==null)
                {
                    //extraAmountLandlord = new ExtraAmount("",0,0,0);
                }
                extraAmountLandlord = value;
            }
        }*/
        public int getStockSaleQuantity(int quantity)
        {
            foreach (Customer cus in customers)
            {
                quantity = quantity - cus.sale._sale_quantity;
            }
            /*if (quantity==0)
            {
                status = EStatus.UnPaid; //Enum.GetName(typeof(EStatus), 1);
            }else
            {
                status = EStatus.InComplete; //Enum.GetName(typeof(EStatus), 0);
            }*/

            return quantity;
        }
        public void UpdateTotal()
        {
            getCommission();
            getChongi();
            total_sale = GetTotalSaleLandLord;
            total_services = (int)GetTotalService;
            //land_product.sale_remaining_product = ;
            land_product.sale_remaining_product = getStockSaleQuantity(land_product.total_Quantity);


        }
        public float GetCommission
        {
            get
            {
                return (float)Math.Ceiling(Total_Commission);
            }
            set
            {
                Total_Commission = value;
            }
        }
        public float GetChongi
        {
            get
            {

                return Total_Chongi;
            }
            set
            {
                Total_Chongi = value;
            }
        }
        public int GetTotalSaleLandLord
        {
            get
            {
                if (bill_type == "B")
                {
                    total_bikri =(int) Math.Ceiling( bikri_quantity * bikri_rate);
                    return total_bikri;
                }
                else
                {
                    total_sale = (int)customers.Sum(x => x.sale.getTotalSale() + x.sale.getTotalExtraAmountLandlord());
                    return total_sale;
                }
            }
        }
        public float GetGrandTotal
        {
            get
            {
                if (bill_type == "B")
                    return (bikri_quantity * bikri_rate) - (GetCommission + GetChongi + GetTotalService);
                else
                    return (GetTotalSaleLandLord -
                    (GetCommission + GetChongi + GetTotalService));
            }
        }
        public float GetTotalService
        {
            get
            {
                total_services =
                    expense.total_labour + expense.total_rent
                    + expense.total_munshiana + expense.total_marketfee
                    + (land_person.advance > 0 ? land_person.advance : client._person_cl.advance);
                return total_services;
            }
        }
        public Landlord()
        {
            customers = new List<Customer>();
            //sale = new Sale();
            // client_sale = new TotalSale();
            client = new Client();
            land_person = new Person();
            land_product = new Product();
            service = new Services();
            //extraAmountLandlord = new ExtraAmount("",0,0,0);
        }
        public Landlord(bool newObj)
        {

        }




        public void addCustomer()
        {
            foreach (Customer customer in customers)
            {
                //Admin.list_cusomters.Add(customer);
            }
        }
        /*public void client_sale()
        {
            if (setting.rent_per_product && setting.labour_per_product && setting.clerk_per_bill)
            {
                total_rent = getTotalRent();
                total_labour = getLabour();
                total_munshiana = getMunshiana(setting.clerk_per_bill);
                bill_Total_Amount = total_sale - (advance_amount + total_rent + total_labour + total_munshiana);
            }
        }*/



        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public float getCommission()
        {
            if (service == null)
            {
                service = new Services();
            }

            Total_Commission = bill_type == "b" ? ((float)(bikri_quantity * bikri_rate) * service.commission_client_product) / 100 : ((float)(total_sale) * service.commission_client_product) / 100;
            return Total_Commission;
        }
        public float getChongi()
        {
            Total_Chongi = (int)(service.client_chongi * total_quantity);
            return Total_Chongi;
        }
    }
}
