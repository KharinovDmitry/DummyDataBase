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
                t.Print(0, tables);

            Console.ReadKey();
        }

        private static void ParseTables()
        {
            try
            {
                foreach (var dirMain in Directory.GetDirectories(@"./data"))
                {
                    string jsonScheme;
                    TableScheme tableScheme = null;
                    string csvData = null;
                    foreach (var file in Directory.GetFiles(dirMain))
                    {
                        if(file == null)
                        {
                            continue;
                        }
                        if (file.Contains(".json"))
                        {
                            jsonScheme = File.ReadAllText(file);
                            tableScheme = JsonConvert.DeserializeObject<TableScheme>(jsonScheme);
                            continue;
                        }
                        if (file.Contains(".csv"))
                        {
                            csvData = File.ReadAllText(file).Replace("\r", "");
                            continue;
                        }
                    }
                    if (tableScheme != null && csvData != null)
                    {
                        tables.Add(new Table(tableScheme, csvData));
                    }
                }
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}