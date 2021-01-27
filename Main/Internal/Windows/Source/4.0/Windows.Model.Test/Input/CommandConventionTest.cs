namespace EyeSoft.Windows.Model.Test.Input
{
	using System;
	using System.Windows.Input;
    using Core.Reflection;
    using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

	[TestClass]
	public class CommandConventionTest
	{
		[TestMethod]
		public void GetActionMethodFromViewModelByConvention()
		{
			var methods =
				new CommandConvention()
					.Get(typeof(ViewModel1), Reflector.Property<ViewModel1>(x => x.ShowChildCommand));

			methods.ActionMethod.MethodInfo.Name
				.Should("Cannot find the associated method to the command.").Be.EqualTo("ShowChild");

			methods.CanExecuteMethod.MethodInfo.Name
				.Should("Cannot find the associated can method to the command.").Be.EqualTo("CanShowChild");

			methods.ActionMethod.IsAsync
				.Should("The discovered command should be identified as async.").Be.True();
		}

		[TestMethod]
		public void NoSuitableMethodActionForCommandGivesAnError()
		{
			var commandProperty =
				Reflector.Property<ViewModelNoValidActionName>(x => x.ShowChildCommand);

			var methods =
				new CommandConvention()
				.Get(typeof(ViewModelNoValidActionName), commandProperty);

			methods.Errors.Should().Have.Count.EqualTo(2);
		}

		[TestMethod]
		public void IssuesWithMultipleCommandsGiveAUniqueExceptionWithAllDetails()
		{
			Action discoverCommand =
				() =>
					new CommandsDiscover()
						.Discover(new ViewModelNoValidCommands());

			const string ExpectedExceptionMessage =
				"Issues found on type ViewModelNoValidCommands:" + "\r\n" +
				"- command NewCustomerCommand" + "\r\n" +
				" - missing action: looking for NewCustomer or SyncNewCustomer method" + "\r\n" +
				"- command NewOrderCommand" + "\r\n" +
				" - missing action: looking for NewOrder or SyncNewOrder method";

			Executing
				.This(discoverCommand)
				.Should().Throw<InvalidOperationException>()
				.And.Exception.Message.Should().Be.EqualTo(ExpectedExceptionMessage);
		}

		[TestMethod]
		public void PreAssignedCommandsAreNotModifiedFromTheDiscover()
		{
			var viewModel = new ViewModelNoConsiderAssignedCommands();

			viewModel.NewCustomerCommand.Should().Be.OfType<MyAsyncCommand>();
		}

		private class ViewModel1
		{
			public ICommand ShowChildCommand { get; private set; }

			protected void ShowChild()
			{
			}

			protected bool CanShowChild()
			{
				return true;
			}
		}

		private class ViewModelNoValidActionName : AutoRegisterViewModel
		{
			public ICommand ShowChildCommand { get; private set; }

			protected void ShowChildWrong()
			{
			}
		}

		private class ViewModelNoValidCommands : AutoRegisterViewModel
		{
			public ICommand NewCustomerCommand { get; private set; }

			public ICommand NewOrderCommand { get; private set; }
		}

		private class ViewModelNoConsiderAssignedCommands : AutoRegisterViewModel
		{
			public ViewModelNoConsiderAssignedCommands()
			{
				NewCustomerCommand = new MyAsyncCommand(this, () => { });
			}

			public ICommand NewCustomerCommand { get; private set; }

			protected void NewCustomer()
			{
			}
		}

		private class MyAsyncCommand : AsyncRefreshCommand
		{
			public MyAsyncCommand(IViewModel viewModel, Action action)
				: base(viewModel, action)
			{
			}
		}
	}
}