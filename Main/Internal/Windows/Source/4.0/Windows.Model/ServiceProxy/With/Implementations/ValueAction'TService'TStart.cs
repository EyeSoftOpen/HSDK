using System;
using System.Threading.Tasks;
using EyeSoft.Mapping;

namespace EyeSoft.Windows.Model.ServiceProxy.With.Implementations
{
	internal class ValueAction<TService, TStart> : IValueAction<TService, TStart> where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly TStart value;

		public ValueAction(LoaderParams<TService> loaderParams, TStart value)
		{
			this.loaderParams = loaderParams;
			this.value = value;
		}

		public IValueExecuted<TStart> Execute<TServiceParameter>(Action<TService, TServiceParameter> action)
		{
			var dataService = new DataService<TService>(loaderParams.ProxyCreator);

			TServiceParameter parameter;

			if (typeof(TStart) != typeof(TServiceParameter))
			{
				parameter = Mapper.Map<TServiceParameter>(value);
			}
			else
			{
				parameter = (TServiceParameter)(object)value;
			}

			if (!loaderParams.UseTaskFactory)
			{
				dataService.Execute(parameter, action);

				return new ValueExecuted<TStart>(value);
			}

			var task =
				Task.Factory.StartNew(
					() =>
						{
							dataService.Execute(parameter, action);
							return value;
						});

			return new ValueExecuted<TStart>(task, value);
		}
	}
}