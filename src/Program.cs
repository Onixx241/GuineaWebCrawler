using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Program() 
{

    public async static Task Main(string[] args) 
    {
        CrawlConfig config = new CrawlConfig();
        ParseArgs.Args(args, config);


        PageSaver newSaver = new PageSaver();
        newSaver.ClearFolder();

        Console.WriteLine("Parsing");

        //url, crawllimit, samedomainmode -- //config.url, config.crawllimit, config.samedomain
        Crawler crawl = new Crawler(config.Url, config.CrawlLimit, config.SameDomain);

        //parse robots.txt
        crawl.manager = new RobotsTxtManager(config.Url);
        await crawl.manager.GrabRobotsAsync();
        crawl.manager.ParseRobots();

        //add filters
        crawl.Filters.Add(new MailtoFilter());
        crawl.Filters.Add(new HashtagFilter());
        crawl.Filters.Add(new FaviconFilter());

        await crawl.StartCrawler();

        //write summary
        //newSaver.SaveResults(crawl.VisitedLinks);
                                                                                           //integrated security bool
        DatabaseSaver newdb = new DatabaseSaver("Datasource", "User","Password","Catalog", true);
        newdb.SaveResults(crawl.VisitedLinks);
    }

    //Next:
    //implement builder pattern.    
    //toggle saving html and deleting it for extended crawling and memory saving
    //multi link input from urlhere.txt
    //export to database <- up now
}
