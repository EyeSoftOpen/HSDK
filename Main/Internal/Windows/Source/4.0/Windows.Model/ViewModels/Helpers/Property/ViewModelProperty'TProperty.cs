namespace EyeSoft.Windows.Model
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	using EyeSoft.Reflection;

	internal class ViewModelProperty<TProperty> : ViewModelProperty, IViewModelProperty<TProperty>
	{
		private readonly Action<string, string[]> addToDependent;

		public ViewModelProperty(
			INotifyViewModel nofiyViewModelViewModel,
			Action<string, string[]> addToDependent,
			string propertyName) : base(propertyName, nofiyViewModelViewModel)
		{
			this.addToDependent = addToDependent;
		}

		IFirstChangeViewModelProperty<TProperty> IViewModelProperty<TProperty>.OnFirstChanging(Action<TProperty> onChangeAction)
		{
			changingActions.AddChangeActionOn(onChangeAction, 0);
			return this;
		}

		IAfterFirstChangeViewModelProperty<TProperty>
			IFirstChangeViewModelProperty<TProperty>.OnFirstChange(Action<TProperty> onChangeAction)
		{
			changedActions.AddChangeActionOn(onChangeAction, 1);
			return this;
		}

		IChangeViewModelProperty<TProperty> IChangingViewModelProperty<TProperty>.OnChanged(Action<TProperty> onChangeAction)
		{
			changedActions.AddChangeActionOn(onChangeAction, 0);
			return this;
		}

		IChangingViewModelProperty<TProperty> IAfterFirstChangeViewModelProperty<TProperty>.OnChanging(Action<TProperty> onChangeAction)
		{
			changingActions.AddChangeActionOn(onChangeAction, 0);
			return this;
		}

		IChangeViewModelProperty<TProperty>
			IChangeViewModelProperty<TProperty>.DependsFrom(params Expression<Func<object>>[] propertiesExpression)
		{
			var propertyNames = propertiesExpression.Select(property => property.PropertyName()).ToArray();

			addToDependent(PropertyName, propertyNames);

			return this;
		}
	}
}