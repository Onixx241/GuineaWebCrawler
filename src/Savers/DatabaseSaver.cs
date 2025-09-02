﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class DatabaseSaver : ISaveResultsAsync
{
    //maybe read from a txt file for someone to be able to put their own info in?
    //connect to db, for each link add with value (cmd.params.addwithvalue)
    //if table doesnt exist in db make one.
    public string Datasource { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }
    public string initcatalog { get; set; }
    public bool IntegratedSecurity { get; set; }

    public DatabaseSaver(string source, string user, string pass, string initialcatalog, bool integratedSec) 
    {
        this.Datasource = source;
        this.User = user;
        this.Pass = pass;
        this.initcatalog = initialcatalog;
        this.IntegratedSecurity = integratedSec;
    }

    public SqlConnectionStringBuilder CreateConnectionString() 
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = this.Datasource,
            UserID = this.User,
            Password = this.Pass,
            IntegratedSecurity = this.IntegratedSecurity,
            InitialCatalog = this.initcatalog,
            TrustServerCertificate = true
        };

        return builder;
    }

    public void CreateTable() 
    {
        var builder = CreateConnectionString();

        using (SqlConnection sql = new SqlConnection(builder.ConnectionString)) 
        {
            sql.Open();

            using (SqlCommand cmd = new SqlCommand()) 
            {
                cmd.Connection = sql;
                //domain | link | time added
                cmd.CommandText = "IF OBJECT_ID('Linktable', 'U') IS NULL " + "CREATE TABLE Linktable(Domain VARCHAR(150), Link VARCHAR(MAX), DateAdded DATE)"; ;

                cmd.ExecuteNonQuery();
            }

            sql.Close();
        }
    }
        //consider batch transaction.
    public async Task SaveResultsAsync(HashSet<string> LinkList) 
    {

        CreateTable();

        var builder = CreateConnectionString();

        string command = $"INSERT INTO Linktable(Domain, Link, DateAdded) VALUES(@Domain, @Link, @DateAdded)";

        DateTime date = DateTime.Now;
        string PassableDate = $"{date.Year}-{date.Month}-{date.Day}";

        using (SqlConnection sql = new SqlConnection(builder.ConnectionString)) 
        {

            sql.Open();
            foreach (string link in LinkList)
            {
                try
                {
                    Uri uri = new Uri(link);
                    string domain = uri.Host;

                    using (SqlCommand cmd = new SqlCommand(command, sql))
                    {

                        cmd.Parameters.AddWithValue("@Domain", domain);
                        cmd.Parameters.AddWithValue("@Link", link);
                        cmd.Parameters.AddWithValue("@DateAdded", PassableDate);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (UriFormatException ex)
                {
                    Console.WriteLine($"Invalid URL: {link}, skipping. Error: {ex.Message}");
                }
            }
        }
    }

}