using System.Windows.Markup;

namespace DummyDataBase
{
    static class CsvParser
    {
        public static Table ParseCsv(string input, TableScheme scheme)
        {
            if (!isValid(input, scheme))
            {
                return null;
            }

            Table table = new Table(scheme);

            string[] lines = input.Split("\n");
            for (int i = 1; i < lines.Length; i++)
            {
                Row newRow = new Row();
                string[] data = lines[i].Split(";");
                for (int j = 0; j < data.Length; j++)
                {
                    object value;
                    switch (scheme.Columns[j].Type)
                    {
                        case DataType.String:
                            value = data[j];
                            break;
                        case DataType.Int:
                            value = int.Parse(data[j]);
                            break;
                        case DataType.Float:
                            value = float.Parse(data[j]);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    newRow.Data.Add(scheme.Columns[j], value);
                }
                table.Rows.Add(newRow);
            }

            return table;
        }

        private static bool isValid(string input, TableScheme scheme)
        {
            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            return isValidTitles(scheme, titles) && isValidData(scheme, inputLines);

        }

        private static bool isValidData(TableScheme scheme, string[] inputLines)
        {
            for(int s = 1; s < inputLines.Length; s++)
            {
                string[] fields = inputLines[s].Split(";");
                for (int i = 0; i < fields.Length; i++)
                {
                    switch (scheme.Columns[i].Type)
                    {
                        case DataType.Int:
                            if(!int.TryParse(fields[i], out int temp))
                            {
                                Console.WriteLine("Неверный тип данных\n" +
                                    "столбец: {0} строка: {1} - Ожидалось int, а встречено \"{2}\"",
                                    s + 1, i + 1, fields[i]);
                                return false;
                            }
                            break;
                        case DataType.Float:
                            if(!float.TryParse(fields[i], out float temp2))
                            {
                                Console.WriteLine("Неверный тип данных\n" +
                                    "столбец: {0} строка: {1} - Ожидалось float, а встречено \"{2}\"",
                                    s + 1, i + 1, fields[i]);
                                return false;
                            }
                            break;
                        case DataType.String:
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            return true;
        }

        private static bool isValidTitles(TableScheme scheme, string[] titles)
        {
            if (titles.Length != scheme.Columns.Count)
            {
                Console.WriteLine("Ожидалось {0} столбцов, а получено {1}",
                    scheme.Columns.Count, titles.Length);
                return false;
            }
            for (int i = 0; i < titles.Length; i++)
            {
                if (!titles[i].ToLower().Contains(scheme.Columns[i].Name.ToLower()))
                {
                    Console.WriteLine("Несоответсвие заголовков\n" +
                        "столбец: {1} - Ожидалось {1}, а встречено \"{2}\"",
                        i + 1, scheme.Columns[i].Name, titles[i]);
                    return false;
                }
            }
            return true;
        }
    }
}
