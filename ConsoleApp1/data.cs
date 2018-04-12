using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ConsoleApp1
{
    public class Data
    {

        public int row_count;
        public int contains_null_count;  
        public List<Column> columns;
        public DataTable Table; 
        public Data()
        {
            this.row_count = 0;
            this.contains_null_count = 0; 
            this.columns = new List<Column>();
            this.Table = new DataTable(); 
        }
        public void add_new_col(string name)
        {
            Table.Columns.Add(name, typeof(string));
            columns.Add(new Column(name,this)); 
        }
        public void add_new_row(List<String> new_row)
        {
            int counter = 0;
            foreach (string entery in new_row)
            {
                this.columns[counter].process_entery(entery);
                counter++; 
            }
            this.Table.Rows.Add(new_row); 
        }
        public void data_summery()
        {
            Console.Write("row count : ");
            Console.Write(this.row_count);
            Console.WriteLine();
            Console.Write("row with missing value(s) count : ");
            Console.Write(this.contains_null_count);
            Console.WriteLine();
            Console.Write("columns count : ");
            Console.Write(this.columns.Count);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Columns summery : ");
            Console.WriteLine();
            foreach (Column col in this.columns)
            {
                col.column_summery(); 
            }
        }
    }
}