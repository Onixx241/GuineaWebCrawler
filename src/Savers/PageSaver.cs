using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class PageSaver : ISaveResults
{
    public void SaveHTMLToFile(string page, int PageNum)
    {
        string dir = "./pagesaves/";

        Directory.CreateDirectory(dir);

        using (StreamWriter Sw = new StreamWriter($"{dir}Output{PageNum}.html"))
        {
            Sw.Write(page);
        }

    }

    public void SaveResults(HashSet<string> VisitedLinks) 
    {
        using (StreamWriter Sw = new StreamWriter("Summary.txt")) 
        {

            Sw.Write("--------------Links Crawled------------\n" +
                    "----------------------------------------\n");
            foreach (string link in VisitedLinks) 
            {
                Sw.WriteLine($"--------------------\n{link}\n");
            }

            Sw.WriteLine($"File Created At {DateTime.Now}");

        }
    }

    public void ClearFolder() 
    {
        foreach(string filee in Directory.GetFiles("./pagesaves/")) 
        {
            File.Delete(filee);
        }
    }

}
