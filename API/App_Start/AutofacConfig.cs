using API.Business.Implementation;
using API.Business.Interfaces;
using API.Repository.CSV;
using API.Repository.Interfaces;
using Autofac;
using Autofac.Integration.WebApi;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace API.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType(typeof(HotelRepository)).As(typeof(IHotelRepository)).WithParameter("path", 
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["path"]));

            builder.RegisterType(typeof(HotelService)).As(typeof(IHotelService));

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}