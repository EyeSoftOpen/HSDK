namespace EyeSoft.Docs.Settings.Wpf
{
	using EyeSoft.Docs.Settings.ViewModels;
	using EyeSoft.Serialization;
	using EyeSoft.ServiceStack.Text;
	using EyeSoft.Windows.Model;

	public partial class App
	{
		public App()
		{
			Serializer.Set(new JsonSerializerFactory());

			DialogService.Show<MainViewModel>();
		}
	}
}