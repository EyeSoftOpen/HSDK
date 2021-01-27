namespace EyeSoft.Docs.Settings.Windows
{
    using Core.Serialization;
    using EyeSoft.Docs.Settings.ViewModels;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.DialogService;
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