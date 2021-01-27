namespace EyeSoft.Core.Mapping
{
    using System.Reflection;

    public class KeyMemberInfoMetadata
		: MemberInfoMetadata
	{
		internal KeyMemberInfoMetadata(MemberInfo propertyInfo)
			: base(propertyInfo)
		{
		}
	}
}