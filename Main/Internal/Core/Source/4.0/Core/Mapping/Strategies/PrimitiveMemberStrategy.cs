namespace EyeSoft.Mapping.Strategies
{
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