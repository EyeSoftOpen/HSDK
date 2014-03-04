namespace EyeSoft.ServiceStack.Text
{
	using EyeSoft.Serialization;

	public class JsonSerializerFactory : ISerializerFactory
	{
		public const string Name = "json";

		public string TypeName
		{
			get { return Name; }
		}

		public ISerializer<T> Create<T>()
		{
			return new JsonSerializer<T>();
		}
	}
}