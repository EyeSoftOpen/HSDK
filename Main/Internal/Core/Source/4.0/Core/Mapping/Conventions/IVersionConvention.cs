namespace EyeSoft.Mapping.Conventions
{
	public interface IVersionConvention
	{
		bool CanBeTheVersion(MemberInfoMetadata memberInfoMetadata);
	}
}