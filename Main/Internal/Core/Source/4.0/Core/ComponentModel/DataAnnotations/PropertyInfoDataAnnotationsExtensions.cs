namespace EyeSoft.ComponentModel.DataAnnotations
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Reflection;

	using EyeSoft.Extensions;
	using EyeSoft.Mapping;
	using EyeSoft.Reflection;

	public static class MemberInfoDataAnnotationsExtensions
	{
		public static bool IsRequired(this MemberInfo memberInfo)
		{
			var memberMetadata = new MemberInfoMetadata(memberInfo);

			var isRequired =
				memberInfo
					.GetAttribute<RequiredAttribute>()
					.IsNotNull();

			if (memberMetadata.Type.IsNullable() && isRequired)
			{
				const string MessageFormat =
					"Member '{MemberName}' of type '{Type}' declared in '{DeclaringType}' cannot " +
					"have Required attribute because it is a nullable type.";

				var message =
					MessageFormat
					.NamedFormat(
						memberMetadata.Name,
						memberMetadata.Type.FriendlyShortName(),
						memberMetadata.DeclaringType.FullName);

				new ArgumentException(message)
					.Throw();
			}

			return
				isRequired;
		}
	}
}