namespace EyeSoft.Data.Nhibernate.Mapping.Mappers
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Mapping;

	using NHibernate.Mapping.ByCode;

	internal class ReferenceMapper<T>
		where T : class
	{
		private readonly IPropertyContainerMapper<T> annotationsMap;
		private readonly IEnumerable<ReferenceMemberInfoMetadata> referenceProperties;

		public ReferenceMapper(IPropertyContainerMapper<T> annotationsMap, IEnumerable<ReferenceMemberInfoMetadata> referenceProperties)
		{
			this.annotationsMap = annotationsMap;
			this.referenceProperties = referenceProperties;
		}

		public void Map()
		{
			var mapCollectionMethod =
				GetType()
					.GetMethod("MapReference", BindingFlags.NonPublic | BindingFlags.Instance);

			referenceProperties
				.ForEach(x => mapCollectionMethod.MakeGenericMethod(GetType(x.Type)).Invoke(this, new object[] { x }));
		}

		private Type GetType(Type type)
		{
			return
				type;
		}

		private void MapReference<TProperty>(ReferenceMemberInfoMetadata member)
			where TProperty : class
		{
			annotationsMap
				.ManyToOne<TProperty>(
					member.Name,
					m =>
						{
							AccessorHelper.SetAccessor(member, m);
							m.Cascade(Cascade.Merge);
							m.Column(typeof(TProperty).Name + "Id");
							m.NotNullable(member.Required);
						});
		}
	}
}