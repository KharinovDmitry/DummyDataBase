using Newtonsoft.Json;

namespace DummyDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Table> tables = FileReader.ParseTables(@"./data");
            foreach (Table table in tables)
            {
                Console.WriteLine(table.Name);
            }
            Console.ReadKey();
        }
    }
}