using System;
using System.ComponentModel;
using System.Reflection;
using EyeSoft.Reflection;

namespace EyeSoft.Windows.Model.ServiceProxy.Item.Property.Implementations
{
	internal class ItemLoaded<TService, TProperty>
		: IItemLoaded<TService, TProperty>
		where TService : IDisposable
	{
		private readonly LoaderParams<TService> loaderParams;

		private readonly Action<TProperty> setValue;

		public ItemLoaded(
			LoaderParams<TService> loaderParams,
			INotifyPropertyChanged instance,
			MemberInfo member)
		{
			this.loaderParams = loaderParams;

			if (member == null)
			{
				return;
			}

			setValue = value => member.SetValue(instance, value);
		}

		public IItemPropertyFilled<TProperty> Fill<T>(Func<TService, T> func)
		{
			return new ItemFilled<TService, T, TProperty>(loaderParams, func, setValue);
		}
	}
}