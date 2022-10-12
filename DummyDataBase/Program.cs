using Newtonsoft.Json;

namespace DummyDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string readerJson = File.ReadAllText("./ReaderScheme.json");
            JsonTable tableReaders = JsonConvert.DeserializeObject<JsonTable>(readerJson);
            string readerCsv = File.ReadAllText("./Data/Readers.csv");
            List<Reader> readers = CsvParser.ParseReaders(readerCsv, tableReaders);

            string bookJson = File.ReadAllText("./BookScheme.json");
            JsonTable tableBooks = JsonConvert.DeserializeObject<JsonTable>(bookJson);
            string bookCsv = File.ReadAllText("./Data/Books.csv");
            List<Book> books = CsvParser.ParseBooks(bookCsv, tableBooks);

            string actionJson = File.ReadAllText("./ActionScheme.json");
            JsonTable tableActions = JsonConvert.DeserializeObject<JsonTable>(actionJson);
            string actionCsv = File.ReadAllText("./Data/Actions.csv");
            List<Action> actions = CsvParser.ParseActions(actionCsv, tableActions, readers, books);
           
            Console.ReadKey();
        }
    }
}