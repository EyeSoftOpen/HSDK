using System.Collections.Generic;

namespace EyeSoft.Windows.Model.Collections.Filter
{
	public interface IFilterable
	{
		IEnumerable<string> Keys { get; }
	}
}