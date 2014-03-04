namespace EyeSoft.Mapping
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using EyeSoft.ComponentModel.DataAnnotations.Schema;
	using EyeSoft.Extensions;
	using EyeSoft.Mapping.Strategies;
	using EyeSoft.Reflection;

	public class TypeMapper
	{
		private readonly TypeMapperConventions typeMapperConventions;

		private readonly IMemberStrategy primitiveMemberStrategy = new PrimitiveMemberStrategy();
		private readonly IMemberStrategy referenceMemberStrategy = new ReferenceMemberStrategy();
		private readonly IMemberStrategy collectionMemberStrategy = new CollectionMemberStrategy();

		internal TypeMapper(TypeMapperConventions typeMapperConventions)
		{
			this.typeMapperConventions = typeMapperConventions;
		}

		public MappedType Map<T>()
			where T : class
		{
			return
				Map(typeof(T));
		}

		public MappedType Map(Type sourceType)
		{
			var memberInfoValidator = new MemberInfoValidator();

			var mappedType =
				(typeMapperConventions.TypeConvention == null) ?
					sourceType :
					typeMapperConventions.TypeConvention.MapTo(sourceType);

			var members =
				mappedType
					.GetAnyInstanceFieldsOrProperties()
					.Where(memberInfoValidator.IsValidFieldOrProperty)
					.Where(member => member.GetAttribute<NotMappedAttribute>().IsNull())
					.Select(member => new MemberInfoMetadata(member, mappedType))
					.ToList();

			var key =
				(typeMapperConventions.KeyConvention == null) ?
					null :
					SearchSpecialMemberByConventionAndRemoveFromMembers(
						members,
						memberInfo => typeMapperConventions.KeyConvention.CanBeTheKey(memberInfo));

			var version =
				(typeMapperConventions.VersionConvention == null) ?
					null :
					SearchSpecialMemberByConventionAndRemoveFromMembers(
							members,
							memberInfo => typeMapperConventions.VersionConvention.CanBeTheVersion(memberInfo));

			var primitives =
				members
					.Where(primitiveMemberStrategy.HasToMap)
					.Where(member => CustomStrategy(typeMapperConventions.PrimitiveStrategy, member))
					.Select(CreatePrimitivePropertyInfoMetadata);

			var references =
				members
					.Where(referenceMemberStrategy.HasToMap)
					.Where(member => CustomStrategy(typeMapperConventions.ReferenceStrategy, member))
					.Select(CreateReferencePropertyInfoMetadata);

			var collections =
				members
					.Where(collectionMemberStrategy.HasToMap)
					.Where(member => CustomStrategy(typeMapperConventions.CollectionStrategy, member))
					.Select(CreateCollectionPropertyInfoMetadata)
					.ToList();

			RemoveFieldMemberOnCollectionIfPropertyExists(collections);

			var keyMemberInfoMetadata =
				(key == null) ?
					null :
					new KeyMemberInfoMetadata(key);

			return
				new MappedType(sourceType, mappedType, keyMemberInfoMetadata, version, primitives, references, collections);
		}

		private MemberInfoMetadata SearchSpecialMemberByConventionAndRemoveFromMembers(
			ICollection<MemberInfoMetadata> members,
			Func<MemberInfoMetadata, bool> respectConvention)
		{
			var specialMember =
				members
					.SingleOrDefault(respectConvention);

			if (specialMember != null)
			{
				members.Remove(specialMember);
			}

			return specialMember;
		}

		private bool CustomStrategy(IMemberStrategy memberStrategy, MemberInfoMetadata member)
		{
			return
				(memberStrategy == null) || memberStrategy.HasToMap(member);
		}

		private PrimitiveMemberInfoMetadata CreatePrimitivePropertyInfoMetadata(MemberInfoMetadata memberInfoMetadata)
		{
			return new PrimitiveMemberInfoMetadata(memberInfoMetadata);
		}

		private ReferenceMemberInfoMetadata CreateReferencePropertyInfoMetadata(MemberInfoMetadata memberInfoMetadata)
		{
			return new ReferenceMemberInfoMetadata(memberInfoMetadata);
		}

		private CollectionMemberInfoMetadata CreateCollectionPropertyInfoMetadata(MemberInfoMetadata memberInfoMetadata)
		{
			return new CollectionMemberInfoMetadata(memberInfoMetadata);
		}

		private void RemoveFieldMemberOnCollectionIfPropertyExists(ICollection<CollectionMemberInfoMetadata> collections)
		{
			var collectionsByType =
				collections
					.GroupBy(collection => collection.Type)
					.Where(group => @group.Count() > 1)
					.SelectMany(
						collectionByType =>
							collectionByType.Where(collection => collection.MemberType == MemberTypes.Field));

			foreach (var collection in collectionsByType)
			{
				collections.Remove(collection);
			}
		}
	}
}