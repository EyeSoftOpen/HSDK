namespace EyeSoft.Core
{
    using System;

    public interface IDisposableFactory
	{
		T Create<T>() where T : IDisposable;
	}
}