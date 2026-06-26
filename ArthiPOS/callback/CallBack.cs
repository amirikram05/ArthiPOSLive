namespace ArthiPOS.callback
{
    public interface CallBackInterface
    {
        DataUpdate Product(DataUpdate data);
    }
    public class CallBack : CallBackInterface
    {


        public DataUpdate Product(DataUpdate data)
        {
            return data;
        }
    }

    public class DataUpdate
    {
        public string Id
        { get; private set; }
        public string Name
        { get; private set; }
        public string Type
        { get; private set; }

        public string Rent { get; private set; }
        public string Labour { get; private set; }
        public string BipComm { get; private set; }
        public string CusComm { get; private set; }
        public string Laga { get; private set; }
        public string Chongi { get; private set; }
        public string MSG { get; private set; }
    }
}
