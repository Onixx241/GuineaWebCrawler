using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

public class    FaviconFilter : ILinkFilter 
{
    public bool ShouldAllow(string link) 
    {
        return !link.Contains("/favicons/");
    }
}