﻿using API.App_Start;
using System.Web.Http;
using System.Web.Mvc;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutofacConfig.Register();
        }
    }
}
