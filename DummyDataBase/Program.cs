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

            foreach (var book in books)
            {
                Console.WriteLine("{0} \"{1}\" г.{2}",book.Author, book.Name, book.YearPublication);
                string info = GetInfoBook(book, history);
                if (info != "")
                    Console.WriteLine("\t" + info);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static string GetInfoBook(Book book, List<Action> history)
        {
            string res = "";
            foreach (var action in history)
            {
                if(action.Book == book)
                {
                    if (action.TypeAction.Contains("Взял"))
                    {
                        res = $"Взял {action.Reader.FullName} {action.Date.Day}.{action.Date.Month}.{action.Date.Year}";
                    }
                    else
                    {
                        res = "";
                    }
                }
            }
            return res;
        }
    }
}