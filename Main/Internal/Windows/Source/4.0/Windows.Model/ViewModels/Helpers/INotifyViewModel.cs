namespace EyeSoft.Windows.Model
{
	internal interface INotifyViewModel
	{
		void OnPropertyChanging(string propertyName);

		void OnPropertyChanged(string propertyName);
	}
}