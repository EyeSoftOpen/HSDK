namespace EyeSoft.Windows.Model.Collections.Filter
{
    using Core.Collections.Generic;

    public class FullTextFilter<T> : IFilter where T : IFilterable
	{
		private readonly IFilterable filterable;

		private readonly string[] keywords;

		public FullTextFilter(T filterable, params string[] keywords)
		{
			this.filterable = filterable;
			this.keywords = keywords;
		}

		public bool HasToInclude()
		{
			var hasToInclude = filterable.Keys.ContainsSequence(keywords, StringComparers.Contains);

			return hasToInclude;
		}
	}
}