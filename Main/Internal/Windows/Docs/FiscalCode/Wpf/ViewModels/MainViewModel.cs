namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels
{
	using System.Threading.Tasks;
    using Calculate;
    using EyeSoft.Accounting.Italian.Istat;
	using EyeSoft.Windows.Model;
    using Validate;

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
        }

		public ValidateFiscalCodeViewModel ValidateFiscalCode
		{
			get;
        }
	}
}