using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Demo.RestApi.Domain.IOC
{
	public class DomainInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes
					.FromThisAssembly()
					.Pick()
					.WithServiceDefaultInterfaces()
					.LifestyleSingleton());

			container.Register(
				Classes
					.FromAssemblyInDirectory(new AssemblyFilter("bin/extensions"))
					.InNamespace("Demo.RestApi.Domain", true).WithServiceDefaultInterfaces().LifestyleSingleton());
		}
	}
}
