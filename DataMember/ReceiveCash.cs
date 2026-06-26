namespace DataMember
{
    public class ReceiveCash
    {
        public string name;
        public string key;
        public int amount;
        public string date;
        public string id;
        public int discount;
        public int remainingCash;
        public string recID;
        public string type;
        public string desc;

        public ReceiveCash(string date, string name, int amount, string key,
            string id, int discount, int remainingCash
            , string recID, string type, string desc)
        {
            this.id = id;
            this.date = date;
            this.key = key;
            this.amount = amount;
            this.name = name;
            this.discount = discount;
            this.remainingCash = remainingCash;
            this.recID = recID;
            this.type = type;
            this.desc = desc;

        }
    }
}
