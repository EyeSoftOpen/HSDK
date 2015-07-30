namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels
{
	using System.Threading.Tasks;

	using EyeSoft.Accounting.Italian.Istat;
	using EyeSoft.Windows.Model;

	public class MainViewModel : AutoRegisterViewModel
	{
		public MainViewModel()
		{
			Task.Factory.StartNew(() => new CityRepository().AllTown());

			CalculateFiscalCode = new CalculateFiscalCodeViewModel();
			ValidateFiscalCode = new ValidateFiscalCodeViewModel();
		}

		public CalculateFiscalCodeViewModel CalculateFiscalCode
		{
			get;
			private set;
		}

		public ValidateFiscalCodeViewModel ValidateFiscalCode
		{
			get;
			private set;
		}
	}
}