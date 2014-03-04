namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Reflection;

	using EyeSoft.Reflection;

	public static class CollectionFactory
	{
		private static readonly Singleton<ICollectionFactory> collectionFactoryInstance =
			new Singleton<ICollectionFactory>(() => new DefaultCollectionFactory());

		public static void Set(Func<ICollectionFactory> instance)
		{
			collectionFactoryInstance.Set(instance);
		}

		public static IObservableCollection<TCollectionType> Create<TCollectionType>(
			INotifyPropertyChanged instance,
			MemberInfo memberInfo)
		{
			if (memberInfo == null)
			{
				return collectionFactoryInstance.Instance.Create<TCollectionType>();
			}

			var assignedCollection = memberInfo.GetValue(instance);

			IObservableCollection<TCollectionType> localCollection;

			if (assignedCollection != null)
			{
				localCollection = (IObservableCollection<TCollectionType>)assignedCollection;
				localCollection.Clear();
			}
			else
			{
				localCollection = collectionFactoryInstance.Instance.Create<TCollectionType>();
				memberInfo.SetValue(instance, localCollection);
			}

			return localCollection;
		}

		public static IObservableCollection<T> Create<T>()
		{
			return collectionFactoryInstance.Instance.Create<T>();
		}

		public static IObservableCollection<T> Create<T>(IEnumerable<T> collection)
		{
			return collectionFactoryInstance.Instance.Create(collection);
		}
	}
}