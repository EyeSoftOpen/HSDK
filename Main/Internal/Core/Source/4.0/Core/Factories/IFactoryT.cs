namespace EyeSoft.Core
{
	public interface IFactory<out T>
	{
		T Create();
	}
}