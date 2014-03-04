namespace EyeSoft.Windows.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using EyeSoft.Reflection;
	using EyeSoft.Windows.Model.Collection.Direct;
	using EyeSoft.Windows.Model.Collection.Property;
	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Item;
	using EyeSoft.Windows.Model.Item.Property;
	using EyeSoft.Windows.Model.With;

	public class ServiceFactory<TService> where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		public ServiceFactory(Func<TService> proxyCreator, bool useFactory = true)
		{
			loaderParams = new LoaderParams<TService>(proxyCreator, useFactory);
		}

		public ServiceFactory(IDisposableFactory<TService> proxyFactory, bool useFactory = true)
		{
			loaderParams = new LoaderParams<TService>(proxyFactory.Create, useFactory);
		}

		public IItemFilled<T> Fill<T>(Func<TService, T> func)
		{
			return new ItemFilled<TService, T, T>(loaderParams, func, null);
		}

		public IItemLoaded<TService, TProperty> Property<TViewModel, TProperty>(
			TViewModel instance, Expression<Func<TViewModel, TProperty>> collection)
			where TViewModel : INotifyPropertyChanged
		{
			var member = collection.Member();

			return new ItemLoaded<TService, TProperty>(loaderParams, instance, member);
		}

		public Collection.Direct.ICollectionFilled<T> Fill<T>(Func<TService, IEnumerable<T>> func)
		{
			return new CollectionFilled<TService, T>(loaderParams, func);
		}

		public ICollectionLoaded<TService, TCollectionType>
			Collection<TViewModel, TCollectionType>(
			TViewModel instance,
			Expression<Func<TViewModel, IEnumerable<TCollectionType>>> collectionPropertyExpression)
			where TViewModel : INotifyPropertyChanged
		{
			var member = collectionPropertyExpression.Member();
			var collection = CollectionFactory.Create<TCollectionType>(instance, member);

			return new CollectionLoaded<TService, TCollectionType>(loaderParams, collection);
		}

		public IValueAction<TService, TStart> With<TStart>(TStart value)
		{
			return new ValueAction<TService, TStart>(loaderParams, value);
		}

		public void Execute(Action<TService> action)
		{
			Action executeAction = () =>
				{
					using (var service = loaderParams.ProxyCreator())
					{
						action(service);
					}
				};

			if (!loaderParams.UseTaskFactory)
			{
				executeAction();
				return;
			}

			Task.Factory.StartNew(executeAction);
		}
	}
}