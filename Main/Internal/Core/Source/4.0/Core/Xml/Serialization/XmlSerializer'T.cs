namespace EyeSoft.Xml.Serialization
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	using EyeSoft.Serialization;

	public class XmlSerializer<T> : ISerializer<T>
	{
		private readonly FormattedXmlSerializer serializer = new FormattedXmlSerializer(typeof(T));

		public T DeserializeFromReader(TextReader reader)
		{
			return (T)serializer.Deserialize(reader);
		}

		public T DeserializeFromStream(Stream stream)
		{
			return (T)serializer.Deserialize(stream);
		}

		public T DeserializeFromString(string value)
		{
			using (var stream = value.ToStream())
			{
				var obj = (T)serializer.Deserialize(stream);

				return obj;
			}
		}

		public void SerializeToWriter(T value, TextWriter writer)
		{
			serializer.Serialize(writer, value);
		}

		public void SerializeToStream(T value, Stream stream)
		{
			serializer.Serialize(stream, value);
		}

		public string SerializeToString(T value)
		{
			return serializer.Serialize(value);
		}

		public string Serialize(IEnumerable<T> collection)
		{
			var list = collection.ToList();

			var listType = list.GetType();

			var collectionSerializer = new FormattedXmlSerializer(listType);

			var collectionSerialized = collectionSerializer.Serialize(list);

			var typeName = list.AsQueryable().ElementType.Name;
			var collectionName = string.Format("ArrayOf{0}", typeName);
			var newCollectionName = string.Format("{0}List", typeName);

			collectionSerialized = collectionSerialized.Replace(collectionName, newCollectionName);

			return collectionSerialized;
		}

		public IEnumerable<T> DeserializeCollection(string xml)
		{
			var nodes = XElement.Parse(xml).Nodes().Select(element => element.ToString()).ToList();

			var deserializeCollection = nodes.Select(DeserializeFromString).ToList();

			return deserializeCollection;
		}
	}
}