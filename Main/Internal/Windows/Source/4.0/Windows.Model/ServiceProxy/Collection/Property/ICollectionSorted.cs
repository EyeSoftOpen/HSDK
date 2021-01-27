namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
    using System;
    using System.Collections.Generic;

    public interface ICollectionSorted<out TService, TCollectionType>
		where TService : IDisposable
	{
		ICollectionPropertyFilled<TCollectionType> Fill<T>(Func<TService, IEnumerable<T>> func);
	}
}