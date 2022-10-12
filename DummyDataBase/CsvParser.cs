namespace DummyDataBase
{
    static class CsvParser
    {
        public static List<Reader> ParseReaders(string input, JsonTable format)
        {
            if (!isValid(input, format))
            {
                return null;
            }
            else
            {
                List<Reader> list = new List<Reader>();
                Console.WriteLine("Valid");
                return list;
            }
        }

        private static bool isValid(string input, JsonTable format)
        {
            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            if (titles.Length != format.Fields.Count)
            {
                Console.WriteLine("Ожидалось {0} столбцов, а получено {1}",
                    format.Fields.Count, titles.Length);
                return false;
            }
            for (int i = 0; i < titles.Length; i++)
            {
                Console.WriteLine("[{0}] [{1}]", titles[i], format.Fields[i].Name);
                if (!titles[i].Contains(format.Fields[i].Name))
                {
                    Console.WriteLine("Несоответсвие заголовков\n" +
                        "Столбец:{0} Ожидалось {1}, а встречено {1}",
                        i + 1, format.Fields[i].Name, titles[i]);
                    return false;
                }
            }

            string[] readersStrings = new string[inputLines.Length];
            inputLines.CopyTo(readersStrings, 0);
            foreach (var readerString in readersStrings)
            {
                string[] fields = readerString.Split(";");
                for (int i = 0; i < fields.Length; i++)
                {
                    
                }
            }
            return true;
        }
    }
}
