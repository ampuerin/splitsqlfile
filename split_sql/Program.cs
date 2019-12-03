using System;
using System.IO;
using System.Linq;

namespace split_sql
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Splitting file 'script.sql' in 9500 lines separate files . . .");
            try
            {
                if (File.Exists("script.sql"))
                {
                    string[] ss = File.ReadAllLines("script.sql");
                    ss = ss.Skip(1).ToArray();

                    int max_lines = 9500;
                    int batch = 1;

                    var chunk = ss.Take(max_lines);
                    var rem = ss.Skip(max_lines);

                    while (chunk.Take(1).Count() > 0)
                    {
                        String filename = "script_part_" + batch.ToString() + ".sql";
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            foreach (string line in chunk)
                            {
                                sw.WriteLine(line);
                            }
                        }
                        chunk = rem.Take(max_lines);
                        rem = rem.Skip(max_lines);
                        batch++;
                    }
                }
                else
                {
                    Console.WriteLine("Not 'script.sql' found in this folder");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
