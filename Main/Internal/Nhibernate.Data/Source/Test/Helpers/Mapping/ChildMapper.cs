namespace EyeSoft.Data.Nhibernate.Test.Helpers.Mapping
{
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using NHibernate.Mapping.ByCode.Conformist;

	public class ChildMapper : ClassMapping<Child>
	{
		public ChildMapper()
		{
			Id(x => x.Id);

			Property(x => x.Street, map => map.NotNullable(true));

			ManyToOne(
				x => x.School,
				map => map.NotNullable(true));
		}
	}
}