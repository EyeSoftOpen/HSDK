namespace EyeSoft
{
	public interface ICondition<T>
	{
		INegableCondition<T> Is { get; }
	}
}