namespace EyeSoft.Windows
{
	using System;
	using System.Reflection;

	public static class Component
	{
		public static Uri ToUri(string resourceName, string folder = "Views", Assembly assembly = null)
		{
			assembly = assembly ?? Assembly.GetExecutingAssembly();

			var assemblyName = assembly.GetName().Name;

			if (folder != null)
			{
				folder = string.Concat(folder, "\\");
			}

			var uriString = string.Concat(assemblyName, ";component\\", folder, resourceName, ".xaml");

			return new Uri(uriString, UriKind.RelativeOrAbsolute);
		}
	}
}