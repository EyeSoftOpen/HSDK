namespace EyeSoft.Mapping
{
    using System.Reflection;

    using EyeSoft.ComponentModel.DataAnnotations;

    public class ReferenceMemberInfoMetadata
        : MemberInfoMetadata
    {
        internal ReferenceMemberInfoMetadata(MemberInfo memberInfo)
            : base(memberInfo)
        {
            Required = memberInfo.IsRequired();
        }

        public bool Required { get; private set; }
    }
}