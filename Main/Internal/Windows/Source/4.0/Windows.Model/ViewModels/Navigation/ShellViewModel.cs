namespace EyeSoft.Windows.Model
{
	public abstract class ShellViewModel : AutoRegisterViewModel, INavigableViewModel
	{
		public NavigableViewModel Content
		{
			get { return GetProperty<NavigableViewModel>(); }
			private set { SetProperty(value); }
		}

		public override bool CanClose()
		{
			return Content != null ? Content.CanClose() : base.CanClose();
		}

		void INavigableViewModel.Navigate(NavigableViewModel navigable)
		{
			Content = navigable;
		}

		void INavigableViewModel.Close()
		{
			Close();
		}

		protected internal override void Activated()
		{
			base.Activated();

			if (Content != null)
			{
				Content.Activated();
			}
		}

		protected internal override void KeyDown(System.Windows.Input.KeyEventArgs keyEventArgs)
		{
			base.KeyDown(keyEventArgs);

			if (Content != null)
			{
				Content.KeyDown(keyEventArgs);
			}
		}

		protected internal override void Release()
		{
			if (Content != null)
			{
				Content.Release();
			}

			base.Release();
		}

		protected internal override void Dispose(bool disposing)
		{
			if (Content != null)
			{
				Content.Dispose(disposing);
			}

			base.Dispose(disposing);
		}

		protected void Navigate(NavigableViewModel navigable)
		{
			((INavigableViewModel)this).Navigate(navigable);
		}
	}
}