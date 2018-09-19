namespace EyeSoft.CodeDom.Compiler
{
	using System;
	using System.CodeDom.Compiler;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using EyeSoft.CodeDom.Converters;

	public class CodeDomFluentCompiler
	{
		public Assembly Compile(CodeDomFluent codeDomFluent)
		{
			var codeDomProvider =
				CodeDomProvider
					.CreateProvider(
						"CSharp",
						new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });

			var compilerParameters =
				new CompilerParameters
					{
						GenerateExecutable = false,
						GenerateInMemory = false,
						CompilerOptions	 = "/optimize"
					};

			//// #if DEBUG
			////     compilerParameters.IncludeDebugInformation = true;
			////     compilerParameters.OutputAssembly =
			////			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Temp.dll");
			//// #endif

			codeDomFluent
				.ReferencedAssemblyList
				.ToList()
				.ForEach(assembly => compilerParameters.ReferencedAssemblies.Add(assembly));

			var source =
				new CodeDomFluentConverter()
					.ConvertToString(codeDomFluent);

			var results =
				codeDomProvider
					.CompileAssemblyFromSource(compilerParameters, source);

			if (results.Errors.Count > 0)
			{
				var messageTokens = results.Errors.Cast<CompilerError>().Select(error => error.ErrorText);

				throw new CompilerException(string.Join(Environment.NewLine, messageTokens.ToArray()));
			}

			return results.CompiledAssembly;
		}
	}
}