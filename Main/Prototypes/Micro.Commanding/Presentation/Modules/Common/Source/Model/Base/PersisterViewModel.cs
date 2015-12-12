namespace EyeSoft.Architecture.Prototype.Windows.Model.Base
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using EyeSoft.Timers;
	using EyeSoft.Windows.Model;

	public abstract class PersisterViewModel<T> : IdentityViewModel
	{
		private readonly Type viewModelType;

		private readonly IDictionary<string, TimerAndAction> timerDictionary = new Dictionary<string, TimerAndAction>();

		private string lastChangedProperty;

		private readonly object lockInstance = new object();

		private bool saving;

		protected PersisterViewModel()
		{
			viewModelType = GetType();

			Persisters = new Persisters<T>();
		}

		protected Persisters<T> Persisters { get; }

		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var propertyName = e.PropertyName;

			var persisterType = GetPersisterType(propertyName);

			if (persisterType == null)
			{
				return;
			}

			var propertyType = viewModelType.GetProperty(propertyName).PropertyType;

			if (propertyType != typeof(string))
			{
				ApplyApiCommand(propertyName, persisterType);
				return;
			}

			if (lastChangedProperty != null && lastChangedProperty == propertyName)
			{
				lock (lockInstance)
				{
					CreateOrRestartTimer(propertyName, persisterType);
				}

				return;
			}

			CreateOrRestartTimer(propertyName, persisterType);
		}

		private Type GetPersisterType(string propertyName)
		{
			var persisterName = $"Model.ViewModels.Main.Persisters.{propertyName}{viewModelType.Name.Replace("ViewModel", null)}Persister";

			var persisterType = viewModelType.Assembly.GetType(persisterName, false);

			return persisterType;
		}

		private void CreateOrRestartTimer(string propertyName, Type persisterType)
		{
			lastChangedProperty = propertyName;

			if (!timerDictionary.ContainsKey(propertyName))
			{
				Action applyApiCommandAndDisposeTimer = () => ApplyApiCommandAndDisposeTimer(propertyName, persisterType);

				var timer = TimerFactory.Start(TimeSpan.FromSeconds(10), applyApiCommandAndDisposeTimer);
				timerDictionary.Add(propertyName, new TimerAndAction(timer, applyApiCommandAndDisposeTimer));
			}

			timerDictionary[propertyName].Timer.Stop();
			timerDictionary[propertyName].Timer.Start();
		}

		protected override void Dispose(bool disposing)
		{
			var timerAndActionsByCreation = timerDictionary.Values.OrderBy(x => x.Creation).ToArray();

			foreach (var timer in timerAndActionsByCreation)
			{
				timer.Action();
			}

			base.Dispose(disposing);
		}


		private void ApplyApiCommandAndDisposeTimer(string propertyName, Type persisterType)
		{
			lock (lockInstance)
			{
				if (saving)
				{
					return;
				}

				using (var timer = timerDictionary[propertyName].Timer)
				{
					timerDictionary.Remove(propertyName);
					timer.Stop();
				}

				saving = true;

				ApplyApiCommand(propertyName, persisterType);

				saving = false;
			}
		}

		private void ApplyApiCommand(string propertyName, Type persisterType)
		{
			var persister = Resolver.Resolve<IPersister>(persisterType);

			var command = Persisters[propertyName];

			persister.Persist(command);
		}
	}
}