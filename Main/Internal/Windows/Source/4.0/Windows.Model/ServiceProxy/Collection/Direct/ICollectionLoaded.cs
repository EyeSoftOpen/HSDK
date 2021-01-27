namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Direct
{
    using System;
    using System.Collections.Generic;

    public interface ICollectionLoaded<out TService>
		where TService : IDisposable
	{
		ICollectionFilled<T> Fill<T>(Func<TService, IEnumerable<T>> func);
	}
}