using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        //actual removal part
        foreach (int dex in slatedRemoval) 
        {
            this.Links.RemoveAt(dex);
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
}
