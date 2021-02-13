namespace EyeSoft.Windows.Model.Test.Helpers.ViewModels
{
    using EyeSoft.Windows.Model;

    internal class ChildDialogViewModel : ViewModel
	{
		public ChildDialogViewModel(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}