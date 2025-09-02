using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class Filter
{
    public string BaseAddress;
    public List<string> Links { get; set; }


    public Filter(HashSet<string> links, string baseA) 
    {
        this.Links = new List<string>();

        foreach (string link in links) 
        {
            this.Links.Add(link);
            
        }

        this.BaseAddress = baseA;
    }

    
    public void AbsoluteLinks()
    {
        int linkpos = 0;

        List<string> templinks = new List<string>();

        //populate templist
        foreach(string link in this.Links) 
        {
            templinks.Add(link);
        }



        foreach (string link in templinks) 
        {
            string currentlink = link;

            if (!string.IsNullOrEmpty(link) && link.ToCharArray().First() == '/')
            {
                currentlink = this.BaseAddress + link;
                this.Links[linkpos] = currentlink;
            }
            else 
            {
                // keep as is
            }

            linkpos = linkpos + 1;
        }


    }

    //remove trailing slashes
    public void Normalize() 
    {
        List<string> templinks = new List<string>();

        //populate templinks
        foreach (string links in this.Links) 
        {
            templinks.Add(links);
        }

        foreach (string link in this.Links) 
        {
            if (!string.IsNullOrEmpty(link) && link.Last() == '/') 
            {
                string toreplace = link;

                //Console.WriteLine($"Normalizing link: {link}");

                string newlink = link.Remove(link.Length - 1);

                templinks.Remove(link);
                templinks.Add(newlink);

                //Console.WriteLine("Normalized link: " + newlink);


            }
            else 
            {
                continue;
            }

        }

        this.Links = templinks;
    }

    public void ProcessLinks() 
    {
        this.AbsoluteLinks();
        this.Normalize();
    }
}
