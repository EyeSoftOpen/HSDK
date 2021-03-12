namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

	[TestClass]
	public class ViewModelTest
	{
		private const string ExpectedPropertyName = "Name";

		private int order;

		[TestMethod]
		public void ChangeViewModelPropertyVerifyPropertyChangedEventIsRaised()
		{
			var viewModel = CreateViewModelAndAssignProperty();

			viewModel.Name = ExpectedPropertyName;
			order.Should().Be(2);
		}

		[TestMethod]
		public void ChangeViewModelPropertyMultipleTimesWithTheSameValueVerifyPropertyChangedEventIsRaisedOne()
		{
			var viewModel = CreateViewModelAndAssignProperty();

			viewModel.Name = ExpectedPropertyName;
			order.Should().Be(2);

			viewModel.Name = ExpectedPropertyName;
			order.Should().Be(2);
		}

		[TestMethod]
		public void ChangeViewModelPropertyVerifyPropertyChangedEventIsRaisedUsingTheExpressionProperty()
		{
			var viewModel = new ViewModelWithExpressionOnPropertyChanged { Name = "Test" };

			string propertyName = null;
			viewModel.PropertyChanged += (s, e) => propertyName = e.PropertyName;

			viewModel.Name = "Test1";

			propertyName.Should().Be("Name");
		}

		private ViewModel1 CreateViewModelAndAssignProperty()
		{
			var viewModel = new ViewModel1();

			viewModel.PropertyChanging += (s, e) => CheckPropertyName(e.PropertyName, ExpectedPropertyName, 0);

			viewModel.PropertyChanged += (s, e) => CheckPropertyName(e.PropertyName, ExpectedPropertyName, 1);

			return viewModel;
		}

		private void CheckPropertyName(
			string propertyName,
			string expectedPropertyName,
			int expectedOrder)
		{
			propertyName.Should().Be(expectedPropertyName);
			order.Should().Be(expectedOrder);
			order++;
		}

		private class ViewModel1 : ViewModel
		{
			public string Name
			{
				get => GetProperty<string>();
                set => SetProperty(value);
            }
		}

		private class ViewModelWithExpressionOnPropertyChanged : ViewModel
		{
			private string name;

			public string Name
			{
				get => name;
                set
				{
					name = value;
					OnPropertyChanged(() => Name);
				}
			}
		}
	}
}