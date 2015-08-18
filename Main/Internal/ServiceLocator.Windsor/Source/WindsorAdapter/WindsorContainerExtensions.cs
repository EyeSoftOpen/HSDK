namespace EyeSoft.ServiceLocator.Windsor
{
	using System.Linq;
	using System.Text;

	using Castle.MicroKernel;
	using Castle.MicroKernel.Handlers;
	using Castle.Windsor;
	using Castle.Windsor.Diagnostics;

	public static class WindsorContainerExtensions
	{
		public static void CheckForPotentiallyMisconfiguredComponents(this IWindsorContainer container)
		{
			var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
			var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

			var handlers = diagnostics.Inspect();

			if (!handlers.Any())
			{
				return;
			}

			var message = new StringBuilder();
			var inspector = new DependencyInspector(message);

			foreach (var handler in handlers.Cast<IExposeDependencyInfo>())
			{
				handler.ObtainDependencyDetails(inspector);
			}

			throw new MisconfiguredComponentException(message.ToString());
		}
	}
}