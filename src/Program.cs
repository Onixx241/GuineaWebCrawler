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
        }

        Crawler crawl = new CrawlerBuilder()
            .SetUrl(config.Url)
            .SetLimit(config.CrawlLimit)
            .SetDomainMode(config.SameDomain)
            .SetSaveMode(config.Mode)
            .AddFilter(new FaviconFilter())
            .AddFilter(new HashtagFilter())
            .AddFilter(new MailtoFilter())
            .ClearFolder()
            .BuildCrawler();


        await crawl.StartCrawler();

        //write summary
        crawl.supportSaver.SaveResults(crawl.VisitedLinks);

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
    //mongodb support
    //add intermediate saving / exporting during program for extended crawl sessions.
    //implement builder pattern.    
    //toggle saving html and deleting it for extended crawling and memory saving
    //multi link input from urlhere.txt
    //refactor program and crawl logic <-- up now
    //export to database <- check
}
