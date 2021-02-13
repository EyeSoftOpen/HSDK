namespace EyeSoft.Windows.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Windows;
	using System.Windows.Input;
    using Extensions;
    using EyeSoft.Windows.Model.Conventions;

	public class DefaultDialogService : IDialogService
	{
		private static readonly MethodInfo keyDownMethod =
			typeof(ViewModel).GetMethod("KeyDown", BindingFlags.Instance | BindingFlags.NonPublic);

		private static readonly MethodInfo activatedMethod =
			typeof(ViewModel).GetMethod("Activated", BindingFlags.Instance | BindingFlags.NonPublic);

		private readonly Func<object, Window> viewModelToView;

		private readonly IMessageBox messageBox;

		private readonly IResolverLocator resolverLocator;

		private readonly ViewModelToViewConvention viewModelToViewConvention;

		private readonly HashSet<Window> openedWindows = new HashSet<Window>();

		public DefaultDialogService() : this((IResolverLocator)null)
		{
		}

		public DefaultDialogService(Func<object, Window> viewModelToView)
		{
			this.viewModelToView = viewModelToView;
		}

		public DefaultDialogService(IResolverLocator resolverLocator)
			: this(resolverLocator, new DefaultViewModelToViewConvention())
		{
		}

		public DefaultDialogService(IMessageBox messageBox)
			: this(messageBox, null, new DefaultViewModelToViewConvention())
		{
		}

		public DefaultDialogService(IMessageBox messageBox, IResolverLocator resolverLocator)
			: this(messageBox, resolverLocator, new DefaultViewModelToViewConvention())
		{
		}

		public DefaultDialogService(IMessageBox messageBox, ViewModelToViewConvention viewModelToViewConvention)
			: this(messageBox, null, viewModelToViewConvention)
		{
		}

		public DefaultDialogService(ViewModelToViewConvention viewModelToViewConvention)
			: this((IResolverLocator)null, viewModelToViewConvention)
		{
		}

		public DefaultDialogService(
			IResolverLocator resolverLocator,
			ViewModelToViewConvention viewModelToViewConvention)
			: this(new MessageBox(), resolverLocator, viewModelToViewConvention)
		{
		}

		public DefaultDialogService(
			IMessageBox messageBox,
			IResolverLocator resolverLocator,
			ViewModelToViewConvention viewModelToViewConvention)
		{
			this.resolverLocator = resolverLocator;
			this.messageBox = messageBox;
			this.viewModelToViewConvention = viewModelToViewConvention;

			OpenedWindows = new OpenedWindowsCollection(openedWindows);
		}

		public OpenedWindowsCollection OpenedWindows { get; private set; }

		public void ShowMain<TViewModel>(params object[] arguments) where TViewModel : ViewModel
		{
			ShowWindowSafe(default(TViewModel), false, arguments, true);
		}

		public void ShowMain<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			ShowWindowSafe(viewModel, false, null, true);
		}

		public void Show<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			ShowWindowSafe(viewModel, false, null);
		}

		public void Show<TViewModel>(params object[] arguments)
			where TViewModel : ViewModel
		{
			ShowWindowSafe(default(TViewModel), false, arguments);
		}

		public void ShowModal<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			ShowWindowSafe(viewModel, true, null);
		}

		public void ShowModal<TViewModel>(params object[] arguments)
			where TViewModel : ViewModel
		{
			ShowWindowSafe(default(TViewModel), true, arguments);
		}

		public TRet ShowModal<TViewModel, TRet>(TViewModel viewModel) where TViewModel : ViewModel, IDialogViewModel<TRet>
		{
			return (TRet)ShowWindowSafe(viewModel, true, null);
		}

		public TRet ShowModal<TViewModel, TRet>(params object[] arguments) where TViewModel : ViewModel, IDialogViewModel<TRet>
		{
			return (TRet)ShowWindowSafe(default(TViewModel), true, arguments);
		}

		public virtual MessageBoxResult ShowMessage(
			string title,
			string message,
			MessageBoxButton button = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information)
		{
			Func<MessageBoxResult> action = () =>
				messageBox.ShowBox(Application.Current.GetMainWindow(), title, message, button, icon);

			return Application.Current.Sync().Execute(action);
		}

		void IDialogService.Close<TViewModel>(TViewModel viewModel)
		{
			CloseSafe(viewModel);
		}

		protected object ShowWindowSafe<TViewModel>(TViewModel viewModel, bool isModal, object[] arguments, bool isMain = false)
			where TViewModel : ViewModel
		{
			var result = Application.Current.Sync().Execute(() => ShowWindow(viewModel, isModal, arguments, isMain));

			return result;
		}

		protected virtual object ShowWindow<TViewModel>(TViewModel viewModel, bool isModal, IEnumerable<object> arguments, bool isMain)
			where TViewModel : ViewModel
		{
			var window = viewModelToView != null ? viewModelToView(viewModel) : CreateWindowUsingConvention(viewModel, arguments);

			window.Activated += ActivatedWindow;
			window.KeyDown += KeyDown;
			window.Closing += CanCloseWindow;
			window.Closed += ReleaseWindow;

			var dataContext = (ViewModel)window.DataContext;

			if (isMain)
			{
				Application.Current.MainWindow = window;
			}

			openedWindows.Add(window);

			object result = null;

			if (dataContext.GetType().Implements(typeof(IDialogViewModel<>)))
			{
				window.Closing += (s, e) => { result = (dataContext as dynamic).Result; };
			}

			if (Application.Current == null)
			{
				return result;
			}

			var owner = Application.Current.GetMainWindow();

			if ((owner != null) && (owner.GetType() != window.GetType()))
			{
				window.Owner = owner;
			}

			if (isModal)
			{
				window.ShowDialog();
			}
			else
			{
				window.Show();
			}

			return result;
		}

		protected virtual void Close<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			var window = openedWindows.SingleOrDefault(x => ReferenceEquals(x.DataContext, viewModel));

			if (window == null)
			{
				var message = string.Format("Cannot find the window for the view model of type '{0}'.", viewModel.GetType().Name);

				throw new ArgumentException(message);
			}

			window.Close();
		}

		protected virtual void ActivatedWindow(object sender, EventArgs e)
		{
			activatedMethod.Invoke(ViewModelFromSender(sender), null);
		}

		protected virtual void KeyDown(object sender, KeyEventArgs e)
		{
			keyDownMethod.Invoke(ViewModelFromSender(sender), new object[] { e });
		}

		protected virtual void CanCloseWindow(object sender, CancelEventArgs e)
		{
			var viewModel = ViewModelFromSender(sender);
			e.Cancel = !viewModel.CanClose();
		}

		protected virtual void ReleaseWindow(object sender, EventArgs e)
		{
			var window = (Window)sender;
			window.Activated -= ActivatedWindow;
			window.Closing -= CanCloseWindow;
			window.Closed -= ReleaseWindow;

			openedWindows.Remove(window);

			((ViewModel)window.DataContext).Dispose();
		}

		private static ViewModel ViewModelFromSender(object sender)
		{
			var window = (Window)sender;
			var viewModel = (ViewModel)window.DataContext;
			return viewModel;
		}

		private void CloseSafe<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			Application.Current.Sync().Execute(() => Close(viewModel));
		}

		private Window CreateWindowUsingConvention<TViewModel>(TViewModel viewModel, IEnumerable<object> arguments)
			where TViewModel : ViewModel
		{
			var viewModelType =
				viewModel == default(TViewModel) ?
					typeof(TViewModel) :
					viewModel.GetType();

			var viewType = viewModelToViewConvention.MapTo(viewModelType);

			var window =
				new ViewModelFactory(resolverLocator)
					.AssignViewModelUsingReflectionOrLocator(viewModel, viewModelType, viewType, arguments);

			return window;
		}
	}
}