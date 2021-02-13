namespace EyeSoft.Mapping
{
    using System;
    using System.Collections.Generic;

    public class DomainMapper : IDomainMapper
	{
		private readonly ISet<MappedType> mappedTypes = new HashSet<MappedType>();

		public IDomainMapper Register<T>() where T : class
		{
			return Register(TypeMapperFactory.Create().Map(typeof(T)));
		}

		public IDomainMapper Register(MappedType mappedType)
		{
			if (mappedTypes.Contains(mappedType))
			{
				throw new ArgumentException($"Cannot map '{mappedType.Source.FullName}' more than once.");
			}
			mappedTypes.Add(mappedType);
			return this;
		}

		IEnumerable<MappedType> IDomainMapper.MappedTypes()
		{
			return mappedTypes;
		}
	}
}