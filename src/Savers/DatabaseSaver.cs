using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class DatabaseSaver
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
                cmd.CommandText = "IF OBJECT_ID('Linktablet', 'U') IS NULL " + "CREATE TABLE Linktable(Link VARCHAR(MAX))"; ;

                cmd.ExecuteNonQuery();
            }

            sql.Close();
        }
    }
    public void SaveResults(HashSet<string> LinkList) 
    {
        //this does not work when it has been ran more than one time , how can i check if there is already a similar table . <---- now
        CreateTable();

        var builder = CreateConnectionString();

        string command = $"INSERT INTO Linktable(Link) VALUES(@Link)";

        using (SqlConnection sql = new SqlConnection(builder.ConnectionString)) 
        {

            sql.Open();
            foreach (string link in LinkList)
            {

                using (SqlCommand cmd = new SqlCommand(command, sql))
                {

                    cmd.Parameters.AddWithValue("@Link", link);

                    cmd.ExecuteNonQuery();


                }

            }

            sql.Close();

        }
    }

}