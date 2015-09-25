using EyeSoft.Windows.Model.DialogService;

namespace EyeSoft.Windows.Model.ViewModels
{
	public abstract class ConfirmCancelViewModel<TRet> : ConfirmCancelViewModel, IDialogViewModel<TRet>
	{
		public abstract TRet Result { get; }
	}
}