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

        Crawler crawl = new Crawler(ReadUrl(), 10);
        await crawl.StartCrawler();

        //write summary
        PageSaver.WriteSummary(crawl.VisitedLinks);
    }

    //Next:
    //export to json
    //export to database
    //Same Domain Only <-- up now
}
