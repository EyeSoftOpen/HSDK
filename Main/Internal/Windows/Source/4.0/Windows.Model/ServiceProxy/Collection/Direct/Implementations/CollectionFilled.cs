namespace EyeSoft.Windows.Model.Collection.Direct
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Mapping;
	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Threading;

	internal class CollectionFilled<TService, TCollectionType> : ICollectionFilled<TCollectionType>
		where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly Func<TService, IEnumerable<TCollectionType>> func;

		public CollectionFilled(LoaderParams<TService> loaderParams, Func<TService, IEnumerable<TCollectionType>> func)
		{
			this.loaderParams = loaderParams;
			this.func = func;
		}

		public void Completed(Action<IObservableCollection<TCollectionType>> completed)
		{
			if (!loaderParams.UseTaskFactory)
			{
				completed(LoadCollection());
				return;
			}

			Task.Factory.StartNew(() => LoadCollection())
				.ContinueWith(t => completed(t.Result), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		public void Completed<TConverted>(Action<IObservableCollection<TConverted>> completed)
		{
			if (!loaderParams.UseTaskFactory)
			{
				var collection = LoadCollection();

				var converted = ConvertCollection<TConverted>(collection);

				completed(converted);

				return;
			}

			Task.Factory.StartNew(() => ConvertCollection<TConverted>(LoadCollection()))
				.ContinueWith(t => completed(t.Result), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		private IObservableCollection<TConverted>
			ConvertCollection<TConverted>(IEnumerable<TCollectionType> collection)
		{
			if (collection == null)
			{
				return null;
			}

			var itemsConverted = collection.Select(x => Mapper.Map<TConverted>(x));

			var converted = CollectionFactory.Create(itemsConverted);

			return converted;
		}

		private IObservableCollection<TCollectionType> LoadCollection()
		{
			var collection = CollectionFactory.Create<TCollectionType>();

			var dataService = new DataService<TService>(loaderParams.ProxyCreator);

			dataService.LoadEnumerable(func, collection);

			return collection;
		}
	}
}