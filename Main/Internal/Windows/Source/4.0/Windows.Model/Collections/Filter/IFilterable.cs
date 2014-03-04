namespace EyeSoft.Windows.Model.Collections
{
	using System.Collections.Generic;

	public interface IFilterable
	{
		IEnumerable<string> Keys { get; }
	}
}