using System.Windows.Input;

namespace EyeSoft.Windows.Model.ViewModels
{
	public interface IConfirmCancelViewModel
	{
		ICommand ConfirmCommand { get; }

		ICommand CancelCommand { get; }
	}
}