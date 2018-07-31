using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace ContactApp.Models
{
    public class WebAPIConsumer
    {
        public static IEnumerable<T> GetAllObjects<T>(String requestUri)
        {
            IEnumerable<T> lstObjects = new List<T>();
            using (var client = new HttpClient())
            {
                //HTTP GET
                var responseTask = client.GetAsync(requestUri);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<T>>();
                    readTask.Wait();

                    lstObjects = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    lstObjects = Enumerable.Empty<T>(); 
                    result.ThrowException();
                }
            }
            return lstObjects;
        }

        public static T GetObject<T>(String requestUri) where T : class
        {
            T obj = null;
            using (var client = new HttpClient())
            {
                //HTTP GET
                var responseTask = client.GetAsync(requestUri);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
                else
                {
                    result.ThrowException();
                }
            }
            return obj;
        }

        public static System.Net.HttpStatusCode CreateObject<T>(String requestUri, T obj)
        {
            using (var client = new HttpClient())
            {
                //HTTP POST
                var postTask = client.PostAsJsonAsync<T>(requestUri, obj);
                postTask.Wait();

                var result = postTask.Result;

                //if web api returns http status code other than created,then throw exception.
                if (result.StatusCode != System.Net.HttpStatusCode.Created) { result.ThrowException(); }

                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                obj = readTask.Result;

                return result.StatusCode;
            }        
        }

        public static System.Net.HttpStatusCode UpdateObject<T>(String requestUri, T obj) where T:class
        {
            using (var client = new HttpClient())
            {
                //HTTP PUT
                var putTask = client.PutAsJsonAsync<T>(requestUri, obj);
                putTask.Wait();

                var result = putTask.Result;
                //if web api returns http status code other than created,then throw exception.
                if (result.StatusCode != System.Net.HttpStatusCode.OK) { result.ThrowException(); }

                return result.StatusCode;
            }
        }

        public static System.Net.HttpStatusCode DeleteObject<T>(String requestUri)
        {
            using (var client = new HttpClient())
            {
                //HTTP DELETE
                var deleteTask = client.DeleteAsync(requestUri);
                deleteTask.Wait();

                var result = deleteTask.Result;

                //if web api returns http status code other than created,then throw exception.
                if (result.StatusCode != System.Net.HttpStatusCode.OK) { result.ThrowException(); }

                return result.StatusCode;
            }
        }
    }

    public static class WebAPIConsumerExtensions
    {
        public static void ThrowException(this HttpResponseMessage result) {
            throw new System.Web.Http.HttpResponseException(result);
        }

        public static string ErrorMessage(this HttpResponseMessage result) {
            return "Error Message : " + result.Content.ReadAsStringAsync().Result + Environment.NewLine +
                   ", Error Code : " + result.StatusCode;
        }
    }
}