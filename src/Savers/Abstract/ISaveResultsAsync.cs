using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISaveResultsAsync 
{
    public Task SaveResultsAsync(HashSet<string> linklist);
}
