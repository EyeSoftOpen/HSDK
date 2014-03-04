namespace EyeSoft.Mapping
{
	using System.Reflection;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

	internal class MemberInfoValidator
	{
		private const BindingFlags PropertyBindingFlags =
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		public bool IsValidFieldOrProperty(MemberInfo member)
		{
			if ((member.MemberType != MemberTypes.Field) && (member.MemberType != MemberTypes.Property))
			{
				return false;
			}

			if (member.IsDelegateField())
			{
				return false;
			}

			if (member.IsCompilerGenerated())
			{
				return false;
			}

			if (member.MemberType == MemberTypes.Property)
			{
				var propertyInfo = member as PropertyInfo;
				propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyInfo.Name, PropertyBindingFlags);
				return propertyInfo.CanWrite || propertyInfo.GetSetMethod().IsNotNull();
			}

			if (member.MemberType == MemberTypes.Field)
			{
				var fieldInfo = member as FieldInfo;
				return !fieldInfo.IsInitOnly;
			}

			return false;
		}
	}
}