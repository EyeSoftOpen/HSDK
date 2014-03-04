namespace EyeSoft.DynamicProxy
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

	public static class PropertySetActionExtensions
	{
		private static readonly IDictionary<Type, Field<PropertyChangedEventHandler>> fieldDictionary =
			new Dictionary<Type, Field<PropertyChangedEventHandler>>();

		public static void RaisePropertyChanged(this PropertySetInvocation propertyInvocation)
		{
			Action<PropertyChangedEventHandler> action =
				handler =>
					handler
						.Invoke(propertyInvocation.Instance, new PropertyChangedEventArgs(propertyInvocation.PropertyName));

			fieldDictionary
				.Key(propertyInvocation.TargetType)
				.CreateIfEmpty(() => propertyInvocation.TargetType.GetField<PropertyChangedEventHandler>())
				.Value(propertyInvocation.Instance)
				.Extend().OnNotDefault(action);
		}
	}
}