using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieCase.Entities.Search;

namespace TTMovieCase.Utilities.CookieFilters
{
    public interface ICookieManager
    {
        List<SearchListVM> AddSearchList(string searchMovie);
        List<SearchListVM> GetSearchList();
    }
}
