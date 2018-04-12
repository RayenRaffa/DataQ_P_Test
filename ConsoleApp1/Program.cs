using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace ConsoleApp1
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }
               
                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else 
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {

            // Read sample data from CSV file
            Data my_data = new Data();
            using (CsvFileReader reader = new CsvFileReader("C:/Users/moataz/Desktop/hello.csv"))
            {
                CsvRow row = new CsvRow();
                reader.ReadRow(row); 
                foreach (string col_name in row) // suppose that the first row is the cols name !
                {
                    my_data.add_new_col(col_name); 
                }
                while (reader.ReadRow(row))
                {
                    //int counter = 0; // helps to dectect null values in the row
                    int row_contain_null_val = 0;
                    my_data.row_count++; 
                    List<string> new_row = new List<string>()  ; 
                    for(int counter = 0; counter < my_data.columns.Count; counter++)
                    {
                        string entery; 
                        if (counter < row.Count)
                        {
                            entery = row[counter]; 
                        }
                        else
                        {
                            entery = "";
                            row_contain_null_val = 1; 
                        }
                        new_row.Add(entery);
                        my_data.contains_null_count += row_contain_null_val; 
                    }
                    my_data.add_new_row(new_row); 
                }
            }    
               
            my_data.data_summery(); 
            }
    }
}
