using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using ContactApp.Models;
using ContactApp.Models.DependencyInjector;
using ContactApp.Api.Filters;

namespace ContactApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //global exception handler to create http response message and send it to client.
            config.Filters.Add(new RestAPIGlobalExceptionHandler());

            //repository class dependency for backend activities.
            var container = new UnityContainer();
            container.RegisterType<IContactRepository, ContactRepository>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
