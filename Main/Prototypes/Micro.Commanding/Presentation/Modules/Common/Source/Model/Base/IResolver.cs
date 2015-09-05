namespace Model
{
	using System;

	public interface IResolver
	{
		T Resolve<T>(Type type);
		T Resolve<T>();
	}
}