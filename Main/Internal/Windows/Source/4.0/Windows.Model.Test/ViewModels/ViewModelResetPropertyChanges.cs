namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

	[TestClass]
	public class ViewModelResetPropertyChanges
	{
		[TestMethod]
		public void AssignPropertyResetPropertyChangedReAssingPropertyVerifyChangesIsNotZero()
		{
			var viewModel = new MainViewModel { Customer = { Name = "Value1" } };

			const string LastValue = "Value2";

			viewModel.Customer.Name = LastValue;

			viewModel.Customer.Changed.Should().Be.True();
			viewModel.Customer.Changes.Should().Be.EqualTo(2);

			viewModel.Customer.Name = LastValue;

		    viewModel.Customer.ResetPropertyChanges();

			viewModel.Customer.Changed.Should().Be.False();
			viewModel.Customer.Changes.Should().Be.EqualTo(0);

			viewModel.Customer.Name = "Value3";
			viewModel.Customer.Changed.Should().Be.True();
			viewModel.Customer.Changes.Should().Be.EqualTo(1);
		}

		private class MainViewModel
		{
			public MainViewModel()
			{
				Customer = new CustomerViewModel();
			}

			public CustomerViewModel Customer { get; private set; }
		}

		private class CustomerViewModel : ViewModel
		{
			public string Name
			{
				get { return GetProperty<string>(); }
				set { SetProperty(value); }
			}
		}
	}
}