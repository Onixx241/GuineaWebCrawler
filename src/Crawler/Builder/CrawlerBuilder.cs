using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class CrawlerBuilder() 
{
    //properties
    public string Url { get; set; }
    public int CrawlLimit { get; set; }
    public bool SameDomain { get; set; }
    public SaveMode Mode { get; set; }
    public List<ILinkFilter> Filters { get; set; }

    public PageSaver newSaver { get; set; } = new PageSaver();

    public CrawlerBuilder SetUrl(string url) 
    {
        this.Url = url;
        return this;
    }
    public CrawlerBuilder SetLimit(int limit) 
    {
        this.CrawlLimit = limit;
        return this;
    }
    public CrawlerBuilder SetDomainMode(bool domainmode) 
    {
        this.SameDomain = domainmode;
        return this;
    }
    public CrawlerBuilder SetSaveMode(SaveMode mode) 
    {
        this.Mode = mode;
        return this;
    }
    public CrawlerBuilder AddFilter(ILinkFilter filter) 
    {
        this.Filters.Add(filter);
        return this;
    }
    public CrawlerBuilder ClearFolder()
    {
        PageSaver clear = new PageSaver();
        clear.ClearFolder();
        return this;
    }
    public Crawler BuildCrawler() 
    {
        Crawler newCrawl = new Crawler(this.Url, this.CrawlLimit, this.SameDomain);
        newCrawl.manager = new RobotsTxtManager(this.Url);
        newCrawl.manager.GrabRobotsAsync();
        newCrawl.manager.ParseRobots();

        foreach(ILinkFilter filter in this.Filters) 
        {
            newCrawl.Filters.Add(filter);
        }

        return newCrawl;
    }

}