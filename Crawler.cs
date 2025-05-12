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

    public List<List<string>> Sublinks { get; set; }
    public Queue<string> CrawlQueue { get; set; }

    public Filter Fil;

    public Crawler(string url)
    {
        this.Url = url;
        this.CurrentPage = "";
        this.CrawlQueue = new Queue<string>();

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

        this.Fil = new Filter(ExtractLinks(this.CurrentPage), this.Url);

        this.Fil.RunFilters();

        //continue here make it re

        this.PrintLinks();

        return this.Fil.Links;

    }

    

    public void PostDownload() 
    {
    }



    public async Task crawl() 
    {
        //populate queue
        //queue all
        //visit sub links 
        //return sub links from first link 
        //put sublinks somewhere
        //dequeue visited links and go to next link
        foreach (string links in //????) 
        {
            this.CrawlQueue.Enqueue(links);
        }

        foreach (string nextlink in CrawlQueue) 
        {
            await DownloadPage(nextlink);



            this.CrawlQueue.Dequeue();
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

    public void PrintLinks()
    {
        foreach (string list in //add new way of getting link here since foundlinks is gone)
        {
            Console.WriteLine($"------------------------\n{list}");
        }
    }

    public async Task StartCrawler()
    {
        //first one
        await DownloadPage(this.Url);

        //crawl
        await crawl();
    }

}