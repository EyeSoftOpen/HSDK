namespace EyeSoft.Core.Mapping
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

		public Type Source { get; private set; }

		public Type Mapped { get; private set; }

		public KeyMemberInfoMetadata Key { get; private set; }

		public MemberInfoMetadata Version { get; private set; }

		public IEnumerable<PrimitiveMemberInfoMetadata> Primitives { get; private set; }

		public IEnumerable<ReferenceMemberInfoMetadata> References { get; private set; }

		public IEnumerable<CollectionMemberInfoMetadata> Collections { get; private set; }

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