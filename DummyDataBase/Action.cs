namespace DummyDataBase
{
    class Action
    {
        public int ID { get; }
        public Book Book { get; }
        public Reader Reader { get; }
        public DateTime Date { get; }
        public string TypeAction { get; }

        public Action(int id, Book book, Reader reader, DateTime date, string typeAction)
        {
            ID = id;
            Book = book;
            Reader = reader;
            Date = date;
            TypeAction = typeAction;
        }
        public void Print()
        {
            Console.WriteLine("{0} {1} {2} {3} {4}", ID, Book.Name, Reader.FullName, Date, TypeAction);
        }
    }
}
