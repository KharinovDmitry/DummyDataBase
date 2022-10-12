using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DummyDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string readerJson = File.ReadAllText("./ReaderScheme.json");
            JsonTable tableReaders = JsonConvert.DeserializeObject<JsonTable>(readerJson);
            string readerCsv = File.ReadAllText("./Data/Readers.csv");
            List<Reader> readers = CsvParser.Parse(readerCsv, tableReaders);

            string bookJson = File.ReadAllText("./BookScheme.json");
            JsonTable tableBooks = JsonConvert.DeserializeObject<JsonTable>(bookJson);

            string actionJson = File.ReadAllText("./ActionScheme.json");
            JsonTable tableActions = JsonConvert.DeserializeObject<JsonTable>(actionJson);



            Console.ReadKey();
        }
    }
    public class Item
    {
        public int millis;
        public string stamp;
        public DateTime datetime;
        public string light;
        public float temp;
        public float vcc;
    }
}