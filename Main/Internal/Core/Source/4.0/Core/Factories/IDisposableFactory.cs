namespace EyeSoft
{
    using System;

    public interface IDisposableFactory
	{
		T Create<T>() where T : IDisposable;
	}
}