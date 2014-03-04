namespace EyeSoft.Runtime.Serialization
{
	using EyeSoft.Serialization;

	public class BinarySerializerFactory : ISerializerFactory
	{
		public const string Name = "bin";

		public string TypeName
		{
			get { return Name; }
		}

		public ISerializer<T> Create<T>()
		{
			return new BinarySerializer<T>();
		}
	}
}