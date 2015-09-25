using System;
using System.Linq.Expressions;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
	public interface IChangeViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> DependsFrom(params Expression<Func<object>>[] dependentFrom);
	}
}