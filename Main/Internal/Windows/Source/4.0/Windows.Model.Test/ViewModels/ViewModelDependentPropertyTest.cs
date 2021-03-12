namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System.Collections.Generic;
    using EyeSoft.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

	[TestClass]
	public class ViewModelDependentPropertyTest
	{
		private static readonly string primary =
			Reflector.PropertyName<DependentViewModel>(x => x.Primary);

		private static readonly string dependent =
			Reflector.PropertyName<DependentViewModel>(x => x.Dependent);

		[TestMethod]
		public void ChangeViewModelPropertyVerifyPropertyChangeIsRaisedAlsoForTheDependentProperties()
		{
			var viewModel = new DependentViewModel { Primary = "Value1" };

			viewModel.Primary = "Value2";

			viewModel.ChangeList.Should().BeSameAs(new [] { primary, dependent, primary, dependent });
		}

		private class DependentViewModel : ViewModel
		{
			private readonly IList<string> changeList = new List<string>();

			public DependentViewModel()
			{
				Property(() => Dependent).DependsFrom(() => Primary);
			}

			public string Dependent => "DependentValue";

            public string Primary
			{
				get => GetProperty<string>();
                set => SetProperty(value);
            }

			public IEnumerable<string> ChangeList => changeList;

            protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
			{
				changeList.Add(e.PropertyName);
				base.OnPropertyChanged(e);
			}
		}
	}
}