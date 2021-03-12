namespace EyeSoft.Windows.Model.Test.Input
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

	[TestClass]
	public class AutoRegisterViewModelCommandWithParameterTest
	{
		[TestMethod]
		public void CheckAssignedCommandIsGenericCommandForMethodWithParameter()
		{
			var viewModel = new ViewModelCommandWithParameter();

			viewModel.ShowChildCommand.Should().NotBeNull();

			viewModel.ShowChildCommand.Should().BeOfType<AsyncRefreshCommand<string>>();
		}

	    [TestMethod]
	    public void UpdateViewModelPropertiesAndCheckIfCommandCanExecuteWasUpdated()
	    {
	        var viewModel = new ViewModelCommandWithParameter();

	        viewModel.CanShowChild(null).Should().BeFalse();

	        viewModel.AllowCommand = true;

	        viewModel.CanShowChild(null).Should().BeTrue();
        }

        private class ViewModelCommandWithParameter : AutoRegisterViewModel
		{
		    public bool AllowCommand
		    {
		        get => GetProperty<bool>();
                set => SetProperty(value);
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