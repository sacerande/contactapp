using System;
using System.Web.Mvc;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace ContactApp.Mvc.Filters
{
    public class GlobalExceptionHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true) return;
            
            filterContext.ExceptionHandled = true;

            Exception ex = filterContext.Exception;
            var result = new ViewResult() {
                ViewName = "Error"
            };
            result.ViewBag.error = ex.Message;
            filterContext.Result = result; 
        }
    }
}
