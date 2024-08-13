using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthiPOS.Utill
{
    public interface PagePrint
    {
        void pageA4();
        void pageA5();
        void pageA6();
    }
    public class ClientPageReport : PagePrint
    {
        public ClientPageReport()
        {

        }
        public void pageA4()
        {
            throw new NotImplementedException();
        }

        public void pageA5()
        {
            throw new NotImplementedException();
        }

        public void pageA6()
        {
            throw new NotImplementedException();
        }
    }
    public class CustomerPageReport : PagePrint
    {
        public CustomerPageReport()
        {

        }
        public void pageA4()
        {
            throw new NotImplementedException();
        }

        public void pageA5()
        {
            throw new NotImplementedException();
        }

        public void pageA6()
        {
            throw new NotImplementedException();
        }
    }
}
