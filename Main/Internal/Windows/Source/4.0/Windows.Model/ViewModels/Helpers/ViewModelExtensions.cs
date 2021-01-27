namespace EyeSoft.Windows.Model.ViewModels.Helpers
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Core.Reflection;

    public static class ViewModelExtensions
	{
		// TODO: UPGRADE FW 45 - decomment using Framework 4.5
		////public static void SetFieldChanged<T>(
		////	this INotifyPropertyChanged viewModel,
		////	T value,
		////	[CallerMemberName] string propertyName = "")
		////{
		////	InternalSetFieldChanged(viewModel, value, propertyName);
		////}

		public static void RaisePropertyChanged<TViewModel>(this TViewModel viewModel, string propertyName)
			where TViewModel : INotifyPropertyChanged
		{
			viewModel.Execute("OnPropertyChanged", propertyName);
		}

		internal static void SetFieldValue<T>(this INotifyPropertyChanged viewModel, T value, string propertyName)
		{
			var viewModelType = viewModel.GetType();

			var field = viewModelType.Field(propertyName);
			var original = (T)field.GetValue(viewModel);

			if (EqualityComparer<T>.Default.Equals(value, original))
			{
				return;
			}

			field.SetValue(viewModel, value);

			viewModel.RaisePropertyChanged(propertyName);
		}
	}
}