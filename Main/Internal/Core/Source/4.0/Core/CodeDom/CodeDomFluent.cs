namespace EyeSoft.CodeDom
{
	using System.Collections.Generic;

	public class CodeDomFluent
	{
		private readonly List<string> referencedAssemblyList =
			new List<string>();

		private readonly IDictionary<string, Namespace> namespaceDictionary =
			new Dictionary<string, Namespace>();

		public CodeDomFluent()
		{
			ReferencedAssemblyList = referencedAssemblyList;
			NamespaceList = namespaceDictionary.Values;
		}

		internal IEnumerable<Namespace> NamespaceList { get; private set; }

		internal IEnumerable<string> ReferencedAssemblyList { get; private set; }

		public Namespace Namespace(string namespaceName)
		{
			var localNamespace = new Namespace(this, namespaceName);
			namespaceDictionary.Add(namespaceName, localNamespace);
			return localNamespace;
		}

		public CodeDomFluent ReferenceAssembly(string assemblyName)
		{
			referencedAssemblyList.Add(assemblyName);
			return this;
		}
	}
}