namespace EyeSoft.Docs.Settings.ViewModels
{
	using EyeSoft.Data;
	using EyeSoft.Windows.Model;

	public class MainViewModel : ViewModel
	{
		private readonly ApplicationDataSettings<MainViewModel> dataSettings =
			ApplicationInfo.Instance.Settings<MainViewModel>("Test");

		private bool isInitialized;

		public string Name
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public string Address
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public override bool CanClose()
		{
			dataSettings.Save(this);
			return true;
		}

		protected override void Activated()
		{
			if (isInitialized)
			{
				return;
			}

			isInitialized = true;

			var settings = dataSettings.Load(() => new MainViewModel());

			Name = settings.Name;
			Address = settings.Address;
		}
	}
}