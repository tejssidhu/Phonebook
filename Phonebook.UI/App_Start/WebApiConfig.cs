using System.Web.Http;

namespace Phonebook.UI
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

            config.Routes.MapHttpRoute(
                name: "DefaultEditApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$" }
            );

            //Should match /api/My/3ead6bea-4a0a-42ae-a009-853e2243cfa3
            config.Routes.MapHttpRoute(
                name: "DefaultSingleGuidApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "GetAllByGUID" },
                constraints: new { id = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$" } // id must be guid
            );

            //Should match /api/My/3ead6bea-4a0a-42ae-a009-853e2243cfa3
            config.Routes.MapHttpRoute(
                name: "DefaultDoubleGuidApi",
                routeTemplate: "api/{controller}/{id}/{itemId}",
                defaults: new { action = "GetSingleByGUID" },
                constraints: new
                {
                    id = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$",
                    itemId =  @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$"
                } // id must be guid
            );
        }
    }
}
