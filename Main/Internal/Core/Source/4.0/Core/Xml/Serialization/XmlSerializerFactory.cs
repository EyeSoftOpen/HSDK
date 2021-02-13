namespace EyeSoft.Xml.Serialization
{
    using EyeSoft.Serialization;

    public class XmlSerializerFactory : ISerializerFactory
	{
		public const string Name = "xml";

		public string TypeName => Name;

        public ISerializer<T> Create<T>()
		{
			return new XmlSerializer<T>();
		}
	}
}