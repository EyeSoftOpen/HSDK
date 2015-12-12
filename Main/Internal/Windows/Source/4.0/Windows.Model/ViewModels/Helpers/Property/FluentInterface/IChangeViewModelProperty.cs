namespace EyeSoft.Windows.Model
{
	using System;
	using System.Linq.Expressions;

	public interface IChangeViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> DependsFrom(params Expression<Func<object>>[] dependentFrom);
	}
}