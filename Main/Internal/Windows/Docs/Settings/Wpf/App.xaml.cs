namespace EyeSoft.Docs.Settings.Windows
{
    using EyeSoft.Serialization;
    using EyeSoft.Docs.Settings.ViewModels;
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