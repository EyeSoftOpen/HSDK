namespace EyeSoft.Windows.Model.Collections.Filter
{
    using System.Collections.Generic;

    public interface IFilterable
	{
		IEnumerable<string> Keys { get; }
	}
}