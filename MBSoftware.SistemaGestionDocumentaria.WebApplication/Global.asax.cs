using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MBSoftware.SistemaGestionDocumentaria.WebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Application_Error(object sender, EventArgs e) 
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Guid obj = Guid.NewGuid();
            log4net.LogicalThreadContext.Properties["CustomId"] = obj.ToString();
            log.Error("App_Error", ex);

            if (ex is Exception)
            {
                Server.ClearError(); // important!!!
                Response.Clear();
                Response.StatusCode = 404;
                Response.TrySkipIisCustomErrors = true;
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"Error\":{\"Code\":" + "\"" +obj .ToString()+ "\"" + ",\"Status\":\"Ocurrió un error, consulté con el administrador.\"}}");
            }
        }
    }
}