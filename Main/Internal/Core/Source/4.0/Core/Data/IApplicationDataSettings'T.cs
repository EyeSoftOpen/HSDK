namespace EyeSoft.Core.Data
{
    using System;

    public interface IApplicationDataSettings<T>
	{
		void Save(T value);

		T Load(Func<T> defaultValue = null);
	}
}