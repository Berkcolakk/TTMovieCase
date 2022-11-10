using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieCase.Utilities.CustomRequest
{
    public class RequestFilter : IRequestFilter
    {
        private readonly IConfiguration configuration;
        public RequestFilter(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        private readonly HttpClient client = new();
        public string GetRequest(Dictionary<string, string>? Params, string CustomHostName)
        {
            string URL = $"{configuration.GetSection("APIHostName").Value}{CustomHostName}";
            URL = QueryHelpers.AddQueryString(URL, "api_key", configuration.GetSection("ApiKey").Value);
            if (Params is not null)
            {
                foreach (var item in Params)
                {
                    URL = QueryHelpers.AddQueryString(URL, item.Key, item.Value);
                }
            }
            Task<string> response = client.GetStringAsync($"{URL}");
            return response.Result;
        }
    }
}
