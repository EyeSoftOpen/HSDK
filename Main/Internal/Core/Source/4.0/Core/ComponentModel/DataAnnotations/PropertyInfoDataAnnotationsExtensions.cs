namespace EyeSoft.Core.ComponentModel.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using Extensions;
    using Mapping;
    using Reflection;

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
                var message =
                    $"Member '{memberMetadata.Name}' of type '{memberMetadata.Type}' declared in '{memberMetadata.DeclaringType}' cannot " +
                    "have Required attribute because it is a nullable type.";

                throw new ArgumentException(message);
            }

            return isRequired;
        }
    }
}