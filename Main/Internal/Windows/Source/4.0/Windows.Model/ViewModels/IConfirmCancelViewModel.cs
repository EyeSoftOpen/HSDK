namespace EyeSoft.Windows.Model.ViewModels
{
    using System.Windows.Input;

    public interface IConfirmCancelViewModel
	{
		ICommand ConfirmCommand { get; }

		ICommand CancelCommand { get; }
	}
}