namespace EyeSoft.Windows.Model.ViewModels
{
    using DialogService;

    public abstract class ConfirmCancelViewModel<TRet> : ConfirmCancelViewModel, IDialogViewModel<TRet>
	{
		public abstract TRet Result { get; }
	}
}