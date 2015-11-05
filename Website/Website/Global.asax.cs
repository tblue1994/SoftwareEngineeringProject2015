using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Website.App_Start;
using Website.Models;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Doesn't work with any of CreateDatabaseIfNotExists, DropCreateDatabaseAlways, or 
            // DropCreateDatabaseIfModelChanges.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WebsiteContext>());

            // Attempt to hackily force creation (still doesn't work without)
            //WebsiteContext.Create().Database.Initialize(true);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.EnsureInitialized();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = UnityConfig.GetConfiguredContainer();
            DependencyResolver.SetResolver(new MvcResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new MyApiResolver(container);
            //UnityConfig.GetConfiguredContainer();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }
    }
}
