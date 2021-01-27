namespace EyeSoft.Windows.Model.ServiceProxy.Item
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Mapping;
    using Property;
    using Threading;

    internal class ItemFilled<TService, T, TProperty> :
		IItemFilled<TProperty>,
		IItemPropertyFilled<TProperty>
		where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly Func<TService, T> func;

		private readonly Action<TProperty> setProperty;

		private Task<TProperty> task;

		private TProperty value;

		public ItemFilled(
			LoaderParams<TService> loaderParams,
			Func<TService, T> func,
			Action<TProperty> setProperty)
		{
			this.loaderParams = loaderParams;
			this.func = func;
			this.setProperty = setProperty;

			if (setProperty == null)
			{
				return;
			}

			LoadData();
		}

		public void Completed(Action<TProperty> action)
		{
			if (setProperty == null)
			{
				LoadData();
			}

			if (!loaderParams.UseTaskFactory)
			{
				action(value);
				return;
			}

			task.ContinueWith(t => action(t.Result), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		public void Completed<TConverted>(Action<TConverted> action)
		{
			if (setProperty == null)
			{
				LoadData();
			}

			Action<TProperty> convertAndExectueCompleted =
				item =>
					{
						var converted = default(TConverted);
						
						// ReSharper disable once AccessToStaticMemberViaDerivedType
						// ReSharper disable once RedundantNameQualifier
						if (!Comparer.Equals(item, default(T)))
						{
							converted = Mapper.Map<TConverted>(item);
						}

						action(converted);
					};

			if (task == null)
			{
				convertAndExectueCompleted(value);
				return;
			}

			task.ContinueWith(t => convertAndExectueCompleted(t.Result), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}

		private TProperty LoadItem()
		{
			var dataService = new DataService<TService>(loaderParams.ProxyCreator);

			var data = dataService.LoadItem(func);

			if (Comparer.Equals(data, default(T)))
			{
				return default(TProperty);
			}

			var toProperty = Mapper.Map<TProperty>(data);

			return toProperty;
		}

		private void LoadData()
		{
			Func<TProperty> load = LoadItem;

			if (!loaderParams.UseTaskFactory)
			{
				value = Mapper.Map<TProperty>(load());

				if (setProperty != null)
				{
					setProperty(value);
				}

				return;
			}

			Func<TProperty> function = () =>
				{
					var data = load();

					if (setProperty != null)
					{
						setProperty(data);
					}

					return data;
				};

			task = Task.Factory.StartNew(function);
		}
	}
}