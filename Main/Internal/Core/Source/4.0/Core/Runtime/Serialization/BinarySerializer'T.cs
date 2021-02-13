namespace EyeSoft.Runtime.Serialization
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using EyeSoft.Serialization;

    public class BinarySerializer<T> : ISerializer<T>
	{
		public T DeserializeFromReader(TextReader reader)
		{
			var stream = ((StreamReader)reader).BaseStream;
			return DeserializeFromStream(stream);
		}

		public T DeserializeFromStream(Stream stream)
		{
			return (T)new BinaryFormatter().Deserialize(stream);
		}

		public T DeserializeFromString(string value)
		{
			var bytes = Encoding.Default.GetBytes(value);

			using (var stream = new MemoryStream(bytes))
			{
				return DeserializeFromStream(stream);
			}
		}

		public void SerializeToWriter(T value, TextWriter writer)
		{
			var stream = ((StreamWriter)writer).BaseStream;
			new BinaryFormatter().Serialize(stream, value);
		}

		public void SerializeToStream(T value, Stream stream)
		{
			new BinaryFormatter().Serialize(stream, value);
		}

		public string SerializeToString(T value)
		{
			using (var stream = new MemoryStream())
			{
				SerializeToStream(value, stream);
				return Encoding.Default.GetString(stream.ToArray());
			}
		}
	}
}