namespace EyeSoft.Data.Nhibernate.Mapping.Mappers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using Collections.Generic;
    using EyeSoft.Mapping;
    using NHibernate.Mapping.ByCode;
    using Reflection;

    internal class CollectionMapper<T>
		where T : class
	{
		private readonly IEnumerable<CollectionMemberInfoMetadata> collectionProperties;
		private readonly IPropertyContainerMapper<T> annotationsMap;

		public CollectionMapper(
			IPropertyContainerMapper<T> annotationsMap,
			IEnumerable<CollectionMemberInfoMetadata> collectionProperties)
		{
			this.annotationsMap = annotationsMap;
			this.collectionProperties = collectionProperties;
		}

		public void Map()
		{
			collectionProperties.ForEach(InvokeMapCollection);
		}

		private void InvokeMapCollection(CollectionMemberInfoMetadata property)
		{
			this
				.Invoke("MapCollection", new[] { GetCollectionGenericType(property.Type) }, property);
		}

		private Type GetCollectionGenericType(Type type)
		{
			return
				type
					.GetGenericArguments()
					.Single();
		}

		private void MapCollection<TProperty>(CollectionMemberInfoMetadata member)
		{
			annotationsMap
				.Bag<TProperty>(
					member.Name,
					m =>
						{
							m.Inverse(member.Inverse);
							m.Lazy(GetLazy(member));
							AccessorHelper.SetAccessor(member, m);

							m.Cascade(Cascade.Merge | Cascade.DeleteOrphans);
							m.Key(k => k.Column(typeof(T).Name + "Id"));
						},
						r => r.OneToMany());
		}

		private CollectionLazy GetLazy(CollectionMemberInfoMetadata collection)
		{
			if (collection.Lazy)
			{
				return CollectionLazy.Lazy;
			}

			return CollectionLazy.NoLazy;
		}
	}
}