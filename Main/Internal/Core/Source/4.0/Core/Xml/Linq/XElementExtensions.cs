namespace EyeSoft.Xml.Linq
{
    using System.Linq;
    using System.Xml.Linq;

    public static class XElementExtensions
	{
		public static XElement RemoveNamespaces(this XElement xmlDocument)
		{
			if (!xmlDocument.HasElements)
			{
				var xElement =
					new XElement(xmlDocument.Name.LocalName)
						{
							Value = xmlDocument.Value
						};

				foreach (var attribute in xmlDocument.Attributes())
				{
					xElement.Add(attribute);
				}

				return xElement;
			}

			return
				new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(RemoveNamespaces));
		}
	}
}