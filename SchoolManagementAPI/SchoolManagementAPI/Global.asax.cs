using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SchoolManagementAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.Flush();
            }
        }

        protected void Application_BeginRequest()
        {
            //string[] allowedOrigin = new string[] { "http://localhost:55958", "http://localhost:55902" };
            //var origin = HttpContext.Current.Request.Headers["Origin"];
            //if (origin != null && allowedOrigin.Contains(origin))
            //{
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
            //}
        }
    }
}
