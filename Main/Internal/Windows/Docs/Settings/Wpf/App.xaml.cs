namespace EyeSoft.Docs.Settings.Windows
{
	using EyeSoft.Docs.Settings.ViewModels;
	using EyeSoft.Serialization;
	using EyeSoft.Windows.Model;

	using Newtonsoft.Json;

	public partial class App
	{
		public App()
		{
			Serializer.Set(new JsonSerializerFactory());

			DialogService.Show<MainViewModel>();
		}
	}
}