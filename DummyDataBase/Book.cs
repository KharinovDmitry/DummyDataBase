namespace DummyDataBase
{
    class Book
    {
        public int ID { get; }
        public string Name { get; }
        public string Author { get; }
        public int YearPublication { get; }
        public int Wardrobe { get; set; }
        public int Shelf { get; set; }

        public Book(int id, string name, string author, int yearPublication, int wardrobe, int shelf)
        {
            ID = id;
            Name = name;
            Author = author;
            YearPublication = yearPublication;
            Wardrobe = wardrobe;
            Shelf = shelf;
        }

        public void Print()
        {
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", ID, Name, Author, YearPublication, Wardrobe, Shelf);
        }
    }
}
