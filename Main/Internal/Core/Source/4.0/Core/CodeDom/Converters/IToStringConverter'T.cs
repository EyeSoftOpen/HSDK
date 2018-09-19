namespace EyeSoft.CodeDom.Converters
{
	public interface IToStringConverter<in T>
		where T : class
	{
		string ConvertToString(T value);
	}
}