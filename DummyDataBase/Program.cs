using Newtonsoft.Json;

namespace DummyDataBase
{
    internal class Program
    {
        static List<Table> tables;
        static void Main(string[] args)
        {
            tables = new List<Table>();
            ParseTables();

            foreach (Table t in tables)
                t.Print();

            Console.ReadKey();
        }

        private static void ParseTables()
        {
            string readerJson = File.ReadAllText("./ReaderScheme.json");
            TableScheme tableReaders = JsonConvert.DeserializeObject<TableScheme>(readerJson);
            string readerCsv = File.ReadAllText("./Data/Readers.csv").Replace("\r", "");
            tables.Add(CsvParser.ParseCsv(readerCsv, tableReaders));

            string bookJson = File.ReadAllText("./BookScheme.json");
            TableScheme tableBooks = JsonConvert.DeserializeObject<TableScheme>(bookJson);
            string bookCsv = File.ReadAllText("./Data/Books.csv").Replace("\r", "");
            tables.Add(CsvParser.ParseCsv(bookCsv, tableBooks));
            /*
            string actionJson = File.ReadAllText("./ActionScheme.json");
            TableScheme tableActions = JsonConvert.DeserializeObject<TableScheme>(actionJson);
            string actionCsv = File.ReadAllText("./Data/Actions.csv").Replace("\r", "");
            tables.Add(CsvParser.ParseCsv(actionCsv, tableActions));
            */
        }
    }
}