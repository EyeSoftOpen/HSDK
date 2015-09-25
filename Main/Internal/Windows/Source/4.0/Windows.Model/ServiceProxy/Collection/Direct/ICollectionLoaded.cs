using System;
using System.Collections.Generic;

namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Direct
{
	public interface ICollectionLoaded<out TService>
		where TService : IDisposable
	{
		ICollectionFilled<T> Fill<T>(Func<TService, IEnumerable<T>> func);
	}
}