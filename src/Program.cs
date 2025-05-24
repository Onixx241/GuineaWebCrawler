using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Program() 
{
    //make a reader for a text file and make it a variable and stick it into the crawler constructor

    public async static Task Main(string[] args) 
    {
        CrawlConfig config = new CrawlConfig();
        ParseArgs.Args(args, config);


        PageSaver newSaver = new PageSaver();
        newSaver.ClearFolder();

        Console.WriteLine("Parsing");

        //url, crawllimit, samedomainmode
        Crawler crawl = new Crawler(config.Url, config.CrawlLimit, config.SameDomain);

        //parse robots.txt
        crawl.manager = new RobotsTxtManager(config.Url);
        await crawl.manager.GrabRobotsAsync();
        crawl.manager.ParseRobots();

        //add filters
        crawl.Filters.Add(new MailtoFilter());
        crawl.Filters.Add(new HashtagFilter());

        await crawl.StartCrawler();

        //write summary
        newSaver.SaveResults(crawl.VisitedLinks);
    }

    //Next:
    //toggle saving html and deleting it for extended crawling and memory saving
    //exclude favicon links and stuff like that 
    //multi link input from urlhere.txt <-- Next
    //export to database
}
