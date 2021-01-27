namespace EyeSoft.Core.Serialization
{
	public interface ISerializerFactory
	{
		string TypeName { get; }

		ISerializer<T> Create<T>();
	}
}