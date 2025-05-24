using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CrawlConfig 
{
    public string Url { get; set; } = ToUrl.ReadUrl();
    public int CrawlLimit { get; set; }
    public bool SameDomain { get; set; }

    public CrawlConfig() { }
}
