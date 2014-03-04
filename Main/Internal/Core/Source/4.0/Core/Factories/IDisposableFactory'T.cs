namespace EyeSoft
{
	using System;

	public interface IDisposableFactory<out T> : IFactory<T>
		where T : IDisposable
	{
	}
}