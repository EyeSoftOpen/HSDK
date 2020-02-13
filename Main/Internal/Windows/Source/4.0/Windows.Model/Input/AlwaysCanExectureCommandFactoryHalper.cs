namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Threading.Tasks;
	using System.Windows.Input;

	public static class AlwaysCanExectureCommandFactoryHalper
	{
		public static ICommandFactory CreateCommandFactory(bool isAsync, IViewModelChecker viewModelChecker)
		{
			return new AlwaysCanExecuteCommandFactory(isAsync, viewModelChecker);
		}

		private class AlwaysCanExecuteCommandFactory : ICommandFactory
		{
			private readonly bool isAsync = true;
			private readonly IViewModelChecker viewModelChecker;

			public AlwaysCanExecuteCommandFactory(bool isAsync, IViewModelChecker viewModelChecker)
			{
				this.isAsync = isAsync;
				this.viewModelChecker = viewModelChecker;
			}

			public ICommand Create(IViewModel viewModel, Action action)
			{
				return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action);
			}

			public ICommand Create(IViewModel viewModel, Action action, Func<bool> canExecute)
			{
				return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, canExecute);
			}

			public ICommand CreateAsync(IViewModel viewModel, Action action)
			{
				return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, isAsync);
			}

			public ICommand CreateAsync(IViewModel viewModel, Action action, Func<bool> canExecute)
			{
				return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, canExecute, isAsync);
			}

			public ICommand Create<T>(IViewModel viewModel, Action<T> action)
			{
				return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action);
			}

			public ICommand Create<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
			{
				return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, canExecute);
			}

			public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action)
			{
				return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, isAsync);
			}

			public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
			{
				return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, canExecute, isAsync);
			}
		}

		private class AlwaysCanExecuteCommand<T> : AsyncRefreshCommand<T>
		{
			private readonly IViewModelChecker viewModelChecker;

			public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action<T> execute, bool isAsync = false)
				: this(viewModelChecker, viewModel, execute, null, isAsync)
			{
			}

			public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action<T> execute, Func<T, bool> canExecute, bool isAsync = false)
				: base(viewModel, execute, canExecute, isAsync)
			{
				this.viewModelChecker = viewModelChecker;
			}

			public override bool CanExecute(object parameter)
			{
				return true;
			}

			public override void Execute(object parameter)
			{
				if (!CanExecuteInternal(parameter))
				{
					viewModelChecker.Check(ViewModel);

					return;
				}

				if (!IsAsync)
				{
					ExecuteAction((T)parameter);
				}
				else
				{
					Task.Factory.StartNew(() =>
					{
						ExecuteAction((T)parameter);
					});
				}
			}

			private bool CanExecuteInternal(object parameter)
			{
				if (CanExecuteFunc == null)
				{
					return true;
				}

				if (IsAsync)
				{
					return Task.Factory.StartNew(() => CanExecuteFunc((T)parameter)).Result;
				}

				return CanExecuteFunc((T)parameter);
			}
		}

		private class AlwaysCanExecuteCommand : AsyncRefreshCommand
		{
			private readonly IViewModelChecker viewModelChecker;

			public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action execute, bool isAsync = false)
				: this(viewModelChecker, viewModel, execute, null, isAsync)
			{
			}

			public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action execute, Func<bool> canExecute, bool isAsync = false)
				: base(viewModel, execute, canExecute, isAsync)
			{
				this.viewModelChecker = viewModelChecker;
			}

			public override bool CanExecute(object parameter)
			{
				return true;
			}

			public override void Execute(object parameter)
			{
				if (!CanExecuteInternal())
				{
					viewModelChecker.Check(ViewModel);

					return;
				}

				ExecuteAction();
			}

			private bool CanExecuteInternal()
			{
				if (CanExecuteFunc == null)
				{
					return true;
				}

				if (IsAsync)
				{
					return Task.Factory.StartNew(CanExecuteFunc).Result;
				}

				return CanExecuteFunc();
			}
		}
	}
}