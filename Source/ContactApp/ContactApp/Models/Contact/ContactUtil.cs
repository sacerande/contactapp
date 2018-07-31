using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace ContactApp.Models
{
    public class ContactUtil
    {
        public static void ThrowHttpResponseException(String content, HttpStatusCode statusCode, String ReasonPhrase)
        {
            throw new HttpResponseException(
                    new HttpResponseMessage()
                    {
                        Content = new StringContent(content),
                        StatusCode = statusCode,
                        ReasonPhrase = ReasonPhrase
                    }
                    );
        }
    }
}