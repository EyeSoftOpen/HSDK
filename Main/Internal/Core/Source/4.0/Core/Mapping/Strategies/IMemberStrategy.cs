namespace EyeSoft.Core.Mapping.Strategies
{
	public interface IMemberStrategy
	{
		bool HasToMap(MemberInfoMetadata memberInfoMetadata);
	}
}