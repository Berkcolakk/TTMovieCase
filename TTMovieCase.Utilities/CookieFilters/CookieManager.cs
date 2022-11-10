using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieCase.Entities.Search;

namespace TTMovieCase.Utilities.CookieFilters
{
    public class CookieManager : TTMovieCase.Utilities.CookieFilters.ICookieManager
    {
        private IHttpContextAccessor ContextAccessor;
        public CookieManager(IHttpContextAccessor ContextAccessor)
        {
            this.ContextAccessor = ContextAccessor;
        }

        public List<SearchListVM> AddSearchList(string searchMovie)
        {
            List<SearchListVM> searchListVMs = new();
            SearchListVM searchListVM = new();
            string keywords = ContextAccessor.HttpContext.Request.Cookies.Where(x => x.Key == "SearchKeywords").FirstOrDefault().Value;
            if (keywords is not null)
            {
                searchListVMs = JsonConvert.DeserializeObject<List<SearchListVM>>(ContextAccessor.HttpContext.Request.Cookies["SearchKeywords"]);
                if (searchListVMs.Count > 4)
                {
                    searchListVMs.RemoveAt(4);
                }
                if (searchListVMs.Where(x => x.SearchData == searchMovie).FirstOrDefault() is not null)
                {
                    return searchListVMs;
                }
            }
            searchListVM.SearchData = searchMovie;
            searchListVMs.Add(searchListVM);
            ContextAccessor.HttpContext.Response.Cookies.Delete("SearchKeywords");
            CookieOptions cookie = new();
            cookie.Expires = DateTime.Now.AddYears(1);
            ContextAccessor.HttpContext.Response.Cookies.Append("SearchKeywords", JsonConvert.SerializeObject(searchListVMs), cookie);
            return searchListVMs;
        }
        public List<SearchListVM> GetSearchList()
        {
            string keywords = ContextAccessor.HttpContext.Request.Cookies.Where(x => x.Key == "SearchKeywords").FirstOrDefault().Value;
            if (keywords is null)
            {
                return null;
            }
            List<SearchListVM> searchListVMs = JsonConvert.DeserializeObject<List<SearchListVM>>(ContextAccessor.HttpContext.Request.Cookies["SearchKeywords"]);
            return searchListVMs;
        }
    }
}
