namespace EyeSoft.Core.Mapping.Conventions
{
	public interface IKeyConvention
	{
		bool CanBeTheKey(MemberInfoMetadata memberInfoMetadata);
	}
}