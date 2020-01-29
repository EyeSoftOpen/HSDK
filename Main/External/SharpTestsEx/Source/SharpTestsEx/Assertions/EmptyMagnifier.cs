namespace SharpTestsEx.Assertions
{
	public class EmptyMagnifier : IFailureMagnifier
	{
		#region Implementation of IFailureMagnifier

		public string Message()
		{
			return null;
		}

		#endregion
	}
}