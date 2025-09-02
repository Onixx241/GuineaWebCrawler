using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ToUrl() 
{
    public static string ReadUrl()
    {
        string url;
        if (File.Exists("Urlhere.txt"))
        {
            using (StreamReader sr = new StreamReader("Urlhere.txt"))
            {
                url = sr.ReadToEnd().TrimEnd('/');
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("Urlhere.txt is empty. Please provide a valid URL.");
                return "";
            }

            return url;
        }
        else 
        {
            Console.WriteLine("Urlhere.txt does not exist and must be filled!");
            File.Create("Urlhere.txt");
            return "";  
        }
    }
}