namespace EyeSoft.Mapping
{
	using System.Collections.Generic;
	using System.Linq;

	public class DomainMapper
		: IDomainMapper
	{
		private readonly ISet<MappedType> mappedTypes =
			new HashSet<MappedType>();

		public IDomainMapper Register<T>()
			where T : class
		{
			return Register(TypeMapperFactory.Create().Map(typeof(T)));
		}

		public IDomainMapper Register(MappedType mappedType)
		{
			Enforce.Argument(() => mappedType);

			Ensure
				.That(mappedTypes.AsEnumerable())
				.Is.Not.Containing(mappedType);

			mappedTypes.Add(mappedType);
			return this;
		}

		IEnumerable<MappedType> IDomainMapper.MappedTypes()
		{
			return mappedTypes;
		}
	}
}