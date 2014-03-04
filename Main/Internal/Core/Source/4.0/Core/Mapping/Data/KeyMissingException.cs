namespace EyeSoft.Mapping.Data
{
	using System;

	[Serializable]
	public class KeyMissingException
		: MappingException
	{
		public KeyMissingException(Type entityType)
			: base("The entity '{0}' has no keys defined.".NamedFormat(entityType.Name))
		{
			EntityType = entityType;
		}

		public KeyMissingException(string message, Type entityType)
			: base(message)
		{
			EntityType = entityType;
		}

		public Type EntityType { get; private set; }
	}
}