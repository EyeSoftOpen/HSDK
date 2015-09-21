using System;

namespace EyeSoft.Architecture.Prototype.Windows.Model.Base
{
	public interface IResolver
	{
		T Resolve<T>(Type type);
		T Resolve<T>();
	}
}