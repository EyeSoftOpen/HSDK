namespace EyeSoft.Windows.Model.Collection.Direct
{
	using System;
	using System.Collections.Generic;

	public interface ICollectionLoaded<out TService>
		where TService : IDisposable
	{
		ICollectionFilled<T> Fill<T>(Func<TService, IEnumerable<T>> func);
	}
}