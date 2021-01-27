namespace EyeSoft.Windows.Model.ViewModels
{
    using System;

    public interface IIdentityViewModel<out T> where T : IComparable
	{
		T Id { get; }
	}
}