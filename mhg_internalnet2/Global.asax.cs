using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using mhg_internalnet2.PushNotification;

namespace mhg_internalnet2
{

    public class MvcApplication : System.Web.HttpApplication
    {
        private string _con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //SqlDependency.Start(_con);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            //NotificationComponent NC = new NotificationComponent();
            //var currentTime = DateTime.Now;
            //HttpContext.Current.Session["LastUpdated"] = currentTime;
            //NC.RegisterNotification(currentTime);
        }
        protected void Application_End()
        {
            //here we will stop Sql Dependency
            //SqlDependency.Stop(_con);
        }
    }
}
