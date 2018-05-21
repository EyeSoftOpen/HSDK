namespace EyeSoft.Mapping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    using EyeSoft.ComponentModel.DataAnnotations;
    using EyeSoft.Extensions;
    using EyeSoft.Reflection;

    public class PrimitiveMemberInfoMetadata : MemberInfoMetadata
    {
        internal PrimitiveMemberInfoMetadata(MemberInfo memberInfo)
            : base(memberInfo)
        {
            Required = memberInfo.IsRequired();

            SupportLength = Type.IsOfType<string>();

            if (!SupportLength)
            {
                return;
            }

            var stringLengthAttribute = memberInfo.GetAttribute<StringLengthAttribute>();

            if (!stringLengthAttribute.IsNotNull())
            {
                return;
            }

            Length = stringLengthAttribute.MaximumLength;
        }

        public bool Required { get; private set; }

        public bool SupportLength { get; private set; }

        public int? Length { get; private set; }
    }
}