namespace EyeSoft.Mapping.Strategies
{
	public interface IMemberStrategy
	{
		bool HasToMap(MemberInfoMetadata memberInfoMetadata);
	}
}