namespace SharpTestsEx.Assertions
{
	public interface IMessageComposer<TA>
	{
		string GetMessage(TA actual, string customMessage);
	}
}