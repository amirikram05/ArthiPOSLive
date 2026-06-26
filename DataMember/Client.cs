namespace DataMember
{
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
}
