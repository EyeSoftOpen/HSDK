namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Reflection;

    public interface ICommandConvention
	{
		CommandMethods Get(Type viewModelType, PropertyInfo commandProperty);
	}
}