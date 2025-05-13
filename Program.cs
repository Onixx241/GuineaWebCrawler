public class Program() 
{
    public static string url { get; set; }
    //make a reader for a text file and make it a variable and stick it into the crawler constructor
    public static void ReadUrl() 
    {
        using (StreamReader sr = new StreamReader("Urlhere.txt")) 
        {
            url = sr.ReadToEnd();
            Console.WriteLine(url);
        }
    }
    public async static Task Main() 
    {
        ReadUrl();
        Crawler crawl = new Crawler(url);
        await crawl.StartCrawler();
    }
}
