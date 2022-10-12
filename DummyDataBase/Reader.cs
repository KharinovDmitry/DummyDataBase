namespace DummyDataBase
{
    class Reader
    {
        public int ID { get; }
        public string FullName { get; set; }

        public Reader(int iD, string fullName)
        {
            ID = iD;
            FullName = fullName;
        }
    }
}
