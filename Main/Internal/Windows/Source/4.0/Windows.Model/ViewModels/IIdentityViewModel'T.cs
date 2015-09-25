using System;

namespace EyeSoft.Windows.Model.ViewModels
{
	public interface IIdentityViewModel<out T> where T : IComparable
	{
		T Id { get; }
	}
}