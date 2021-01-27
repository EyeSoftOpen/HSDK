namespace EyeSoft.Core.Runtime.Serialization
{
    using Core.Serialization;

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