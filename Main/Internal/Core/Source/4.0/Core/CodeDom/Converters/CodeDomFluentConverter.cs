namespace EyeSoft.CodeDom.Converters
{
	using System.Linq;
	using System.Text;

	using EyeSoft.Text;

	public class CodeDomFluentConverter :
		IToStringConverter<CodeDomFluent>
	{
		public string ConvertToString(CodeDomFluent codeDomFluent)
		{
			var codeBuilder =
				new StringBuilder();

			foreach (var nameSpace in codeDomFluent.NamespaceList)
			{
				codeBuilder
					.AppendLineFormat("namespace {0}", nameSpace.Name)
					.OpenBracket();

				foreach (var classCodeDom in nameSpace.ClassList)
				{
					codeBuilder
						.Append(new ClassDomConverter().ConvertToString(classCodeDom));
				}

				codeBuilder.CloseBracket();
			}

			var source = codeBuilder.ToString();

			return
				source.Substring(0, source.Count() - 2);
		}
	}
}