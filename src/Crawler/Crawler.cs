using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

public class Crawler
{
    public string Url { get; set; }
    public string Domain { get; set; }

    public string CurrentUrl { get; set; }

    public string initialPage { get; set; }
    public string CurrentPage { get; set; }


    public int CrawlLimit { get; set; }
    public int PageCount;

    public RobotsTxtManager manager { get; set; }

    public bool SameDomainMode { get; set; }

    public Queue<string> CrawlQueue { get; set; }
    public HashSet<string> VisitedLinks { get; set; }


    public List<string> NewLinks { get; set; }

    public Filter Fil;

    public List<ILinkFilter> Filters = new List<ILinkFilter>();

    public Crawler(string url, int limit, bool domainmode)
    {
        this.Url = url;
        this.CurrentPage = "";
        this.CrawlLimit = limit;
        this.PageCount = 0;
        this.SameDomainMode = domainmode;

        this.CrawlQueue = new Queue<string>();
        this.VisitedLinks = new HashSet<string>();
        this.NewLinks = new List<string>();
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

        PageSaver newpage = new PageSaver();
        newpage.SaveHTMLToFile(this.CurrentPage, this.PageCount);
        this.PageCount++;

        HashSet<string> LinkList = ExtractLinks(this.CurrentPage);

        this.Fil = new Filter(LinkList, this.Url);

        this.Fil.ProcessLinks();

        return this.Fil.Links;

    }


    public async Task CrawlAsync() 
    {
        //crawl seed url
        List<string> Firstlinks = await DownloadPage(this.Url);

        int crawlcounter = 0;

        foreach (string link in Firstlinks) 
        {
            if (PassesAllFilters(link) == true)
                this.CrawlQueue.Enqueue(link);
        }

        //while queue count is not 0 
        while (this.CrawlQueue.Count > 0) 
        {
            //crawl delay
            await Task.Delay(this.manager.CrawlDelay);

            //pop out url from front of queue and check if its in visited links.
            string currenturl = CrawlQueue.Dequeue();

            if (this.VisitedLinks.Contains(currenturl) != true && this.manager.Disallowed.Contains(currenturl) != true) 
            {
                // link in front of queue -> add to visited links AND run downloadpage and get new links.
                //possible issue(not really an issue): absoluting links
                //same domain possible solution: if current url is not in the same domain, do not add it to visited links and do not make new links from it.
                if (this.SameDomainMode == true)
                {
                    if (currenturl.Contains(this.Url))
                    {
                        this.VisitedLinks.Add(currenturl);
                        this.NewLinks = await DownloadPage(currenturl);
                    }
                }
                if (this.SameDomainMode == false) 
                {
                    this.VisitedLinks.Add(currenturl);
                    this.NewLinks = await DownloadPage(currenturl);
                }

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

                foreach (string link in this.NewLinks) 
                {

                    if (this.VisitedLinks.Contains(link))
                        continue;


                    if (PassesAllFilters(link))
                        CrawlQueue.Enqueue(link);
                    

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

    private bool PassesAllFilters(string link) 
    {
        foreach (var filter in this.Filters) 
        {
            if (filter.ShouldAllow(link) != true)
                return false;
        }

        return true;
    }

    public async Task StartCrawler()
    {
        //crawl
        await CrawlAsync();
    }

}