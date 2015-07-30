namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.Linq;

	public static class AssemblyInfo
	{
		public static string GetData(IEnumerable<string> lines, AssemblyInfoData data)
		{
			var assemblyLine = GetAssemblyLine(data);

			var line = lines.SingleOrDefault(x => x.StartsWith(assemblyLine));

			if (line == null)
			{
				return null;
			}

			return GetDataFromLine(assemblyLine, line);
		}

		public static string GetData(string line, AssemblyInfoData data)
		{
			var assemblyLine = GetAssemblyLine(data);

			return GetDataFromLine(assemblyLine, line);
		}

		public static T GetData<T>(string line, AssemblyInfoData data)
		{
			return Converter.Convert<T>(GetData(line, data));
		}

		private static string GetDataFromLine(string assemblyLine, string line)
		{
			line = line.Replace(assemblyLine, null).Replace("\")]", null);

			return line;
		}

		private static string GetAssemblyLine(AssemblyInfoData data)
		{
			var assemblyLine = string.Format("[assembly: {0}(\"", data);

			return assemblyLine;
		}
	}
}