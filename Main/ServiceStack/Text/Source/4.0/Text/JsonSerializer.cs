namespace EyeSoft.ServiceStack.Text
{
	using System.IO;

	using EyeSoft.Serialization;

	using global::ServiceStack.Text;

	public class JsonSerializer<T> : ISerializer<T>
	{
		public T DeserializeFromReader(TextReader reader)
		{
			return JsonSerializer.DeserializeFromReader<T>(reader);
		}

		public T DeserializeFromStream(Stream stream)
		{
			return JsonSerializer.DeserializeFromStream<T>(stream);
		}

		public T DeserializeFromString(string value)
		{
			return JsonSerializer.DeserializeFromString<T>(value);
		}

		public void SerializeToStream(T value, Stream stream)
		{
			JsonSerializer.SerializeToStream(value, stream);
		}

		public string SerializeToString(T value)
		{
			return JsonSerializer.SerializeToString(value);
		}

		public void SerializeToWriter(T value, TextWriter writer)
		{
			JsonSerializer.SerializeToWriter(value, writer);
		}
	}
}