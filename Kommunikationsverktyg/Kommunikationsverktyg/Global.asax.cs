﻿using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.DbInitializer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Kommunikationsverktyg
{
    public class MvcApplication : System.Web.HttpApplication
    {

     
        protected void Application_Start()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
