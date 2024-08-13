using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthiPOS.shop.test
{
    public class Admin
    {
        //public List<Landlord> list_landlords = new List<Landlord>();
        public List<Landlord> clients = new List<Landlord>();



        //Private Constructor.  
        private static Admin instance = null;
        private Admin()
        {
        }

        public static Admin GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Admin();

                return instance;
            }
        }



        public void updateLandlord(Landlord landlord)
        {
            clients[getLandlordIndex(landlord.land_person.pid)] = landlord;
        }
        

        public int getLandlordIndex(string id)
        {
            var reponse = clients.FindIndex(r => r.land_person.pid == id);
            return reponse;

        }
        public int getCustomerIndex(Landlord landlord,string cust_id)
        {
            var reponse = landlord.customers.FindIndex(r => r.customer_profile.pid == cust_id);
            return reponse;

        }

        public int getTotalRQuantity()
        {
            int quantity= 0;
            foreach (Landlord land in Admin.GetInstance.clients)
            {
                
                quantity += land.land_product.sale_remaining_product  ;
            }
            return quantity;
        }


        public string getTotalLandloardNames()
        {
            string names = "";
            foreach (Landlord land in Admin.GetInstance.clients)
            {
                names += land.land_person.pname + ", ";
            }
            return names;
        }
        public List<Landlord> getLandlordsList(DataTable data_tbl)
        {
            List<Landlord> clients = new List<Landlord>();

            for (int k = 0; k < data_tbl.Rows.Count; k++)
            {
                DataRow row = data_tbl.Rows[k];
                string _bill_id = row[0].ToString();
                string _date = row[15].ToString();
                string _vehicle_id = row[19].ToString();

                string _id = row[3].ToString();
                string _clientnameid = row[4].ToString();
                string _key = row[16].ToString();

                string _expense = row[14].ToString();

                string _customer_commission = row[20].ToString();
                string _customer_chongi = row[21].ToString();
                string _labourpp = row[22].ToString();
                string _client_chongi = row[24].ToString();
                string _client_commission = row[25].ToString();
                string _rent_per_product = row[26].ToString();
                string total_bipari_commission = row[27].ToString();
                string total_bipari_chongi = row[28].ToString();







                Services s = new Services();


                s.commission_client_product = float.Parse(_client_commission);
                s.commission_customer_product = float.Parse(_customer_commission);

                s.client_chongi = float.Parse(_client_chongi);
                s.customer_chongi = float.Parse(_customer_chongi);

                s.labour_per_product = float.Parse(_labourpp);
                s.rent_per_product = float.Parse(_rent_per_product);



                Client temp = new Client();

                temp._vehicle_id = _vehicle_id;
                temp.date = _date;
                temp.record_id = _bill_id;



                Person cl_person = new Person(_id, _key, _clientnameid, "", 0, int.Parse(_expense));
                temp._person_cl = cl_person;
                temp._services = s;





                //for (int j= 0;j < data_tbl.Rows.Count;j++)
                {
                    //    DataRow row = data_tbl.Rows[j];

                    //    if (_id== row[1].ToString())
                    {
                        //        k=j;
                        string _ll_id = row[3].ToString();
                        string _landloardnameid = row[4].ToString(); ;
                        string _product_id = row[5].ToString();
                        string _product_name = row[6].ToString();
                        string _weight_id = row[7].ToString();
                        string _weight = row[8].ToString();
                        string _total_quantity = row[9].ToString();
                        string _total_rent = row[10].ToString();
                        string _total_labour = row[11].ToString();
                        string _total_munshiana = row[12].ToString();
                        string _advance = row[13].ToString();
                        string _ll_key = row[17].ToString();
                        string _type = row[18].ToString();
                        string _remaining_item = row[23].ToString();

                        s.clerk_per_bill = float.Parse(_total_munshiana);


                        Product p = new Product();
                        p._product_id = _product_id;
                        p._product_name = _product_name;
                        p._weight_id = _weight_id;
                        p._weight = _weight;
                        p._type = _type;
                        p.total_Quantity = int.Parse(_total_quantity);
                        p.sale_remaining_product = int.Parse(_remaining_item);

                        temp._product = p;



                        


                        Person landperson = new Person(_ll_id, _ll_key, _landloardnameid, "", int.Parse(_advance), 0);

                        Landlord landlord = new Landlord();
                        landlord.tag_Action = "insert";
                        landlord.record_id = _bill_id;
                        landlord.date = _date;
                        landlord.client = temp;
                        landlord.service = s;
                        landlord.land_person = landperson;
                        landlord.land_product = p;

                        landlord.total_rent = int.Parse(_total_rent);
                        landlord.total_labour = int.Parse(_total_labour);
                        
                        landlord.total_munshiana = int.Parse(_total_munshiana);
                        if (total_bipari_commission != "")
                        {
                            landlord.total_commission = float.Parse(total_bipari_commission);

                        }
                        if (total_bipari_chongi != "")
                        {
                            landlord.total_chongi = int.Parse(total_bipari_chongi);

                        }
                        DBHandler db = new DBHandler();
                         DataTable dt_customer =(DataTable) db.getClient_Sales(_bill_id);
                         List<Customer> cust_list = new List<Customer>();
                         for (int i = 0; i < dt_customer.Rows.Count; i++)
                         {
                             DataRow cust_row = dt_customer.Rows[i];
                            if (cust_row[0].ToString() == "")
                            {

                            }
                            else
                            { 
                                 Customer c = new Customer();
                                 c.tag_Action = "insert";
                                 c.cust_bill_id = cust_row[0].ToString();
                                //c.product_name = landlord.land_product._product_name;
                                //c.product_packing = landlord.land_product._type;
                                 c.product._product_name = landlord.land_product._product_name;
                                 c.product._weight = landlord.land_product._type;
                                 c.sale._sale_quantity = int.Parse(cust_row[1].ToString());
                                 c.sale._sale_amount = int.Parse(cust_row[2].ToString());
                                 c.sale.total_Sale = int.Parse(cust_row[3].ToString());
                                 c.grand_Total = int.Parse(cust_row[4].ToString());

                                 c.total_sale = int.Parse(cust_row[3].ToString());
                                 c.total_commission = int.Parse(cust_row[5].ToString());
                                 c.total_chongi = int.Parse(cust_row[6].ToString());

                                 c.customer_profile.pid = cust_row[9].ToString();
                                 c.customer_profile.pname = cust_row[10].ToString();
                                 cust_list.Add(c);

                                 landlord.total_sale += (int)c.total_sale;
                            }
                        }
                        landlord.customers = cust_list;

                        clients.Add(landlord);

                    }
                }

            }
            return clients;
        }
        

    }
    public class Person
    {
        
        public string phone;
        public string pid;
        public string pkey;
        public string pname;
        public int advance;
        public int expense;

        public Person() { }
        public Person(string pid, string pkey, string pname, string phone, int advance,int expense)
        {
            this.pid = pid;
            this.pkey = pkey;
            this.pname = pname;
            this.phone = phone;
            this.expense = expense;
            this.advance = advance;
        }

       
    }
    public class Services
    {
        public float labour_per_product;
        public float rent_per_product;
        public float customer_chongi;
        public float commission_customer_product;
        public float client_chongi;
        public float commission_client_product;
        public float clerk_per_bill;
    }
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
        public string _product_name;
        public string _type;
        public string _weight;
        public string _weight_id;
        public int sale_remaining_product;
        public int total_Quantity;
        /*
        public int total_rent;
        public int total_labour;
        public int total_munshiana;*/
        public Product() { }

        public Product(string _product_id, string _product_name, string _type,
            string _weight_id,string _weight, int _quantity/*,int _rent,int _labour,int _munshiana*/)
        {
            this._product_id = _product_id;
            this._product_name = _product_name;
            this._type = _type;
            this._weight = _weight;
            this._weight_id = _weight_id;
            this.total_Quantity = _quantity;
            this.sale_remaining_product = _quantity;
            /*this.total_rent = _rent;
            this.total_labour = _labour;
            this.total_munshiana = _munshiana;*/
        }
    }
    public class Client
    {
        public string record_id;
        public string date;
        public string _vehicle_id;

        public Product _product;
        public Person _person_cl;
        public Services _services;

        public Client()
        {
           // customers = new List<Customer>();
            _product = new Product();
            _person_cl = new Person();
            _services = new Services();
        }

       



    }
    public class Sale
    {
        #region Sale
        
        public float _sale_amount;
        public int _sale_quantity;
        #endregion
        #region Expense
        public int expense;
        #endregion



        #region Total Amounts
        #region sale
        public float total_Sale;
        #endregion

       /* public int total_Rent;
        public int total_Labour;
        public int advance_amount;

        public int total_munshiana;

        public float total_commission;
        public float total_chongi;
        public float bill_Total_Amount;

        public int getTotalService()
        {
            return (total_Rent + total_Labour + advance_amount + total_munshiana);
        }
        */

        #endregion
    }
    public class TotalSale
    {

        public int total_chalan;
        public int total_sale;
        public int total_services;

        public int total_munshiana;
        public int total_rent;
        public int total_labour;
        public int total_expense;

        public float total_commission;
        public int total_chongi;

        

        
    }
    public class Landlord : TotalSale
    {
        public string tag_Action = "";

        public Client client;
        public string record_id;
        public string date;
        //public Sale sale;
        //public Client client;
        public List<Customer> customers;
        //public SeasonSetting setting;

        //public TotalSale client_sale;
        public Person land_person;
        public Product land_product;

        public Services service;
        public float getGrandTotal()
        {
            return total_sale - (total_commission+total_chongi+getTotalService());
        }
        public float getTotalService()
        {
            return total_labour + total_rent + total_munshiana + land_person.advance;
        }
        public Landlord()
        {
            customers = new List<Customer>();
            //sale = new Sale();
           // client_sale = new TotalSale();
            client = new Client();
        }

        public void calculateClientSale()
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

     


        
    }
    public class Customer : TotalSale
    {
        public string tag_Action = "";
        //public int total_chalan;
        //public int total_quantity;
        //public float total_sale;
        //public string product_name;
        //public string product_packing;
        public Product product;
        public Person customer_profile;
        public Sale sale;
        public float grand_Total;
        public string cust_bill_id;
        public Client landloard;


        public Customer()
        {
            customer_profile = new Person();
            //customer_sale = new TotalSale();
            sale = new Sale();
            product = new Product();
        }






    }

    
}
