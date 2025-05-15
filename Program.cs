public class Program() 
{
    public static string url { get; set; }
    //make a reader for a text file and make it a variable and stick it into the crawler constructor

    public static string ReadUrl() 
    {
        using (StreamReader sr = new StreamReader("Urlhere.txt")) 
        {
            url = sr.ReadToEnd();
            Console.WriteLine(url);
        }

        return url;
    }

    public async static Task Main() 
    {
        PageSaver.ClearFolder();

        //url, crawllimit, samedomainmode
        Crawler crawl = new Crawler(ReadUrl(), 25, true);
        await crawl.StartCrawler();

        //write summary
        PageSaver.WriteSummary(crawl.VisitedLinks);
    }

    //Next:
    //Obey Robots.txt
    //multi link input from urlhere.txt
    //export to database
    //export to json
}
