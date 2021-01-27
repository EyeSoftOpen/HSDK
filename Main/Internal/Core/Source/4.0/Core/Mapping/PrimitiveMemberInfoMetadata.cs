namespace EyeSoft.Core.Mapping
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using ComponentModel.DataAnnotations;
    using Extensions;
    using Reflection;

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