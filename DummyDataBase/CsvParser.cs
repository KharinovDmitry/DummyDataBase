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
                string[] inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    string[] readerInfo = inputLines[i].Split(";");
                    int id = int.Parse(readerInfo[0]);
                    string fullName = readerInfo[1];
                    list.Add(new Reader(id, fullName));
                }
                return list;
            }
        }

        public static List<Book> ParseBooks(string input, JsonTable format)
        {
            if (!isValid(input, format))
            {
                return null;
            }
            else
            {
                List<Book> list = new List<Book>();
                string[] inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    string[] bookInfo = inputLines[i].Split(";");
                    int id = int.Parse(bookInfo[0]);
                    string name = bookInfo[1];
                    string autor = bookInfo[2];
                    int year = int.Parse(bookInfo[3]);
                    int wardrobe = int.Parse(bookInfo[4]);
                    int shelf = int.Parse(bookInfo[5]);
                    list.Add(new Book(id, name, autor, year, wardrobe, shelf));
                }
                return list;
            }
        }

        public static List<Action> ParseActions(string input, JsonTable format, List<Reader> readers, List<Book> books)
        {
            if (!isValid(input, format))
            {
                return null;
            }
            else
            {
                List<Action> list = new List<Action>();
                string[] inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    string[] atcionsInfo = inputLines[i].Split(";");    
                    int id = int.Parse(atcionsInfo[0]);
                    int bookId = int.Parse(atcionsInfo[1]);
                    int readerId = int.Parse(atcionsInfo[2]);
                    DateTime date = DateTime.Parse(atcionsInfo[3]);
                    string type = atcionsInfo[4];
                    list.Add(new Action(id, GetBook(bookId, books), GetReader(readerId, readers), date, type));
                }
                return list;
            }
        }

        private static Reader GetReader(int id, List<Reader> list)
        {
            foreach (var reader in list)
            {
                if(reader.ID == id)
                {
                    return reader;
                }
            }
            throw new ArgumentException();
        }

        private static Book GetBook(int id, List<Book> list)
        {
            foreach (var book in list)
            {
                if (book.ID == id)
                {
                    return book;
                }
            }
            throw new ArgumentException();
        }

        private static bool isValid(string input, JsonTable format)
        {
            string[] inputLines = input.Split("\n");
            string[] titles = inputLines[0].Split(";");
            return isValidTitles(format, titles) && isValidData(format, inputLines);

        }

        private static bool isValidData(JsonTable format, string[] inputLines)
        {
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
                                WriteError("Неверный тип данных", s, i, format.Fields[i].Type, fields[i]);
                                return false;
                            }
                            break;
                        case "DateTime":
                            if(!DateTime.TryParse(fields[i], out DateTime temp2))
                            {
                                WriteError("Неверный тип данных", s, i, format.Fields[i].Type, fields[i]);
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
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
                if (!titles[i].ToLower().Contains(format.Fields[i].Name.ToLower()))
                {
                    WriteError("Несоответсвие заголовков", 0, i, format.Fields[i].Name, titles[i]);
                    return false;
                }
            }
            return true;
        }

        private static void WriteError(string textError, int s, int i, string typeFormat, string typeExept)
        {
            Console.WriteLine("{0}\n" +
                "строка: {1} столбец: {2} - Ожидалось {3}, а встречено \"{4}\"",
                textError, s + 1, i + 1, typeFormat, typeExept);
        }
    }
}
