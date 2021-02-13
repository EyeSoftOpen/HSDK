namespace EyeSoft.Mapping.Strategies
{
    using Extensions;

    public class ReferenceMemberStrategy : IMemberStrategy
	{
		public bool HasToMap(MemberInfoMetadata memberInfoMetadata)
		{
			return
				!new PrimitiveMemberStrategy().HasToMap(memberInfoMetadata) &&
				!memberInfoMetadata.Type.IsEnumerable();
		}
	}
}