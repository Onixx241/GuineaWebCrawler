using System.Text.RegularExpressions;

public class Crawler
{
    public string Url { get; set; }

    public string CurrentPage { get; set; }

    public List<string> FoundLinks { get; set; }

    public Filter Fil;

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

        //remove all this later on and move it to another function -underneath this

        this.ExtractLinks();

        this.RemoveDuplicates();

        this.Fil = new Filter(this.FoundLinks, this.Url);

        this.Fil.AbsoluteLinks();

        this.Fil.BasicFilter();

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