namespace DummyDataBase
{
    class Action
    {
        public int ID { get; }
        public Book Book { get; }
        public Reader Reader { get; }
        public DateTime DateTake { get; }
        public DateTime DateReturn { get; set; }    

        public Action(Book book, Reader reader, DateTime dateTake)
        {
            Book = book;
            Reader = reader;
            DateTake = dateTake;
        }
        public Action(Book book, Reader reader, DateTime dateTake, DateTime dateReturn)
        {
            Book = book;
            Reader = reader;
            DateTake = dateTake;
            DateReturn = dateReturn;
        }
    }
}
