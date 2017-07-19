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

    using EyeSoft.Logging;
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
                Logger.Write($"Get {nameof(IsValid)} of view model '{GetType().FullName}'");
                return !Validate().Any();
            }
        }

        public string Error
        {
            get
            {
                var error = GetError(Validate);
                HandlePropertyChanged(nameof(IsValid));

                Logger.Write($"Getting error of view model '{GetType().FullName}'");
                return error;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                Logger.Write($"Get error for property {propertyName} of '{GetType().FullName}'");

                var error = GetError(() => Validate(propertyName));

                HandlePropertyChanged(nameof(IsValid));

                return error;
            }
        }

        public void Dispose()
        {
            Logger.Write($"Disposing view model '{GetType().FullName}'");

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual bool CanClose()
        {
            Logger.Write($"{nameof(CanClose)} view model '{GetType().FullName}'. Value {IsValid}");

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

        public void ResetPropertyChanges()
        {
            viewModelProperties.ResetPropertyChanges();
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

            return GetPropertyValue<T>(propertyName);
        }

        protected T GetPropertyValue<T>(string propertyName)
        {
            var value = viewModelProperties.GetProperty<T>(propertyInfoDictionary[propertyName]);

            Logger.Write($"{nameof(GetPropertyValue)} '{propertyName}' value '{value}'");

            return value;
        }

        protected void SetProperty<T>(
            T value,
            bool suspendPropertyChanged = false,
            [CallerMemberName] string callerName = "")
        {
            var propertyChangedSuspended = suspendPropertyChanged || IsPropertyChangedSuspended;

            var propertyName = callerName;

            Logger.Write($"{nameof(SetProperty)} '{propertyName}' value '{value}'");

            viewModelProperties.SetProperty(propertyInfoDictionary[propertyName], value, propertyChangedSuspended);
        }

        #endregion

        protected IViewModelProperty<TProperty> Property<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            var propertyName = propertyExpression.PropertyName();

#if DEBUG
            if (createdViewModelProperties.Contains(propertyName))
            {
                var message = $"Cannot define the behavior on the property '{propertyName}' more than once.";

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
            Logger.Write($"Property changed for property {propertyName}");

            HandlePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void HandlePropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, e);
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

            viewModelProperties.Changing(
                propertyInfoDictionary[e.PropertyName],
                () => propertyInfoDictionary.GetPropertyValue(e.PropertyName));

            var handler = PropertyChanging;

            handler?.Invoke(this, e);
        }

        #endregion

        private string GetError(Func<IEnumerable<ValidationError>> validate)
        {
            var errors = validate().ToList();

            return !errors.Any() ? string.Empty : errors.Select(x => x.Message).JoinMultiLine();
        }
    }
}