using System.Web.Http;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Demo.RestApi.Common.IOC;
using Demo.RestApi.Domain.IOC;
using Demo.RestApi.Web.App_Start.IOC;

namespace Demo.RestApi.Web.App_Start
{
	public class IocConfig
	{
		public static void ConfigureIocContainer(HttpConfiguration globalConfiguration)
		{
			var container = new WindsorContainer();

			container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));

			container.Install(FromAssembly.Containing<DomainInstaller>()).
				Install(FromAssembly.Containing<CommonInstaller>()).
				Install(FromAssembly.This());

			var dependencyResolver = new WindsorDependencyResolver(container.Kernel);

			GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
		}
	}
}
