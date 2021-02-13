namespace EyeSoft.Docs.Settings.Windows
{
	using System.IO;
    using EyeSoft.Serialization;
    using Newtonsoft.Json;

	public class JsonSerializerFactory : ISerializerFactory
	{
		public string TypeName => "json";

        public ISerializer<T> Create<T>()
		{
			return new JsonSerializer<T>();
		}

		private class JsonSerializer<T> : ISerializer<T>
		{
			public T DeserializeFromReader(TextReader reader)
			{
				throw new System.NotImplementedException();
			}

			public T DeserializeFromStream(Stream stream)
			{
				throw new System.NotImplementedException();
			}

			public T DeserializeFromString(string value)
			{
				return JsonConvert.DeserializeObject<T>(value);
			}

			public void SerializeToWriter(T value, TextWriter writer)
			{
				throw new System.NotImplementedException();
			}

			public void SerializeToStream(T value, Stream stream)
			{
				throw new System.NotImplementedException();
			}

			public string SerializeToString(T value)
			{
				return JsonConvert.SerializeObject(value);
			}
		}
	}
}