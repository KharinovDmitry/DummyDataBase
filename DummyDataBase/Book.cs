namespace DummyDataBase
{
    class Book
    {
        public string Name { get; }
        public string Author { get; }
        public int YearPublication { get; }
        public int Wardrobe { get; set; }
        public int Shelf { get; set; }

        public Book(string name, string author, int yearPublication, int wardrobe, int shelf)
        {
            Name = name;
            Author = author;
            YearPublication = yearPublication;
            Wardrobe = wardrobe;
            Shelf = shelf;
        }
    }
}
