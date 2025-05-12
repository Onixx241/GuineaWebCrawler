using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class PageSaver 
{
    public static int PageNum = 0;

    public static void SaveHTMLToFile(string page)
    {

        using (StreamWriter Sw = new StreamWriter($"./pagesaves/Output{PageNum}.html"))
        {
            Sw.Write(page);
        }

        PageNum++;
    }

}
