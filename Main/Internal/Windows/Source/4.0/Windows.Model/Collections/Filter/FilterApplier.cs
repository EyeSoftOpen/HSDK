namespace EyeSoft.Windows.Model.Collections.Filter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Data;
    using Core.Extensions;

    public class FullTextFilterApplier<T> where T : IFilterable
	{
		private readonly IEnumerable<T> collection;

		private string[] keywords;

		public FullTextFilterApplier(IEnumerable<T> collection)
		{
			this.collection = collection;
		}

		public void Apply(string fullTextSearch)
		{
			var collectionSource = CollectionViewSource.GetDefaultView(collection);

			keywords =
				fullTextSearch.IsNullOrWhiteSpace() ?
					Enumerable.Empty<string>().ToArray() :
					fullTextSearch.ToLower().Split();

			if (!keywords.Any())
			{
				collectionSource.Filter = null;
				collectionSource.Refresh();
				return;
			}

			collectionSource.Filter = x => Filter((T)x);
			collectionSource.Refresh();
		}

		protected virtual bool Filter(T item)
		{
			var hasToIncludeByFullName = new FullTextFilter<T>(item, keywords).HasToInclude();

			return hasToIncludeByFullName;
		}
	}
}