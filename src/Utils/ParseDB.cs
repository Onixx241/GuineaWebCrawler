using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParseDB 
{
    public string Dbfile { get; set; }

    public string Datasource { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string InitialCatalog { get; set; }

    public bool IntegratedSecurity { get; set; }

    public bool CheckForDatabaseInfo() 
    {
        if (!File.Exists("./DBaccess.txt")) 
        {
            Console.WriteLine("Database info not in file !");

            using (FileStream fs = File.Create("./DBaccess.txt")) { }

            using (StreamWriter sw = new StreamWriter("./DBaccess.txt")) 
            {
                sw.WriteLine("DataSource-\nUser-\nPassword-\nInitialCatalog-\nIntegratedSecurity-\n\nMake sure there's a space between the hyphen and your info, integrated security should be true or false.");
            }
            Environment.Exit(1);
            return false;
        }

        using (StreamReader sr = new StreamReader("./DBaccess.txt")) 
        {
            this.Dbfile = sr.ReadToEnd();
            Console.WriteLine(this.Dbfile);
        }
        return true;


    }
    public void ParseFileForDatabase() 
    {
        foreach (string line in this.Dbfile.Split("\n")) 
        {
            string[] parts = line.Split(" ");
            if (parts.Length > 1)
            {
                string argument = parts[1];
                if (!string.IsNullOrWhiteSpace(argument))
                {
                    if (line.Contains("DataSource-"))
                    {
                        this.Datasource = argument;
                    }
                    if (line.Contains("User-"))
                    {
                        this.User = argument;
                    }
                    if (line.Contains("Password-"))
                    {
                        this.Password = argument;
                    }
                    if (line.Contains("InitialCatalog-"))
                    {
                        this.InitialCatalog = argument;
                    }
                    if (line.Contains("IntegratedSecurity-"))
                    {
                        if (bool.TryParse(argument, out bool integrated))
                        {
                            this.IntegratedSecurity = integrated;
                        }
                    }
                }
            }
        }
    }
}