using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParseArgs 
{
    public static void Args(string[] Allargs, CrawlConfig config) 
    {
        for(int i = 0; i < Allargs.Length; i++) 
        {
            switch (Allargs[i])
            {

                case "-url":
                    if (i + 1 < Allargs.Length)
                        config.Url = Allargs[i + 1];
                break;

                case "-limit":
                    if (i + 1 < Allargs.Length)
                        if (int.TryParse(Allargs[i + 1], out int limit))
                            config.CrawlLimit = limit;
                    break;

                case "-dmode":
                    if (i + 1 < Allargs.Length)
                        if (bool.TryParse(Allargs[i + 1], out bool dmode))
                            config.SameDomain = dmode;
                    break;

                case "-json":
                    config.Mode = SaveMode.Json;
                    break;

                case "-db":
                    config.Mode = SaveMode.Database;
                    break;
            }
        }
    }
}