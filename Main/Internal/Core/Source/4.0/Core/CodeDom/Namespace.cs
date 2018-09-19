namespace EyeSoft.CodeDom
{
	using System.Collections.Generic;

	public class Namespace
	{
		private readonly IDictionary<string, ClassCodeDom> classCodeDomList =
			new Dictionary<string, ClassCodeDom>();

		public Namespace(CodeDomFluent codeDomFluent, string name)
		{
			CodeDomFluent = codeDomFluent;
			Name = name;
		}

		internal IEnumerable<ClassCodeDom> ClassList
		{
			get
			{
				return classCodeDomList.Values;
			}
		}

		internal string Name { get; private set; }

		private CodeDomFluent CodeDomFluent { get; set; }

		public ClassCodeDom Class(string className)
		{
			var classCodeDom = new ClassCodeDom(CodeDomFluent, className);
			classCodeDomList.Add(className, classCodeDom);

			return classCodeDom;
		}
	}
}