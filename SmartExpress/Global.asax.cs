﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SmartExpress
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
        }
    }
}
