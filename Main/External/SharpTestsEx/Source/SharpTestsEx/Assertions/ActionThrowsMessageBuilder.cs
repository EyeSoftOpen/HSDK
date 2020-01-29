using System;

namespace SharpTestsEx.Assertions
{
	public class ActionThrowsMessageBuilder: IMessageBuilder<Type, Type>
	{
		#region Implementation of IMessageBuilder<Type,Type>

		public string Compose(MessageBuilderInfo<Type, Type> info)
		{
			var a = info.Actual;
			var e = info.Expected;
			if (a == null)
			{
				return string.Format("The action {0} {1} {2}.{3}", Properties.Resources.AssertionVerb, Properties.Resources.PredicateThrow, e, info.CustomMessage);
			}
			else
			{
				return string.Format("The action {0} {1} {2}.{3}\nThrew: {4}", Properties.Resources.AssertionVerb, Properties.Resources.PredicateThrow, e, info.CustomMessage, a);
			}
		}

		#endregion
	}
}