namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System.Collections.Generic;
    using Core.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

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

			viewModel.ChangeList.Should().Have.SameSequenceAs(primary, dependent, primary, dependent);
		}

		private class DependentViewModel : ViewModel
		{
			private readonly IList<string> changeList = new List<string>();

			public DependentViewModel()
			{
				Property(() => Dependent).DependsFrom(() => Primary);
			}

			public string Dependent
			{
				get { return "DependentValue"; }
			}

			public string Primary
			{
				get { return GetProperty<string>(); }
				set { SetProperty(value); }
			}

			public IEnumerable<string> ChangeList
			{
				get { return changeList; }
			}

			protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
			{
				changeList.Add(e.PropertyName);
				base.OnPropertyChanged(e);
			}
		}
	}
}