namespace EyeSoft.Core.Mapping.Conventions
{
	public interface IVersionConvention
	{
		bool CanBeTheVersion(MemberInfoMetadata memberInfoMetadata);
	}
}