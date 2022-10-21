using Newtonsoft.Json.Linq;
using System.Data.Common;
using System.Diagnostics.Contracts;

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

        public void Print(int count, List<Table> tables)
        {
            Console.WriteLine(Name);
            Console.WriteLine();
            foreach (var row in Rows)
            {
                row.Print(count, tables);
            }
            Console.WriteLine();
        }

        public static Table GetTableByName(string name, List<Table> tables)
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

        public Row GetElement(int key)
        {
            foreach (var row in Rows)
            {
                string keyRow = "";
                foreach (var column in row.Data)
                {
                    if(column.Key.isPrimary)
                    {
                        keyRow += ((int)column.Value).ToString();
                    }
                }
                if(int.Parse(keyRow) == key)
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

        private static void CreateTab(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write("\t");
            }
        }
        public void Print(int count, List<Table> tables)
        {
            foreach (var data in Data)
            {
                if (data.Key.ReferencedTable != null)
                {
                    Console.WriteLine($"{data.Key.Name}:");
                    Table refTable = Table.GetTableByName(data.Key.ReferencedTable, tables);
                    CreateTab(count);
                    refTable.GetElement((int)data.Value).Print(count + 1, tables);
                }
                else
                {
                    CreateTab(count);
                    Console.WriteLine($"{data.Key.Name}:{data.Value}");
                }
            }
            Console.WriteLine();
        }
    }

}
