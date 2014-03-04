namespace EyeSoft.Test.Helpers
{
	internal static class KnownExpressions
	{
		public static readonly ExpressionConstant SingleProperty =
			new ExpressionConstant(
				"obj => obj.Name = 'Bill'",
				obj => obj.Name == "Bill");

		public static readonly ExpressionConstant MultipleProperty =
			new ExpressionConstant(
				"obj => obj.Name = 'Bill' & obj.Age > 3",
				obj => obj.Name == "Bill" && obj.Age > 3);
	}
}