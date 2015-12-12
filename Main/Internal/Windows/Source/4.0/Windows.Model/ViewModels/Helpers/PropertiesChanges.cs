namespace EyeSoft.Windows.Model
{
	using System;
	using System.Collections.Generic;

	internal class PropertyChanges
	{
		private readonly IList<PropertyChangeMetadata> changeActions = new List<PropertyChangeMetadata>();

		public void AddChangeActionOn<TProperty>(Action<TProperty> action, int times)
		{
			changeActions.Add(new PropertyChangeMetadata(times, action));
		}

		public void PropertySetted(object propertyValue)
		{
			foreach (var propertyChangeAction in changeActions)
			{
				if (propertyChangeAction.HasToExecute)
				{
					propertyChangeAction.Execute(propertyValue);
				}

				propertyChangeAction.Changed();
			}
		}
	}
}