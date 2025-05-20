using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class JsonSaver: ISaveResults
{

    public void SaveResults(HashSet<string> list) 
    {
        using (StreamWriter sw = new StreamWriter("JsonSummary.txt")) 
        {
            sw.WriteLine(Serialize(list));
        }
    }
    public string Serialize(HashSet<string> LinkList)
    {
        string jsonstring = JsonSerializer.Serialize(LinkList);

        return jsonstring;
    }
}
