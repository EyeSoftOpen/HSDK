namespace EyeSoft.Mapping.Conventions
{
	public interface IKeyConvention
	{
		bool CanBeTheKey(MemberInfoMetadata memberInfoMetadata);
	}
}