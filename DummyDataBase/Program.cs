using Newtonsoft.Json;

namespace DummyDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string readerJson = File.ReadAllText("./ReaderScheme.json");
            JsonTable tableReaders = JsonConvert.DeserializeObject<JsonTable>(readerJson);
            string readerCsv = File.ReadAllText("./Data/Readers.csv").Replace("\r", "");
            List<Reader> readers = CsvParser.ParseReaders(readerCsv, tableReaders);

            string bookJson = File.ReadAllText("./BookScheme.json");
            JsonTable tableBooks = JsonConvert.DeserializeObject<JsonTable>(bookJson);
            string bookCsv = File.ReadAllText("./Data/Books.csv").Replace("\r", "");
            List<Book> books = CsvParser.ParseBooks(bookCsv, tableBooks);

            string actionJson = File.ReadAllText("./ActionScheme.json");
            JsonTable tableActions = JsonConvert.DeserializeObject<JsonTable>(actionJson);
            string actionCsv = File.ReadAllText("./Data/Actions.csv").Replace("\r", "");
            List<Action> history = CsvParser.ParseActions(actionCsv, tableActions, readers, books);

            if (readers == null || books == null || history == null)
                return;

            PrintBookInfo(books, history);

            Console.ReadKey();
        }

        private static void PrintBookInfo(List<Book> books, List<Action> history)
        {
            int lengthAuthor = 9;
            int lengthBookName = 10;
            int lengthReader = 8;
            int lengthDate = 10;
            foreach (var book in books)
            {
                lengthAuthor = Math.Max(lengthAuthor, book.Author.Length);
                lengthBookName = Math.Max(lengthBookName, book.Name.Length);
                Action lastAction = GetLastTakeAction(book, history);
                if (lastAction != null)
                    lengthReader = Math.Max(lengthReader, lastAction.Reader.FullName.Length);
                else
                    lengthReader = lengthReader;
            }
            PrintTitle(lengthAuthor, lengthBookName, lengthReader, lengthDate);
            foreach (var book in books)
            {
                Console.Write($"| {book.Author} ");
                for (int i = 0; i < lengthAuthor - book.Author.Length; i++)
                    Console.Write(" ");
                Console.Write($"| {book.Name} ");
                for (int i = 0; i < lengthBookName - book.Name.Length; i++)
                    Console.Write(" ");
                Action lastAction = GetLastTakeAction(book, history);
                if(lastAction != null)
                {
                    Console.Write($"| {lastAction.Reader.FullName} ");
                    for (int i = 0; i < lengthReader - lastAction.Reader.FullName.Length; i++)
                        Console.Write(" ");
                    Console.Write($"| {lastAction.Date.Day}.{lastAction.Date.Month}.{lastAction.Date.Year} ");
                    Console.WriteLine("|");
                }
                else
                {
                    Console.Write($"|");
                    for (int i = 0; i < lengthReader + 2; i++)
                        Console.Write(" ");
                    Console.Write($"|");
                    for (int i = 0; i < lengthDate + 2; i++)
                        Console.Write(" ");
                    Console.WriteLine("|");
                }
            }
        }

        private static void PrintTitle(int lengthAuthor, int lengthBookName, int lengthReader, int lengthDate)
        {
            Console.Write("| Автор ");
            for (int i = 0; i < lengthAuthor - 5; i++)
                Console.Write(" ");
            Console.Write("| Название ");
            for (int i = 0; i < lengthBookName - 8; i++)
                Console.Write(" ");
            Console.Write("| Читает ");
            for (int i = 0; i < lengthReader - 6; i++)
                Console.Write(" ");
            Console.Write("| Взял ");
            for (int i = 0; i < lengthDate - 4; i++)
                Console.Write(" ");
            Console.WriteLine("|");

            Console.Write("|");
            for (int i = 0; i < lengthAuthor + 2; i++)
                Console.Write("-");
            Console.Write("|");
            for (int i = 0; i < lengthBookName + 2; i++)
                Console.Write("-");
            Console.Write("|");
            for (int i = 0; i < lengthReader + 2; i++)
                Console.Write("-");
            Console.Write("|");
            for (int i = 0; i < lengthAuthor + 2; i++)
                Console.Write("-");
            Console.WriteLine("|");
        }

        private static Action GetLastTakeAction(Book book, List<Action> history)
        {
            Action res = null;
            foreach (var action in history)
            {
                if(action.Book == book)
                {
                    if (action.TypeAction == "Взял")
                        res = action;
                    else
                        res = null;
                }
                
            }
            return res;
        }
    }
}