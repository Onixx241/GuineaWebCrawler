using System.Runtime.CompilerServices;

public class Program() 
{
    //make a reader for a text file and make it a variable and stick it into the crawler constructor

    public static string ReadUrl() 
    {
        string url;
        using (StreamReader sr = new StreamReader("Urlhere.txt")) 
        {
            url = sr.ReadToEnd().TrimEnd('/');
        }

        return url;
    }

    public async static Task Main() 
    {
        PageSaver.ClearFolder();

        Console.WriteLine("Parsing");
        
        //url, crawllimit, samedomainmode
        Crawler crawl = new Crawler(ReadUrl(), 25, true);

        //parse robots.txt
        crawl.manager = new RobotsTxtManager(ReadUrl());
        await crawl.manager.GrabRobotsAsync();
        crawl.manager.ParseRobots();

        //add filters
        crawl.Filters.Add(new MailtoFilter());
        crawl.Filters.Add(new HashtagFilter());

        await crawl.StartCrawler();
        
        //write summary
        PageSaver.WriteSummary(crawl.VisitedLinks);
        PageSaver.WriteSummaryAsJson(crawl.VisitedLinks);
    }

    //Next:
    //toggle saving html and deleting it for extended crawling and memory saving
    //exclude favicon links and stuff like that 
    //multi link input from urlhere.txt
    //export to database
    //CLI flags -export as txt or json, -same domain crawl mode -url
    //Obey Robots.txt <-- up now 
}
