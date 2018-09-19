namespace EyeSoft
{
	public interface IEnsuring
	{
		INamedCondition<T> That<T>(T value);
	}
}