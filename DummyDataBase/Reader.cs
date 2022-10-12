using System.Xml.Linq;
using System;

namespace DummyDataBase
{
    class Reader
    {
        public int ID { get; }
        public string FullName { get; set; }

        public Reader(int id, string fullName)
        {
            ID = id;
            FullName = fullName;
        }
        public void Print()
        {
            Console.WriteLine("{0} {1}", ID, FullName);
        }
    }
}
