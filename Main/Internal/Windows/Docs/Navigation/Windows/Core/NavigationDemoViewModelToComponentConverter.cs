namespace EyeSoft.Demo.Navigation.Windows.Presentation.Core
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Demo.Navigation.Windows.ViewModels;
	using EyeSoft.Windows;
	using EyeSoft.Windows.Converters;

	public class NavigationDemoViewModelToComponentConverter : ViewModelToComponentConverter
	{
		private static readonly IDictionary<Type, Uri> viewModelTypeToResource =
			new Dictionary<Type, Uri>
				{
					{ typeof(WelcomeViewModel), Component.ToUri(@"Welcome", "Views", typeof(NavigationDemoViewModelToComponentConverter).Assembly) },
					{ typeof(TimeViewModel), Component.ToUri(@"Time", "Views", typeof(NavigationDemoViewModelToComponentConverter).Assembly) }
				};

		protected override Uri Get(Type viewModelType)
		{
			var containsKey = viewModelTypeToResource.ContainsKey(viewModelType);

			return containsKey ? viewModelTypeToResource[viewModelType] : base.Get(viewModelType);
		}
	}
}