namespace EyeSoft.Windows.Model
{
	using System.Windows.Input;

	public abstract class ConfirmCancelViewModel : AutoRegisterViewModel, IConfirmCancelViewModel
	{
		private bool cancelled;

		public ICommand ConfirmCommand { get; protected set; }

		public ICommand CancelCommand { get; protected set; }

		public override bool CanClose()
		{
			return IsValid || cancelled;
		}

		protected abstract void Confirm();

		protected virtual void Cancel()
		{
			cancelled = true;
			Close();
		}

		protected virtual bool CanConfirm()
		{
			return IsValid;
		}

		protected virtual bool CanCancel()
		{
			return true;
		}
	}
}