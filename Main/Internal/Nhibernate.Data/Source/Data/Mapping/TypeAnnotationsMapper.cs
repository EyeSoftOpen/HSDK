namespace EyeSoft.Data.Nhibernate.Mapping
{
	using System;
	using System.Linq;

	using EyeSoft.Data.Nhibernate.Mapping.Mappers;
	using EyeSoft.Extensions;
	using EyeSoft.Mapping;
	using EyeSoft.Mapping.Data;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Type;

	internal class TypeAnnotationsMapper
	{
		public IConformistHoldersProvider Map<T>(MappedType mappedType, IClassMapper<T> classMapper)
			where T : class
		{
			MapKey(mappedType.Key, classMapper);

			MapVersion(mappedType.Version, classMapper);

			MapPrimitives(mappedType, classMapper);

			MapReferences(mappedType, classMapper);

			MapCollections(mappedType, classMapper);

			return (IConformistHoldersProvider)classMapper;
		}

		private void MapKey<T>(MemberInfoMetadata key, IClassAttributesMapper<T> classMapper)
			where T : class
		{
			object unsavedKeyValue = 0;

			if (key.Type == typeof(Guid))
			{
				unsavedKeyValue = Guid.Empty;
			}

			classMapper.Id(
				key.Name,
				m =>
					{
						AccessorHelper.SetAccessor(key, m);
						m.Generator(GetGenerator(key.Type));
						m.UnsavedValue(unsavedKeyValue);
					});
		}

		private void MapVersion<T>(MemberInfoMetadata version, IClassMapper<T> classMapper)
			where T : class
		{
			if (version.IsNull())
			{
				return;
			}

			classMapper
				.Version(
					version.Name,
					m =>
						{
							m.Type(new TimestampType());
							m.Generated(VersionGeneration.Never);
							AccessorHelper.SetAccessor(version, m);
						});
		}

		private void MapPrimitives<T>(MappedType mappedType, IMinimalPlainPropertyContainerMapper<T> classMapper)
			where T : class
		{
			if (!mappedType.Primitives.Any())
			{
				return;
			}

			new PrimitiveMapper<T>(classMapper, mappedType.Primitives).Map();
		}

		private void MapReferences<T>(MappedType mappedType, IPropertyContainerMapper<T> classMapper)
			where T : class
		{
			if (!mappedType.References.Any())
			{
				return;
			}

			new ReferenceMapper<T>(classMapper, mappedType.References).Map();
		}

		private void MapCollections<T>(MappedType mappedType, IPropertyContainerMapper<T> classMapper)
			where T : class
		{
			if (!mappedType.Collections.Any())
			{
				return;
			}

			new CollectionMapper<T>(classMapper, mappedType.Collections).Map();
		}

		private IGeneratorDef GetGenerator(Type type)
		{
			if (type == typeof(Guid))
			{
				return Generators.Assigned;
			}

			if (type == typeof(int))
			{
				return Generators.Identity;
			}

			new ArgumentException("Only Guid or int are supported as types of key the key.")
				.Throw();

			return null;
		}
	}
}