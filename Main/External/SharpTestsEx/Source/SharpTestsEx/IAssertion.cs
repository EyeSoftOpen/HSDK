using System.ComponentModel;

namespace SharpTestsEx
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IAssertion<TActual>
	{
		void Assert(TActual actual, string customMessage);
	}
}