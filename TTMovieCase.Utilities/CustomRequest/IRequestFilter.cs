using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieCase.Utilities.CustomRequest
{
    public interface IRequestFilter
    {
        string GetRequest(Dictionary<string, string>? Params, string CustomHostName);
    }
}
