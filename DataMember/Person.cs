using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMember
{
    public class Person
    {

        public string phone;
        public string pid;
        public string pkey;
        public string pname;
        public int advance;
        public int expense;

        public Person() { }
        public Person(string pid, string pkey, string pname, string phone, int advance, int expense)
        {
            this.pid = pid;
            this.pkey = pkey;
            this.pname = pname;
            this.phone = phone;
            this.expense = expense;
            this.advance = advance;
        }


    }
}
