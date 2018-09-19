namespace EyeSoft.CodeDom
{
	using System;
	using System.Collections.Generic;


	public class ClassCodeDom
	{
		private readonly List<string> importList =
			new List<string>();

		private readonly Dictionary<string, Method> methodDictionary =
			new Dictionary<string, Method>();

		public ClassCodeDom(CodeDomFluent codeDomFluent, string name)
		{
			Enforce
				.Argument(() => codeDomFluent)
				.Argument(() => name);

			CodeDomFluent = codeDomFluent;
			Name = name;

			ImportList = importList;
			MethodList = methodDictionary.Values;
			Visibility = ClassVisibility.Public;
		}

		internal IEnumerable<string> ImportList { get; private set; }

		internal IEnumerable<Method> MethodList { get; private set; }

		internal string Name { get; private set; }

		internal ClassVisibility Visibility { get; private set; }

		private CodeDomFluent CodeDomFluent { get; set; }

		public ClassCodeDom Using(string namespaceName)
		{
			Enforce
				.Argument(() => namespaceName);

			importList.Add(namespaceName);
			return this;
		}

		public IVoidMethod AddMethod(string methodName)
		{
			Enforce.Argument(() => methodName);

			return AddMethod(methodName, typeof(void));
		}

		public IReturnValueMethod AddMethod<TReturnType>(string methodName)
		{
			Enforce.Argument(() => methodName);

			return AddMethod(methodName, typeof(TReturnType));
		}

		private Method AddMethod(string methodName, Type type)
		{
			Enforce
				.Argument(() => methodName)
				.Argument(() => type);

			var method = new Method(CodeDomFluent, type, methodName);
			methodDictionary.Add(methodName, method);
			return method;
		}
	}
}