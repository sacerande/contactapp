using System.Web;
using System.Web.Mvc;
using ContactApp.Mvc.Filters;

namespace ContactApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //global mvc filter to redirect exceptions to error page.
            filters.Add(new GlobalExceptionHandler());
        }
    }
}