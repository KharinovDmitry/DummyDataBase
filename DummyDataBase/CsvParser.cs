namespace DummyDataBase
{
    static class CsvParser
    {
        public static List<Reader> ParseReaders(string input, JsonTable format)
        {
            List<Reader> list = new List<Reader>();

            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            for (int i = 0; i < titles.Length; i++)
            {
                if (titles[i] != format.Fields[i].Name)
                {
                    Console.WriteLine("Несоответсвие заголовков\n" +
                        "Столбец[{0}] Ожидалось {1}, а встречено {1}",
                        i + 1, format.Fields[i].Name, titles[i]);
                    return null;
                }
            }

            string[] readersStrings = new string[inputLines.Length - 1];
            inputLines.CopyTo(readersStrings, 1);
            foreach (var readerString in readersStrings)
            {
                string[] fields = readerString.Split(";");
                for (int i = 0; i < fields.Length; i++)
                {
                    
                }
            }


            return list;
        }
    }
}
