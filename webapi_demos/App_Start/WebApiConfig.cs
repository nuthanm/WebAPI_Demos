using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI_Demos
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // To accept only JSON then we have to remove XML format
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            // To accept only XML then we have to remove XML format
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // If request came from browser then by default server sends XML format and if you want it in JSON
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // To indent JSON Data properly
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // To change Pascal Case to camelCase
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            /*
            // JSONP is one way to enable Cross Domain Sharing
            var jsonPFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, jsonPFormatter);
            */

            //Enable CORS
            // Origin : * means All  and if you want specific URL then add like http://localhost:2150,http://localhost:8080
            // Headers: Specifiy particular content type
            // Methods: Specify if only GET/PUT/DELETE/POST/*
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
