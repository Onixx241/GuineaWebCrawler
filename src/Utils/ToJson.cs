using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class ToJson
{
    public static string Serialize(HashSet<string> LinkList)
    {
        string jsonstring = JsonSerializer.Serialize(LinkList);

        return jsonstring;
    }
}
