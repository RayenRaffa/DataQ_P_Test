using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Column
    {
        public int type; // various types
        public string name;
        public Dictionary<string, int> word_count;
        public List<string> content;
        public Data source;
        public int[] type_count;
        public Column(string name,Data origin)
        {
            this.type = 0; // standard type : string  
            this.name = name;
            this.source = origin;
            this.content = new List<string>();
            this.word_count = new Dictionary<string, int>();
            this.type_count = new int[4];
            for (int i = 0; i < 4; i++)
            {
                this.type_count[i] = 0;
            }
        }
        public void process_entery(string entery)
        {

            if (!this.word_count.ContainsKey(entery)) // updating word count 
            {
                this.word_count[entery] = 1;
            }
            else
            {
                this.word_count[entery]++;
            }

            int n;
            Double x;
            DateTime temp;
            if (int.TryParse(entery, out n))
            {
                this.type_count[0]++;
            }
            else
            {
                if (double.TryParse(entery, out x))
                {
                    this.type_count[1]++;
                }
                else
                {
                    if (DateTime.TryParse(entery, out temp)) // this is just a sample and will be replaced by an algorithm for normalizing dates 
                    {
                        this.type_count[2]++;
                    }
                    else
                    {
                        if (entery != "")
                             this.type_count[3]++; // just a string 
                    }
                }
            }

        }
        public void column_summery()
        {
            Console.Write("     Column name : ");
            Console.Write(this.name);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("         Word count");
            Console.WriteLine();
            Console.WriteLine();
            if (this.word_count.Count == 1)
            {
                Console.Write("              ");
                Console.Write("this column is empty !");
                Console.WriteLine();
                Console.WriteLine();

            }
            else
            {
                foreach (string s in this.word_count.Keys)
                {
                    if (s != "")
                    {
                        Console.Write("              ");
                        Console.Write(s);
                        Console.Write(" : ");
                        Console.Write(this.word_count[s]);
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.Write("         Missing values count : ");
                Console.Write(word_count[""]);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("         type count");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("              integers : ");
                Console.Write(this.type_count[0]);
                Console.WriteLine();
                Console.Write("              doubles : ");
                Console.Write(this.type_count[1]);
                Console.WriteLine();
                Console.Write("              dates : ");
                Console.Write(this.type_count[2]);
                Console.WriteLine();
                Console.Write("              strings : ");
                Console.Write(this.type_count[3]);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
