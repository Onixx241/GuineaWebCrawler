using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class PageSaver 
{
    public static int PageNum = 1;

    public static void SaveHTMLToFile(string page)
    {
        string dir = "./pagesaves/";

        Directory.CreateDirectory(dir);

        using (StreamWriter Sw = new StreamWriter($"{dir}Output{PageNum}.html"))
        {
            Sw.Write(page);
        }

        PageNum++;
    }

    public static void ClearFolder() { }

}
