namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;
	using System.Windows;
	using System.Windows.Data;

	using EyeSoft.Collections.Generic;

	public static class EnumerableExtensions
	{
		public static ListCollectionView ListView<T>(this IEnumerable<T> source)
		{
			return (ListCollectionView)View(source);
		}

		public static ICollectionView View<T>(this IEnumerable<T> source)
		{
			return Application.Current.Sync().Execute(() => CollectionViewSource.GetDefaultView(source));
		}

		public static ListCollectionView InvertSort<T>(this IEnumerable<T> source)
		{
			return Application.Current.Sync().Execute(
				() =>
					{
						var listView = source.ListView();

						if (listView.CustomSort == null)
						{
							return listView;
						}

						return SetComparer(listView, listView.CustomSort.Invert());
					});
		}

		public static ListCollectionView Sort<T>(this IEnumerable<T> source, Expression<Func<T, IComparable>> propertyExpression)
		{
			var comparer = ComparerFactory<T>.Create(propertyExpression);
			return Sort(source, comparer);
		}

		public static ListCollectionView Sort<T>(this IEnumerable<T> source, Func<T, T, int> comparerFunc)
		{
			var comparer = ComparerFactory<T>.Create(comparerFunc);
			return Sort(source, comparer);
		}

		public static ListCollectionView Sort<T>(this IEnumerable<T> source, IComparer<T> comparer)
		{
			Enforce.Argument(() => comparer);

			return SetComparer(source, comparer);
		}

		public static ListCollectionView RemoveSort<T>(this IEnumerable<T> source)
		{
			return SetComparer(source, (IComparer)null);
		}

		internal static ListCollectionView Sort<T>(this IEnumerable<T> source, IComparer comparer)
		{
			return SetComparer(source, comparer);
		}

		private static ListCollectionView SetComparer<T>(this IEnumerable<T> source, IComparer<T> comparer)
		{
			var nonGenericComparer = (comparer == null) ? null : comparer.ToNonGeneric();
			return SetComparer(source, nonGenericComparer);
		}

		private static ListCollectionView SetComparer<T>(this IEnumerable<T> source, IComparer comparer)
		{
			return
				Application.Current.Sync().Execute(() =>
				{
					var listView = source.ListView();

					return SetComparer(listView, comparer);
				});
		}

		private static ListCollectionView SetComparer(this ListCollectionView listView, IComparer comparer)
		{
			listView.CustomSort = comparer;

			listView.Refresh();

			return listView;
		}
	}
}