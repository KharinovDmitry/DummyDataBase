using Newtonsoft.Json.Linq;

namespace DummyDataBase
{
    public class Table
    {
        public string Name { get; }
        public List<Row> Rows { get; }
        private TableScheme scheme { get; }

        public Table(TableScheme scheme, string csv)
        {
            this.scheme = scheme; 
            Rows = CsvParser.ParseCsv(csv, scheme);
            Name = scheme.TableName;
        }

        public void Print()
        {
            Console.WriteLine(Name);
            foreach (var column in scheme.Columns)
            {
                Console.Write(column.Name + " ");
            }
            Console.WriteLine();
            foreach (var row in Rows)
            {
                foreach (var data in row.Data)
                {
                    Console.Write(data.Value + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Table GetTableByName(string name, List<Table> tables)
        {
            foreach(var table in tables)
            {
                if(table.Name == name)
                {
                    return table;
                }
            }
            throw new ArgumentException($"Несуществующая таблица: {name}");
        }

        public object GetElementFromTable(string key, Table table)
        {
            foreach (var row in table.Rows)
            {
                string keyRow = "";
                foreach (var column in row.Data)
                {
                    if(column.Key.isPrimary)
                    {
                        switch(column.Key.Type)
                        {
                            case DataType.String:
                                keyRow += ((string)column.Value);
                                break;
                            case DataType.Int:
                                keyRow += ((int)column.Value).ToString();
                                break;
                            case DataType.Float:
                                keyRow += ((float)column.Value).ToString();
                                break;
                            case DataType.DateTime:
                                keyRow += ((DateTime)column.Value).ToString();
                                break;
                            default:
                                throw new ArgumentException($"Ошибка в {table.Name}:{column.Key.Name}");
                        }
                    }
                }
                if(keyRow == key)
                {
                    return row;
                }
            }
            throw new ArgumentException($"Неизвестный элемент таблицы");
        }

    }

    

    public class Row
    {
        public Dictionary<Column, object> Data { get; }

        public Row()
        {
            Data = new Dictionary<Column, object>();
        }


    }

}
