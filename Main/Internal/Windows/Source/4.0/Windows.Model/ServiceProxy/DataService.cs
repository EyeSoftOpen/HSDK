namespace EyeSoft.Windows.Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;
	using EyeSoft.Windows.Model.Collections.ObjectModel;

	internal class DataService<TService> where TService : IDisposable
	{
		private readonly Func<TService> proxyCreator;

		public DataService(Func<TService> proxyCreator)
		{
			this.proxyCreator = proxyCreator;
		}

		public void LoadEnumerable<T, TCollectionType>(
			Func<TService, IEnumerable<T>> func,
			IObservableCollection<TCollectionType> collection)
		{
			using (var service = proxyCreator())
			{
				var data = func(service);

				if (data == null)
				{
					return;
				}

				var dataConverted =
					typeof(T) != typeof(TCollectionType) ?
						data.Select(item => Mapper.Map<TCollectionType>(item)) :
						data.Select(item => (TCollectionType)(object)item);

				collection.AddRange(dataConverted);
			}
		}

		public T LoadItem<T>(Func<TService, T> func)
		{
			using (var service = proxyCreator())
			{
				var data = func(service);

				return data;
			}
		}

		public void Execute<T>(T arg, Action<TService, T> action)
		{
			using (var service = proxyCreator())
			{
				action(service, arg);
			}
		}
	}
}