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


    public Filter(List<string> links, string baseA) 
    {
        this.Links = links;
        this.BaseAddress = baseA;
    }

    public void BasicFilter() 
    {
        List<int> slatedRemoval = new List<int>();

        int index = 0;


        // this is not removing Mailto harris, add breakpoint to bracket underneath and check why.
        foreach (string toremove in this.Links) 
        {
            if (toremove.StartsWith("#") || toremove.StartsWith("mailto:")) 
            {
                Console.WriteLine($"--------------------\nfor removal : {toremove}\n-------------------------");

                index = this.Links.IndexOf(toremove);
                slatedRemoval.Add(index);
            }

            index = 0;
        }
        int remcount = 1;
        bool oneremoved = false;

        //actual removal part
        foreach (int dex in slatedRemoval) 
        {
            if (oneremoved == false)
            {
                this.Links.RemoveAt(dex);
                oneremoved = true;
            }

            if(oneremoved == true) 
            {
                this.Links.RemoveAt(dex - remcount);
                remcount++;
            }
        }

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

            if (link.ToCharArray().First() == '/') 
            {
                Console.WriteLine($"Found link that should be added to baseaddress: {link}");
                currentlink = this.BaseAddress + link;
                this.Links[linkpos] = currentlink; 
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
            if (link.Last() == '/') 
            {
                string toreplace = link;

                Console.WriteLine($"Normalizing link: {link}");

                string newlink = link.Remove(link.Length - 1);

                templinks.Remove(link);
                templinks.Add(newlink);

                Console.WriteLine("Normalized link: " + newlink);


            }

        }

        this.Links = templinks;
    }

    public void RunFilters() 
    {
        this.AbsoluteLinks();
        this.BasicFilter();
        this.Normalize();
    }
}
