using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;
using System.Xml;

public class Crawler 
{
    public string Url { get; set; }

    public string CurrentPage { get; set; }

    public List<string> FoundLinks { get; set; }

    public Crawler(string url) 
    {
        this.Url = url;
        this.CurrentPage = "";
        this.FoundLinks = new List<string>();
    }

    public async Task DownloadPage() 
    {
        Console.WriteLine("Task Running");

        HttpClient client = new HttpClient();


        var response = await client.GetAsync(this.Url);

        this.CurrentPage = await response.Content.ReadAsStringAsync();

        this.SaveHTMLToFile();

        this.ExtractLinks();

        this.RemoveDuplicates();

        this.PrintLinks();

    }

    public void SaveHTMLToFile() 
    {
        using (StreamWriter Sw = new StreamWriter("./Output.html")) 
        {
            Sw.Write(this.CurrentPage);
        }
    }


    public void RemoveDuplicates()
    {
        List<string> Templinks = new List<string>();

        HashSet<string> Nodupes = new HashSet<string>();

        foreach (string link in this.FoundLinks) 
        {
            Nodupes.Add(link);
        }

        foreach (string ND in Nodupes) 
        {
            Templinks.Add(ND);
        }

        this.FoundLinks = Templinks;
    }

    public void ExtractLinks() 
    {
        Regex FindLinks = new Regex("href=[\"'](?<url>.*?)[\"']", RegexOptions.IgnoreCase);

        MatchCollection matches = FindLinks.Matches(this.CurrentPage);

        foreach (Match link in matches) 
        {
            this.FoundLinks.Add(link.Groups["url"].Value);
        }
    }

    public void PrintLinks() 
    {
        foreach (string list in this.FoundLinks) 
        {
            Console.WriteLine(list);
        }
    }

    public async Task StartCrawler() 
    {
        await DownloadPage();

        ExtractLinks();
    }

}

public class Program() 
{
    public async static Task Main() 
    {
        Crawler crawl = new Crawler("https://handbook-for-cuny-hunter-cs-students.webnode.page");
        await crawl.StartCrawler();
    }
}
