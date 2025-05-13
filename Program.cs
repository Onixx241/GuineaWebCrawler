public class Program() 
{
    public async static Task Main() 
    {
        Crawler crawl = new Crawler("Insert Page Here");
        await crawl.StartCrawler();
    }
}
