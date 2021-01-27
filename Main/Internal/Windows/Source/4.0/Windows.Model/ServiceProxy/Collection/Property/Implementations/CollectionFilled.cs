namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property.Implementations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Collections.ObjectModel;
    using Core.Mapping;
    using Threading;

    internal class CollectionFilled<TService, T, TCollectionType> :
		ICollectionFilled<TCollectionType>,
		ICollectionPropertyFilled<TCollectionType>
		where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly IObservableCollection<TCollectionType> collection;

		private readonly IComparer comparer;

		private readonly Task task;

		internal CollectionFilled(
			LoaderParams<TService> loaderParams,
			Func<TService, IEnumerable<T>> func,
			IObservableCollection<TCollectionType> collection,
			IComparer comparer)
		{
			this.loaderParams = loaderParams;
			this.collection = collection;
			this.comparer = comparer;

			Action action = () => LoadCollection(func);

			if (!loaderParams.UseTaskFactory)
			{
				action();
				return;
			}

			task = Task.Factory.StartNew(action);
		}

		public void Completed(Action<IObservableCollection<TCollectionType>> action)
		{
			if (task == null)
			{
				action(collection);
				return;
			}

			task.ContinueWith(t => action(collection), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		public void Completed<TConverted>(Action<IObservableCollection<TConverted>> action)
		{
			var convertedCollection = (IObservableCollection<TConverted>)null;

			if (collection != null)
			{
				convertedCollection = CollectionFactory.Create(collection.Select(x => Mapper.Map<TConverted>(x)));
			}

			if (task == null)
			{
				action(convertedCollection);
				return;
			}

			task.ContinueWith(t => action(convertedCollection), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		private void LoadCollection(Func<TService, IEnumerable<T>> func)
		{
			var dataService = new DataService<TService>(loaderParams.ProxyCreator);

			dataService.LoadEnumerable(func, collection);

			if (collection != null && comparer != null)
			{
				collection.Sort(comparer);
			}
		}
	}
}