namespace EyeSoft.Core.Mapping
{
    using System.Collections.Generic;

    public interface IDomainMapper
	{
		IDomainMapper Register<T>()
			where T : class;

		IDomainMapper Register(MappedType mappedType);

		IEnumerable<MappedType> MappedTypes();
	}
}