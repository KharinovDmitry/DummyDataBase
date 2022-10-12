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
                
                return list;
            }
        }

        private static bool isValid(string input, JsonTable format)
        {
            bool res = true;
            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            res = isValidTitles(format, titles);
            res = isValidData(format, inputLines) ? res : false;
            return res;
        }

        private static bool isValidData(JsonTable format, string[] inputLines)
        {
            bool res = true;
            for(int s = 1; s < inputLines.Length; s++)
            {
                string[] fields = inputLines[s].Split(";");
                for (int i = 0; i < fields.Length; i++)
                {
                    switch (format.Fields[i].Type)
                    {
                        case "int":
                            if(!int.TryParse(fields[i], out int temp))
                            {
                                res = false;
                                WriteError(s, i, format.Fields[i].Type, fields[i]);
                            }
                            break;
                        case "DateTime":
                            if(!DateTime.TryParse(fields[i], out DateTime temp2))
                            {
                                res = false;
                                WriteError(s, i, format.Fields[i].Type, fields[i]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return res;
        }

        private static void WriteError(int s, int i, string typeFormat, string typeExept)
        {
            Console.WriteLine("Неверный формат данный\n" +
                "строка: {0} столбец: {1} - Ожидалось {2}, а встречено \"{3}\"",
                s + 1, i + 1, typeFormat, typeExept);
        }


        private static bool isValidTitles(JsonTable format, string[] titles)
        {
            if (titles.Length != format.Fields.Count)
            {
                Console.WriteLine("Ожидалось {0} столбцов, а получено {1}",
                    format.Fields.Count, titles.Length);
                return false;
            }
            for (int i = 0; i < titles.Length; i++)
            {
                if (!titles[i].Contains(format.Fields[i].Name))
                {
                    Console.WriteLine("Несоответсвие заголовков\n" +
                        "Столбец:{0} Ожидалось {1}, а встречено {2}",
                        i + 1, format.Fields[i].Name, titles[i]);
                    return false;
                }
            }
            return true;
        }
    }
}
