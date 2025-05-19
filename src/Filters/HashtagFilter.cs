using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HashtagFilter : ILinkFilter 
{
    public bool ShouldAllow(string link) 
    {
        return !link.StartsWith("#");
    }
}
