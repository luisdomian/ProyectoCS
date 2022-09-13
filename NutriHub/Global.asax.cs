using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NutriHub.Controllers;

namespace NutriHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            Response.Clear();
            HttpException httpexception = ex as HttpException;
            RouteData route = new RouteData();
            route.Values.Add("controller","error");
            if(httpexception != null)
            {
                switch (httpexception.GetHttpCode())
                {
                    case 404:
                        route.Values.Add("action", "http404");
                        break;
                    case 500:
                        route.Values.Add("action", "http500");
                        break;
                    default:
                        route.Values.Add("action", "general");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
            }
            //IController errorcontroller = new ErrorController();
            //errorcontroller.Execute(new RequestContext(new HttpContextWrapper(Context), route));
        }
    }
}
