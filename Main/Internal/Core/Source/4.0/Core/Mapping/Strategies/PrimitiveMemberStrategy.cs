namespace EyeSoft.Mapping.Strategies
{
    using Extensions;

    public class PrimitiveMemberStrategy : IMemberStrategy
	{
		public bool HasToMap(MemberInfoMetadata memberInfoMetadata)
		{
			return
				memberInfoMetadata.Type.IsPrimitiveType() ||
				memberInfoMetadata.Type == typeof(byte[]) ||
				memberInfoMetadata.Type.IsNullable();
		}
	}
}