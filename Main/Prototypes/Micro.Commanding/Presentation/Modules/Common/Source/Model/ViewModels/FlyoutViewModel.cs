namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
    using EyeSoft.Windows.Model;

    public class FlyoutViewModel : AutoRegisterViewModel
    {
        public string Title { get; set; }

        public string Detail
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}