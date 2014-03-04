namespace EyeSoft.Windows.Model.Input
{
	using System.Collections.Generic;
	using System.Reflection;

	public class CommandMethod
	{
		public CommandMethod(MethodInfo methodInfo, bool isAsync, IEnumerable<string> errors)
		{
			MethodInfo = methodInfo;
			IsAsync = isAsync;
			Errors = errors;
		}

		public MethodInfo MethodInfo { get; private set; }

		public bool IsAsync { get; private set; }

		public IEnumerable<string> Errors { get; private set; }
	}
}