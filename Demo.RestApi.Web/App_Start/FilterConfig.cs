using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using Demo.RestApi.Domain.DataProviders;
using Demo.RestApi.Domain.Services;
using Demo.RestApi.Web.Filters;

namespace Demo.RestApi.Web.App_Start
{
	public class FilterConfig
	{
		public static void RegisterWebApiFilters(HttpFilterCollection filters, IDependencyResolver dependencyResolver)
		{
			var jwtService = (IJwtService)dependencyResolver.GetService(typeof (IJwtService));
			var responseBuilderService = (IResponseBuilderService)dependencyResolver.GetService(typeof (IResponseBuilderService));
			var requestDataProvider = (IRequestDataProvider)dependencyResolver.GetService(typeof (IRequestDataProvider));

			filters.Add(new JwtFilter(jwtService, responseBuilderService, requestDataProvider));
		}
	}
}
