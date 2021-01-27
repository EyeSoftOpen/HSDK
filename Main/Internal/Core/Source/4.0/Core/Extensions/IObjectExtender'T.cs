namespace EyeSoft.Core.Extensions
{
	public interface IObjectExtender<out T>
	{
		T Instance { get; }
	}
}