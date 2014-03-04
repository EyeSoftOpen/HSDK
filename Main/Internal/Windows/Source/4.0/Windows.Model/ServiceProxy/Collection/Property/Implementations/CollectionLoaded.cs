namespace EyeSoft.Windows.Model.Collection.Property
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Windows.Model.Collections.ObjectModel;

	internal class CollectionLoaded<TService, TCollectionType> :
		ICollectionLoaded<TService, TCollectionType>,
		ICollectionSorted<TService, TCollectionType>
		where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly IObservableCollection<TCollectionType> collection;

		private IComparer comparer;

		public CollectionLoaded(LoaderParams<TService> loaderParams)
			: this(loaderParams, null)
		{
		}

		public CollectionLoaded(LoaderParams<TService> loaderParams, IObservableCollection<TCollectionType> collection)
		{
			this.loaderParams = loaderParams;
			this.collection = collection;
		}

		public ICollectionPropertyFilled<TCollectionType> Fill<T>(Func<TService, IEnumerable<T>> func)
		{
			return new CollectionFilled<TService, T, TCollectionType>(loaderParams, func, collection, comparer);
		}

		ICollectionPropertyFilled<TCollectionType> ICollectionLoaded<TService, TCollectionType>.Fill<T>(Func<TService, IEnumerable<T>> func)
		{
			return new CollectionFilled<TService, T, TCollectionType>(loaderParams, func, collection, comparer);
		}

		ICollectionSorted<TService, TCollectionType>
			ICollectionLoaded<TService, TCollectionType>.Sort(Expression<Func<TCollectionType, IComparable>> func)
		{
			Enforce.Argument(() => func);

			return ((ICollectionLoaded<TService, TCollectionType>)this).Sort(ComparerFactory<TCollectionType>.Create(func));
		}

		ICollectionSorted<TService, TCollectionType>
			ICollectionLoaded<TService, TCollectionType>.Sort(Func<TCollectionType, TCollectionType, int> comparerFunc)
		{
			Enforce.Argument(() => comparerFunc);

			return ((ICollectionLoaded<TService, TCollectionType>)this).Sort(ComparerFactory<TCollectionType>.Create(comparerFunc));
		}

		ICollectionSorted<TService, TCollectionType>
			ICollectionLoaded<TService, TCollectionType>.Sort(IComparer<TCollectionType> customComparer)
		{
			Enforce.Argument(() => customComparer);

			comparer = customComparer.ToNonGeneric();

			return this;
		}
	}
}