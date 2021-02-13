namespace EyeSoft.Windows.Model.Test.Helpers.ViewModels
{
    using System.Windows.Input;

    internal class MainViewModel : AutoRegisterViewModel
	{
		private readonly IDialogService dialogService;

		public MainViewModel(IDialogService dialogService)
		{
			this.dialogService = dialogService;
		}

		public ICommand ShowChildCommand { get; private set; }

		protected void ShowChild()
		{
			dialogService.Show(new ChildDialogViewModel("DialogServiceTest"));
		}
	}
}