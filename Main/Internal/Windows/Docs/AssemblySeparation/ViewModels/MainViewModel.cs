namespace EyeSoft.AssemblySeparation.ViewModels
{
	using System;
    using Windows.Model.ViewModels;
    using EyeSoft.Windows.Model;

	public class MainViewModel : ViewModel
	{
		public MainViewModel()
		{
			Property(() => Name).OnChanged(x => Console.WriteLine("Changed name to {0}.", x));
		}

		public string Name
		{
			get => GetProperty<string>();
            set => SetProperty(value);
        }
	}
}
