namespace EyeSoft.Windows
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;
	using System.Windows;

	public static class ApplicationExtensions
	{
		public static ApplicationMutex ApplicationMutex(this Application application, Process process = null)
		{
			return application.GetType().Assembly.ApplicationMutex(application.Shutdown, process);
		}

		public static void Start(this Application application, string executionPath, string mainAssemblyName, string typeNameOrFullName = null, params object[] arguments)
		{
			var mainAssemblyPath = Path.Combine(executionPath, string.Concat(mainAssemblyName, ".dll"));

			var mainAssembly = Assembly.LoadFrom(mainAssemblyPath);

			if (typeNameOrFullName == null)
			{
				typeNameOrFullName = "ApplicationEntryPoint";
			}

			if (!typeNameOrFullName.Contains("."))
			{
				typeNameOrFullName = string.Concat(mainAssemblyName, ".", typeNameOrFullName);
			}

			var entryPointType = mainAssembly.GetType(typeNameOrFullName, true);

			var constructors = entryPointType.GetConstructors();

			IApplicationEntryPoint entryPoint = null;

			if (constructors.Length == 1)
			{
				var parameters = constructors[0].GetParameters();

				switch (parameters.Length)
				{
					case 0:
						entryPoint = (IApplicationEntryPoint)Activator.CreateInstance(entryPointType);
						break;
					case 1:
						if (parameters[0].ParameterType == typeof(Application))
						{
							entryPoint = (IApplicationEntryPoint)Activator.CreateInstance(entryPointType, application);
						}
						break;
				}
			}
			else
			{
				entryPoint = entryPointType.CreateInstance<IApplicationEntryPoint>(arguments);
			}

			entryPoint.Start();

			if (application.MainWindow != null)
			{
				application.Run();
			}
		}
	}
}