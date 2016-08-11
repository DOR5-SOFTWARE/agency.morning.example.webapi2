using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Demo.RestApi.Common.IOC
{
	public class CommonInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.
					FromThisAssembly()
					.Pick()
					.WithServiceDefaultInterfaces()
					.LifestyleSingleton());
		}
	}
}
