namespace EyeSoft.Core.Serialization
{
    using System.IO;

    public interface ISerializer<T>
	{
		T DeserializeFromReader(TextReader reader);

		T DeserializeFromStream(Stream stream);

		T DeserializeFromString(string value);

		void SerializeToWriter(T value, TextWriter writer);

		void SerializeToStream(T value, Stream stream);

		string SerializeToString(T value);
	}
}