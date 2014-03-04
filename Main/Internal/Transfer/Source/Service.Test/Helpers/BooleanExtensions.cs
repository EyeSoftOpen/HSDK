namespace EyeSoft.Transfer.Service.Test.Helpers
{
	internal static class BooleanExtensions
	{
		public static BooleanCondition OnTrue(this bool condition)
		{
			return new BooleanCondition(condition);
		}
	}
}