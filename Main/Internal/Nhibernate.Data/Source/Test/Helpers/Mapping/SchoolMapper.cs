namespace EyeSoft.Data.Nhibernate.Test.Helpers.Mapping
{
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class SchoolMapper : ClassMapping<School>
	{
		public SchoolMapper()
		{
			Id(x => x.Id);

			Property(x => x.Name, map => map.NotNullable(true));

			Bag(
				x => x.ChildList,
				p =>
				{
					p.Lazy(CollectionLazy.Lazy);
					p.Inverse(true);
				},
				r => r.OneToMany());
		}
	}
}