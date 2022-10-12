using System.Collections.Generic;
using System.Transactions;

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
                var inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    var readerInfo = inputLines[i].Split(";");
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
                Console.WriteLine("not valid");
                return null;
            }
            else
            {
                List<Book> list = new List<Book>();
                var inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    var bookInfo = inputLines[i].Split(";");
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
                var inputLines = input.Split("\n");
                for (int i = 1; i < inputLines.Length; i++)
                {
                    var atcionsInfo = inputLines[i].Split(";");
                    
                    var id = int.Parse(atcionsInfo[0]);
                    var book = int.Parse(atcionsInfo[1]);
                    var reader = int.Parse(atcionsInfo[2]);
                    var date = DateTime.Parse(atcionsInfo[3]);
                    var type = atcionsInfo[4];

                    list.Add(new Action(id, GetBook(book, books), GetReader(reader, readers), date, type));
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
            return null;
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
            return null;
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
