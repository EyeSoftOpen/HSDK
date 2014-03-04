namespace EyeSoft.Transfer.Service.Test.Helpers
{
	internal class BooleanCondition
	{
		public BooleanCondition(bool condition)
		{
			Value = condition;
		}

		public bool Value { get; private set; }
	}
}