using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

//downloadpage -> postdownloadfunc (to view sublinks and store them) -> crawl
//filter is not dynamic since it only takes foundlinks
//stuck on how to implement crawl
//
public class Crawler
{
    public string Url { get; set; }

    public string CurrentUrl { get; set; }

    public string initialPage { get; set; }
    public string CurrentPage { get; set; }

    public List<string> Sublinks { get; set; }
    public Queue<string> CrawlQueue { get; set; }
    public HashSet<string> VisitedLinks { get; set; }

    public Filter Fil;

    public Crawler(string url)
    {
        this.Url = url;
        this.CurrentPage = "";
        this.CrawlQueue = new Queue<string>();
        this.VisitedLinks = new HashSet<string>();

    }

    public async Task<List<string>> DownloadPage(string todownload)
    {
        //break this up in a modular way to crawl

        Console.WriteLine("Crawling Page!");

        HttpClient client = new HttpClient();

        var response = await client.GetAsync(new Uri(todownload));

        this.CurrentPage = await response.Content.ReadAsStringAsync();

        client.Dispose();

        PageSaver.SaveHTMLToFile(this.CurrentPage);

        //remove everything under this later on and move it.
        HashSet<string> LinkList = ExtractLinks(this.CurrentPage);

        this.Fil = new Filter(LinkList, this.Url);

        this.Fil.RunFilters();

        return this.Fil.Links;

    }

    

    public void PostDownload() 
    {
    }



    public async Task crawl() 
    {
        //populate queue
        //visit sub links 
        //return sub links from first link 
        //put sublinks somewhere
        //dequeue visited links and go to next link

        List<string> Firstlinks = await DownloadPage(this.Url);

        foreach (string link in Firstlinks) 
        {
            CrawlQueue.Enqueue(link);
        }


        while (this.CrawlQueue.Count > 0) 
        {
            string currenturl = CrawlQueue.Dequeue();

            if (this.VisitedLinks.Contains(currenturl) != true) 
            {
                this.VisitedLinks.Add(currenturl);
                //Console.WriteLine("Visited Links: " + currenturl);
            }
            else 
            {
                await DownloadPage(currenturl);
            }
        }
    }

    public HashSet<string> ExtractLinks(string page)
    {
        HashSet<string> ExtractedLinks = new HashSet<string>();

        Regex FindLinks = new Regex("href=[\"'](?<url>.*?)[\"']", RegexOptions.IgnoreCase);

        MatchCollection matches = FindLinks.Matches(page);

        foreach (Match link in matches)
        {
            ExtractedLinks.Add(link.Groups["url"].Value);
        }

        return ExtractedLinks;

    }

    public void PrintLinks(HashSet<string> linklist)
    {
        foreach(string list in linklist)
        {
            Console.WriteLine($"------------------------\n{list}");
        }
    }

    public async Task StartCrawler()
    {
        //crawl
        await crawl();
    }

}