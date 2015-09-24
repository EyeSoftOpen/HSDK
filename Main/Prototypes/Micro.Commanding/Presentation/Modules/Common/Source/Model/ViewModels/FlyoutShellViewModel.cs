namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
    using EyeSoft.Windows.Model;

    public abstract class FlyoutShellViewModel : ShellViewModel
    {
        public ViewModel FlyoutDataContext
        {
            get { return GetProperty<ViewModel>(); }
            set { SetProperty(value); }
        }

        public bool ShowFlyout
        {
            get { return GetProperty<bool>(); }
            set { SetProperty(value); }
        }
    }
}