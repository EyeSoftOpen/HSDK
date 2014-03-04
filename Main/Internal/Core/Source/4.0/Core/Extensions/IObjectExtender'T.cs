namespace EyeSoft.Extensions
{
	public interface IObjectExtender<out T>
	{
		T Instance { get; }
	}
}