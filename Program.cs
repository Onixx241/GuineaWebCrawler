using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;
using System.Xml;

public class Program() 
{
    public async static Task Main() 
    {
        Crawler crawl = new Crawler("https://handbook-for-cuny-hunter-cs-students.webnode.page");
        await crawl.StartCrawler();
    }
}
