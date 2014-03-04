namespace EyeSoft.Windows.Markup
{
	using System.Windows;
	using System.Windows.Markup;

	public class XamlParser
	{
		public static FrameworkElement Parse(string xaml)
		{
			var context = new ParserContext();
			context.XmlnsDictionary.Add(string.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

			var element = (FrameworkElement)XamlReader.Parse(xaml, context);
			return element;
		}
	}
}