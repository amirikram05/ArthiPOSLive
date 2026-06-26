using System.Collections.Generic;

namespace DataMember
{

    public class Admin
    {
        //public List<Landlord> list_landlords = new List<Landlord>();
        public List<Landlord> clients = new List<Landlord>();
        public int remainingStock = 0;
        public static bool SaveLog = false;
        public static System.Globalization.CultureInfo cultinfo = new System.Globalization.CultureInfo("ur-PK");

        public static LogExecManager LogExecMang = getLogexecMang();

        private static LogExecManager getLogexecMang()
        {
            
            return new LogExecManager("c:\\arthilog", 10);
        }


        //Private Constructor.  
        private static Admin instance = null;
        private Admin()
        {
        }
        private static string date;
        public static string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
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
        public int getCustomerIndex(Landlord landlord, string cust_id)
        {
            var reponse = landlord.customers.FindIndex(r => r.customer_profile.pid == cust_id);
            return reponse;

        }

        public int getTotalRQuantity()
        {
            int quantity = 0;
            foreach (Landlord land in Admin.GetInstance.clients)
            {

                quantity += land.land_product.sale_remaining_product;
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


    }
}
