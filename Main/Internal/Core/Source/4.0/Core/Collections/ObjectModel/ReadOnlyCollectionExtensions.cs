namespace EyeSoft.Collections.ObjectModel
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public static class ReadOnlyCollectionExtensions
	{
		public static INotifyCollectionChanged AsNotifyCollectionChanged<T>(
			this ReadOnlyObservableCollection<T> collection)
		{
			return collection;
		}
	}
}