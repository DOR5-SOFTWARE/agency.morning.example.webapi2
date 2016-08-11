using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Demo.RestApi.Web.App_Start.IOC
{
	public class ControllersInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes
					.FromThisAssembly()
					.BasedOn<ApiController>()
					.LifestyleScoped()
				);
		}
	}
}
