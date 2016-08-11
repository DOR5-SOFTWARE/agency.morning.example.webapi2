using System.Web.Http;
using Demo.RestApi.Web.App_Start;

namespace Demo.RestApi.Web
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			IocConfig.ConfigureIocContainer(GlobalConfiguration.Configuration);

			WebApiConfig.ConfigureWebApiRouting(GlobalConfiguration.Configuration);

			WebApiConfig.ConfigureDataFormatters(GlobalConfiguration.Configuration);

			FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters, GlobalConfiguration.Configuration.DependencyResolver);
		}
	}
}
