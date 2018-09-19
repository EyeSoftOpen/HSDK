namespace EyeSoft.Conventions
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

	public class MetadataConvention
		: TypeConvention<object, object>
	{
		protected override Type TryMapTo(Type type)
		{
			var metadataAttribute = type.GetAttribute<MetadataTypeAttribute>();

			if (metadataAttribute.IsNotNull())
			{
				return metadataAttribute.MetadataClassType;
			}

			var metadataType =
				type.Assembly
				.GetType("{TypeFullName}Metadata".NamedFormat(type.FullName));

			return
				metadataType ?? type;
		}
	}
}