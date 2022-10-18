namespace DummyDataBase
{
    public class Table
    {
        public string Name { get; }
        public List<Row> Rows { get; }
        private TableScheme scheme { get; }

        public Table(TableScheme scheme)
        {
            this.scheme = scheme; 
            Rows = new List<Row>();
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
