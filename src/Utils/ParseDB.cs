using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParseDB 
{
    public void CheckFileForDatabase() 
    {
        if (!File.Exists("./DBaccess.txt")) 
        {
            Console.WriteLine("Database info not in file !");
            File.Create("./DBaccess.txt");

            using (StreamWriter sw = new StreamWriter("./DBaccess.txt")) 
            {
                //maybe use streamreader.
            }
        }


    }
    public void ParseFileForDatabase() { }
}