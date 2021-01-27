namespace EyeSoft.Core.Serialization
{
    using System.IO;
    using IO;

    public static class SerializerExtensions
	{
		public static T Clone<T>(this ISerializer<T> serializer, T value)
		{
			using (var stream = new MemoryStream())
			{
				serializer.SerializeToStream(value, stream);

				stream.SetSeekOnBegin();

				var cloned = serializer.DeserializeFromStream(stream);
				return cloned;
			}
		}
	}
}