namespace EyeSoft.Windows.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Runtime.CompilerServices;
	using System.Windows;
	using System.Windows.Input;

	using EyeSoft.Reflection;
	using EyeSoft.Validation;

	public abstract class ViewModel : INotifyPropertyChanged,
									INotifyPropertyChanging,
									INotifyViewModel,
									IDataErrorInfo,
									IDisposable
	{
		private readonly ViewModelProperties viewModelProperties;

		private readonly PropertyInfoDictionary propertyInfoDictionary;

		private readonly HashSet<string> createdViewModelProperties = new HashSet<string>();

		private bool disposed;

		private bool hasErrors;

		protected ViewModel()
		{
			propertyInfoDictionary = new PropertyInfoDictionary(this);

			viewModelProperties = new ViewModelProperties(this);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;

		public bool IsPropertyChangedSuspended { get; private set; }

		public int Changes
		{
			get
			{
				return viewModelProperties.Changes;
			}
		}

		public bool Changed
		{
			get
			{
				return viewModelProperties.Changes > 0;
			}
		}

		public virtual bool IsValid
		{
			get
			{
				return !hasErrors;
			}
		}

		public string Error
		{
			get
			{
				var error = GetError(Validate);
				return error;
			}
		}

		public string this[string propertyName]
		{
			get
			{
				var error = GetError(() => Validate(propertyName));

				hasErrors = !error.IsNullOrWhiteSpace();

				HandlePropertyChanged(nameof(IsValid));

				return error;
			}
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		public virtual bool CanClose()
		{
			return IsValid;
		}

		public void ResumePropertyChanged()
		{
			IsPropertyChangedSuspended = false;
		}

		public void SuspendPropertyChanged()
		{
			IsPropertyChangedSuspended = true;
		}

		void INotifyViewModel.OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(propertyName);
		}

		void INotifyViewModel.OnPropertyChanging(string propertyName)
		{
			OnPropertyChanging(propertyName);
		}

		protected internal virtual void Activated()
		{
		}

		protected internal virtual void KeyDown(KeyEventArgs keyEventArgs)
		{
		}

		protected internal virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				Release();
			}

			disposed = true;
		}

		protected internal virtual void Release()
		{
		}

		protected internal virtual void Shutdown()
		{
			Sync().Execute(() => Application.Current.Shutdown());
		}

		protected virtual void Close()
		{
			Sync().Execute(() => DialogService.Close(this));
		}

		protected SyncExecution Sync()
		{
			return new SyncExecution(Application.Current);
		}

		protected AsyncExecution Async()
		{
			return new AsyncExecution(Application.Current);
		}

		protected virtual IEnumerable<ValidationError> Validate()
		{
			return new DefaultValidator().Validate(this);
		}

		protected virtual IEnumerable<ValidationError> Validate(string propertyName)
		{
			return Validate().Where(x => x.PropertyName == propertyName);
		}

		#region property get/set

		protected T GetProperty<T>([CallerMemberName] string callerName = "")
		{
			var propertyName = callerName;
			return viewModelProperties.GetProperty<T>(propertyInfoDictionary[propertyName]);
		}

		protected T GetPropertyValue<T>(string propertyName)
		{
			return viewModelProperties.GetProperty<T>(propertyInfoDictionary[propertyName]);
		}

		protected void SetProperty<T>(T value, bool suspendPropertyChanged = false, [CallerMemberName] string callerName = "")
		{
			var propertyChangedSuspended = suspendPropertyChanged || IsPropertyChangedSuspended;

			var propertyName = callerName;
			viewModelProperties.SetProperty(propertyInfoDictionary[propertyName], value, propertyChangedSuspended);
		}

		#endregion

		protected IViewModelProperty<TProperty> Property<TProperty>(Expression<Func<TProperty>> propertyExpression)
		{
			var propertyName = propertyExpression.PropertyName();

#if DEBUG
			if (createdViewModelProperties.Contains(propertyName))
			{
				var message = string.Format("Cannot define the behavior on the property '{0}' more than once.", propertyName);
				throw new InvalidOperationException(message);
			}
#endif

			createdViewModelProperties.Add(propertyName);

			return viewModelProperties.Property<TProperty>(propertyInfoDictionary[propertyName]);
		}

		#region PropertyChanged

		protected void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged(Expression<Func<object>> propertyExpression)
		{
			var propertyName = propertyExpression.PropertyName();

			((INotifyViewModel)this).OnPropertyChanged(propertyName);
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (IsPropertyChangedSuspended)
			{
				return;
			}

			viewModelProperties.Changed(
				propertyInfoDictionary[e.PropertyName],
				() => propertyInfoDictionary.GetPropertyValue(e.PropertyName));

			HandlePropertyChanged(e);
		}

		private void HandlePropertyChanged(string propertyName)
		{
			HandlePropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		private void HandlePropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion

		#region PropertyChanging

		protected void OnPropertyChanging(string propertyName)
		{
			OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
		}

		protected void OnPropertyChanging(Expression<Func<object>> propertyExpression)
		{
			var propertyName = propertyExpression.PropertyName();

			((INotifyViewModel)this).OnPropertyChanging(propertyName);
		}

		protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
		{
			if (IsPropertyChangedSuspended)
			{
				return;
			}

			viewModelProperties
				.Changing(propertyInfoDictionary[e.PropertyName], () => propertyInfoDictionary.GetPropertyValue(e.PropertyName));

			var handler = PropertyChanging;

			if (handler != null)
			{
				handler(this, e);
			}
		}
		#endregion

		private string GetError(Func<IEnumerable<ValidationError>> validate)
		{
			var errors = validate().ToList();

			return !errors.Any() ? string.Empty : errors.Select(x => x.Message).JoinMultiLine();
		}
	}
}