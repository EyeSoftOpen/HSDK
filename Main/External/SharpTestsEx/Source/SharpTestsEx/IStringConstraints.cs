namespace SharpTestsEx
{
	public interface IStringConstraints : IBeConstraints<IStringBeConstraints>, INegableConstraints<IStringConstraints>,
																				IConstraints<string>, IAllowClone
	{
	}

	public interface IStringBeConstraints : IChildAndChainableConstraints<string, IStringConstraints>
	{
		IAndConstraints<IStringConstraints> EqualTo(string expected);
		IAndConstraints<IStringConstraints> Null();
		IAndConstraints<IStringConstraints> Empty();
	}
}