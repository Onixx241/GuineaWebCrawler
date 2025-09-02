using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        try
        {
            //get robots.txt, store it
            HttpClient get = new HttpClient();
            var response = await get.GetAsync(new Uri(this.TxtLocation));
            get.Dispose();

            this.Robotstxt = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error grabbing robots.txt: {ex.Message}");
            this.Robotstxt = ""; // or handle accordingly
        }

    }

    //doesnt have to be async we're not doing any async stuff
    public void ParseRobots() 
    {
        Console.WriteLine("Parsing Now:");
        //parse here then move all relevant stuff to global variable for easy access to feed it into the crawler 

        foreach (string line in this.Robotstxt.Split("\n"))
        {
            string currentline = line.Trim();
            if (currentline.Contains("Disallow:")) 
            {
                string[] parts = currentline.Split(" ");
                if (parts.Length > 1)
                {
                    string path = parts[1].Trim();
                    this.Disallowed.Add(path);
                    Console.WriteLine($"not allowed --> {path}");
                }
            }

            if (currentline.Contains("Crawl-delay:"))
            {
                string[] parts = currentline.Split(" ");
                if (parts.Length > 1)
                {
                    if (int.TryParse(parts[1].Trim(), out int delay))
                    {
                        this.CrawlDelay = delay;
                        Console.WriteLine($"the delay on the site -> {this.CrawlDelay}");
                    }
                }
            }
            

        }
    }
}
