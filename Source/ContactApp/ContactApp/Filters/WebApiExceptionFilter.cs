using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Filters;
using System.Net.Http;

namespace ContactApp.Api.Filters
{
    public class RestAPIGlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception ex = context.Exception;
            if (ex is HttpResponseException)
            { 
                throw (HttpResponseException)ex; 
            }
            else
            {
                HttpResponseMessage msg = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                msg.Content = new StringContent("Internal Server Error - " + ex.Message);
                msg.ReasonPhrase = "Server Error Occured";
                HttpResponseException responsexception = new HttpResponseException(msg);
                throw responsexception;
            }
        }
    }
}