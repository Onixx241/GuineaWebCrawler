using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Program() 
{

    public async static Task Main(string[] args) 
    {
        CrawlConfig config = new CrawlConfig();
        ParseDB parser = new ParseDB();
        ParseArgs.Args(args, config);

        if (config.Mode == SaveMode.Database)
        {
            parser.CheckForDatabaseInfo();
            parser.ParseFileForDatabase();
            //find a way to pass this to database saver.
        }

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
        crawl.Filters.Add(new FaviconFilter());

        await crawl.StartCrawler();

        //write summary
        newSaver.SaveResults(crawl.VisitedLinks);

        switch (config.Mode) 
        {

            case SaveMode.Database:
                DatabaseSaver newdb = new DatabaseSaver(parser.Datasource, parser.User, parser.Password, parser.InitialCatalog, parser.IntegratedSecurity);
                await newdb.SaveResultsAsync(crawl.VisitedLinks);
                break;

            case SaveMode.Json:
                JsonSaver newJson = new JsonSaver();
                newJson.SaveResults(crawl.VisitedLinks);
                break;

        }

    }

    //Next:
    //add intermediate saving / exporting during program for extended crawl sessions.
    //implement builder pattern.    
    //toggle saving html and deleting it for extended crawling and memory saving
    //multi link input from urlhere.txt
    //refactor program and crawl logic <-- up now
    //export to database <- check
}
