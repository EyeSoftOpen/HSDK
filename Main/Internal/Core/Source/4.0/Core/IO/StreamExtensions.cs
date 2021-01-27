namespace EyeSoft.Core.IO
{
    using System.IO;
    using System.Text;

    public static class StreamExtensions
	{
		public static string StreamToString(this Stream stream)
		{
			var reader = new StreamReader(stream, true);
			stream.SetSeekOnBegin();
			var streamToString = reader.ReadToEnd();
			return streamToString;
		}

		public static string StreamToString(this Stream stream, Encoding encoding)
		{
			var reader = new StreamReader(stream, encoding);
			stream.SetSeekOnBegin();
			var streamToString = reader.ReadToEnd();
			return streamToString;
		}

		public static void SetSeekOnBegin(this Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);
		}

		public static byte[] ToByteArray(this Stream input)
		{
			using (var destination = new MemoryStream())
			{
				input.CopyTo(destination);
				return destination.ToArray();
			}
		}
	}
}