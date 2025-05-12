public class Program() 
{
    public async static Task Main() 
    {
        Crawler crawl = new Crawler("https://handbook-for-cuny-hunter-cs-students.webnode.page");
        await crawl.StartCrawler();
    }
}
