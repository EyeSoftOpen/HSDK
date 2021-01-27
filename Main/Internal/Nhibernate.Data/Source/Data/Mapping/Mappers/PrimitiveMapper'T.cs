namespace EyeSoft.Data.Nhibernate.Mapping.Mappers
{
	using System.Collections.Generic;
    using Core.Collections.Generic;
    using Core.Mapping;
    using NHibernate.Mapping.ByCode;

	internal class PrimitiveMapper<T>
		where T : class
	{
		private readonly IMinimalPlainPropertyContainerMapper<T> annotationsMap;

		private readonly IEnumerable<PrimitiveMemberInfoMetadata> properties;

		public PrimitiveMapper(
			IMinimalPlainPropertyContainerMapper<T> annotationsMap,
			IEnumerable<PrimitiveMemberInfoMetadata> properties)
		{
			this.annotationsMap = annotationsMap;
			this.properties = properties;
		}

		public void Map()
		{
			properties
				.ForEach(MapPrimitiveProperty);
		}

		private void MapPrimitiveProperty(PrimitiveMemberInfoMetadata member)
		{
			if (member.SupportLength && member.Length.HasValue)
			{
				annotationsMap
					.Property(
						member.Name,
						m =>
							{
								AccessorHelper.SetAccessor(member, m);
								m.NotNullable(member.Required);
								m.Length(member.Length.Value);
							});
				return;
			}

			annotationsMap
				.Property(
					member.Name,
					m =>
						{
							m.NotNullable(member.Required);
							AccessorHelper.SetAccessor(member, m);
						});
		}
	}
}