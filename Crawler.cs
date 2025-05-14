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

    public int CrawlLimit { get; set; }

    public Queue<string> CrawlQueue { get; set; }
    public HashSet<string> VisitedLinks { get; set; }

    public Filter Fil;

    public Crawler(string url, int limit)
    {
        this.Url = url;
        this.CurrentPage = "";
        this.CrawlLimit = limit;
        this.CrawlQueue = new Queue<string>();
        this.VisitedLinks = new HashSet<string>();
    }

    public async Task<List<string>> DownloadPage(string todownload)
    {
        if(string.IsNullOrWhiteSpace(todownload)) 
            return new List<string>();
        if (!Uri.IsWellFormedUriString(todownload, UriKind.Absolute)) 
            return new List<string>();
        if(!todownload.StartsWith("Http", StringComparison.OrdinalIgnoreCase)) 
            return new List<string>();

        Console.WriteLine("Crawling Page!");

        HttpClient client = new HttpClient();

        var response = await client.GetAsync(new Uri(todownload));

        this.CurrentPage = await response.Content.ReadAsStringAsync();

        client.Dispose();

        PageSaver.SaveHTMLToFile(this.CurrentPage);

        HashSet<string> LinkList = ExtractLinks(this.CurrentPage);

        this.Fil = new Filter(LinkList, this.Url);

        this.Fil.RunFilters();

        //this.PrintLinks(LinkList);

        return this.Fil.Links;

    }

    

    public void PostDownload() 
    {
    }



    public async Task crawl() 
    {
        //implement crawl limit.
        List<string> Firstlinks = await DownloadPage(this.Url);
        int crawlcounter = 0;

        foreach (string link in Firstlinks) 
        {
            CrawlQueue.Enqueue(link);
        }

        //while queue count is not 0 
        while (this.CrawlQueue.Count > 0) 
        {
            //pop out url from front of queue and check if its in visited links.
            string currenturl = CrawlQueue.Dequeue();

            if (this.VisitedLinks.Contains(currenturl) != true) 
            {
                // link in front of queue -> add to visited links AND run downloadpage and get new links.
                //possible issue(not really an issue): absoluting links
                this.VisitedLinks.Add(currenturl);
                List<string> NewLinks = await DownloadPage(currenturl);


                crawlcounter++;
                foreach (string link in this.VisitedLinks) 
                {
                    Console.WriteLine($"Visited Link: {link}");
                }
                //break between downloading last page and new link enqueueing if reached limit.
                if (crawlcounter == this.CrawlLimit)
                {
                    break;
                }
                //

                foreach (string link in NewLinks) 
                {
                    if (this.VisitedLinks.Contains(link) != true)
                    {
                        this.CrawlQueue.Enqueue(link);
                    }
                }
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