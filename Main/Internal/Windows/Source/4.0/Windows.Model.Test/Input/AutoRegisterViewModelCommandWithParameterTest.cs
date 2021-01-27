namespace EyeSoft.Windows.Model.Test.Input
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

	[TestClass]
	public class AutoRegisterViewModelCommandWithParameterTest
	{
		[TestMethod]
		public void CheckAssignedCommandIsGenericCommandForMethodWithParameter()
		{
			var viewModel = new ViewModelCommandWithParameter();

			viewModel.ShowChildCommand.Should().Not.Be.Null();

			viewModel.ShowChildCommand.Should().Be.OfType<AsyncRefreshCommand<string>>();
		}

	    [TestMethod]
	    public void UpdateViewModelPropertiesAndCheckIfCommandCanExecuteWasUpdated()
	    {
	        var viewModel = new ViewModelCommandWithParameter();

	        viewModel.CanShowChild(null).Should().Be.False();

	        viewModel.AllowCommand = true;

	        viewModel.CanShowChild(null).Should().Be.True();
        }

        private class ViewModelCommandWithParameter : AutoRegisterViewModel
		{
		    public bool AllowCommand
		    {
		        get { return GetProperty<bool>(); }
		        set { SetProperty(value); }
            }

			public ICommand ShowChildCommand { get; private set; }

		    public void ShowChild(string param)
			{
				Console.WriteLine(param);
			}

		    public bool CanShowChild(string param)
		    {
		        return AllowCommand;
		    }

        }
	}
}