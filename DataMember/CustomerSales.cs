using System.Collections.Generic;
using System.Linq;

namespace DataMember
{
    public class CustomerSales : TotalSale
    {
        public Person person;
        public string date;

        public List<Customer> customers;
        public CustomerSales(string date)
        {
            person = new Person();
            customers = new List<Customer>();
            this.date = date;
        }
        public int getQuantity()
        {
            total_quantity = (int)customers.Sum(x => x.total_quantity);
            return total_quantity;
        }
        public int getGrandTotal()
        {
            //total_sale = (int)customers.Sum(x => x.getGrandTotalCustomer());
            return (int)SUM_GTotal_Sale;
        }
        public int getSaleTotal()
        {
            return (int)customers.Sum(x => x.sale.getTotalSale() + x.sale.getTotalExtraAmountCustomer());
        }
        public float getTotalCommission()
        {
            return Total_Commission = customers.Sum(x => x.Total_Commission);
        }
        public float getTotalChongi()
        {
            return Total_Chongi = (int)customers.Sum(x => x.Total_Chongi);
        }
        public int GetGrandTotal
        {
            get
            {
                return total_sale + (int)Total_Commission + (int)Total_Chongi;
            }
        }

    }
}
