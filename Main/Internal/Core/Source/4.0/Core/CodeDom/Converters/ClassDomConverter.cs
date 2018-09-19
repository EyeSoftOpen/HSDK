namespace EyeSoft.CodeDom.Converters
{
	using System.Text;

	using EyeSoft.Text;

	public class ClassDomConverter
		: IToStringConverter<ClassCodeDom>
	{
		public string ConvertToString(ClassCodeDom classCodeDom)
		{
			var classBuilder =
				new StringBuilder();

			foreach (var import in classCodeDom.ImportList)
			{
				classBuilder.AppendIndentedLineFormat(1, "using {0};", import);
			}

			classBuilder.AppendLine();

			classBuilder.AppendIndentedLineFormat(1, "{0} class {1}", classCodeDom.Visibility.ToString().ToLower(), classCodeDom.Name);
			classBuilder.OpenBracket(1);

			var methodConverter = new MethodConverter(classCodeDom.ImportList);

			foreach (var method in classCodeDom.MethodList)
			{
				classBuilder.Append(methodConverter.ConvertToString(method));
			}

			classBuilder.CloseBracket(1);

			return
				classBuilder.ToString();
		}
	}
}