namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface
{
    using System;
    using System.Linq.Expressions;

    public interface IChangeViewModelProperty<out TProperty>
	{
		IChangeViewModelProperty<TProperty> DependsFrom(params Expression<Func<object>>[] dependentFrom);
	}
}