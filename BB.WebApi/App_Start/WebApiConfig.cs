using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BB.WebApi.Utilities;

namespace BB.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Handle checking of the API key
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiKeyHeaderValueHandler());
        }
    }
}
