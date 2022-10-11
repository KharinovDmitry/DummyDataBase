using System;

namespace DummyDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>()
            {
                new Book("Война и мир", "Толстой", 2020, 1, 1),
                new Book("Преступление и наказание", "Достоевский", 2015, 1, 1)
            };
            List<Reader> readers = new List<Reader>()
            {
                new Reader(1, "Устьянцев Евгений Юрьевич"),
                new Reader(2, "Кожурков Георгий Хорошкомалькович")
            };
            List<Action> actions = new List<Action>()
            {
                new Action(books[0], readers[0], new DateTime(2022, 10, 8)),
                new Action(books[1], readers[1], new DateTime(2022, 10, 9), new DateTime(2022, 10, 11))
            };
        }
    }
}