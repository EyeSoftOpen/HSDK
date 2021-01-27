namespace EyeSoft.Core.Xml.Serialization
{
    using Core.Serialization;

    public class XmlSerializerFactory : ISerializerFactory
	{
		public const string Name = "xml";

		public string TypeName
		{
			get { return Name; }
		}

		public ISerializer<T> Create<T>()
		{
			return new XmlSerializer<T>();
		}
	}
}
