using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RobotsTxtManager
{
    public string TxtLocation { get; set; }
    public string Robotstxt { get; set; }

    public List<string> Disallowed = new List<string>();

    public int CrawlDelay = 0;

    public RobotsTxtManager(string BaseA) 
    {
        this.TxtLocation = BaseA + "/robots.txt";
    }

    public async Task GrabRobotsAsync() 
    {
        Console.WriteLine("Grabbing Robots.txt");

        //get robots.txt, store it
        HttpClient get = new HttpClient();
        var response = await get.GetAsync(new Uri(this.TxtLocation));
        get.Dispose();

        this.Robotstxt = await response.Content.ReadAsStringAsync();

    }

    //doesnt have to be async we're not doing any async stuff
    public void ParseRobots() 
    {
        Console.WriteLine("Parsing Now:");
        //parse here then move all relevant stuff to global variable for easy access to feed it into the crawler 

        foreach (string line in this.Robotstxt.Split("\n"))
        {
            string currentline = line;
            if (currentline.Contains("Disallow:")) 
            {
                //decent solution i think 
                currentline = currentline.Split(" ")[1];

                currentline = currentline.Trim();

                this.Disallowed.Add(currentline);

                Console.WriteLine($"not allowed --> {currentline}");

            }

            if (currentline.Contains("Crawl-delay:"))
            {
                currentline = currentline.Split(" ")[1];

                this.CrawlDelay = int.Parse(currentline);

                Console.WriteLine($"the delay on the site -> {this.CrawlDelay}");
            }
            

        }
    }
}
