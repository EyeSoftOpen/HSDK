namespace EyeSoft.Mapping
{
    using System;
    using System.Collections.Generic;

    public class MappedType
		: IEqualityComparer<MappedType>
	{
		internal MappedType(
			Type sourceType,
			Type mappedType,
			KeyMemberInfoMetadata key,
			MemberInfoMetadata version,
			IEnumerable<PrimitiveMemberInfoMetadata> primitives,
			IEnumerable<ReferenceMemberInfoMetadata> references,
			IEnumerable<CollectionMemberInfoMetadata> collections)
		{
			Source = sourceType;
			Mapped = mappedType;
			Key = key;
			Version = version;
			Primitives = primitives;
			References = references;
			Collections = collections;
		}

		public Type Source { get; }

		public Type Mapped { get; }

		public KeyMemberInfoMetadata Key { get; }

		public MemberInfoMetadata Version { get; }

		public IEnumerable<PrimitiveMemberInfoMetadata> Primitives { get; }

		public IEnumerable<ReferenceMemberInfoMetadata> References { get; }

		public IEnumerable<CollectionMemberInfoMetadata> Collections { get; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			return Equals(this, (MappedType)obj);
		}

		public override int GetHashCode()
		{
			return GetHashCode(this);
		}

		public bool Equals(MappedType x, MappedType y)
		{
			return x.Source == y.Source;
		}

		public int GetHashCode(MappedType obj)
		{
			return obj.Source.GetHashCode();
		}
	}
}