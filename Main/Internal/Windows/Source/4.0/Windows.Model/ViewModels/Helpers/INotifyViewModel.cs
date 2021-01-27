namespace EyeSoft.Windows.Model.ViewModels.Helpers
{
	internal interface INotifyViewModel
	{
		void OnPropertyChanging(string propertyName);

		void OnPropertyChanged(string propertyName);
	}
}