namespace EyeSoft.Mapping
{
	using System.Reflection;

	using EyeSoft.Extensions;

	internal static class AccessorHelper
	{
		public static Accessors Get(MemberInfo member)
		{
			var memberInfoMetadata = member as MemberInfoMetadata;

			if (memberInfoMetadata.IsNotNull())
			{
				return memberInfoMetadata.Accessor;
			}

			if (member.MemberType == MemberTypes.Field)
			{
				return Accessors.Field;
			}

			if (member.MemberType == MemberTypes.Property)
			{
				var property = member as PropertyInfo;

				if (property.CanRead && property.CanWrite)
				{
					return Accessors.Property;
				}

				if (property.CanRead && !property.CanWrite)
				{
					return Accessors.PropertyNoSetter;
				}

				return Accessors.Property;
			}

			return Accessors.Property;
		}
	}
}