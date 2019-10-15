using System.Web.Http;
using System.Web.Http.Cors;




namespace EmployeeServices
{
    //public class CustomJsonFomattor : JsonMediaTypeFormatter
    //{
    //    public CustomJsonFomattor()
    //    {
    //        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json"));
    //    }

    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}
    public static class WebApiConfig
    {
        public static object EnableCorsAttribute { get; private set; }

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

            //To remove json formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //To remove xml formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);


            //properly indented the json object in a response.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //return respunse in camel case format
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


            //Register the custom Formatter that is in the custom class above
            //This is to send Json response type in the Header if json is asked for and text/xml if text/xml is asked for in the header(this has nothing to do with the body
            //This has only to do with the reponse type in the header
            //config.Formatters.Add(new CustomJsonFomattor());

            //To Endabl CrossDomain ajax calling throgh web page 
            // Insgall -> install-package Microsoft.AspNet.WebApi.Cors
            //First is fro all the link/url, 2nd parametter(star) is for header type, method supported (Get, put, post, delete)  
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");// this is to enable cors globly
            config.EnableCors(cors);
            //This is to enable cors at the controller lavel, however this need to be added in the controller [EnableCorsAttribute("*", "*", "*")]
            //config.EnableCors();
        }
    }
}
