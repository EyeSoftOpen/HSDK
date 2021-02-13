namespace EyeSoft.Windows.Model
{
	using System;

	public interface IIdentityViewModel<out T> where T : IComparable
	{
		T Id { get; }
	}
}