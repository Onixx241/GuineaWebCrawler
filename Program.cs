public class Program() 
{
    public async static Task Main() 
    {
        Crawler crawl = new Crawler("linkhere.com");
        await crawl.StartCrawler();
    }
}
