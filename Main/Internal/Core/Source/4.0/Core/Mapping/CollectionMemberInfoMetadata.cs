namespace EyeSoft.Mapping
{
	using System.Linq;
	using System.Reflection;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

	public class CollectionMemberInfoMetadata
		: MemberInfoMetadata
	{
		internal CollectionMemberInfoMetadata(MemberInfo memberInfo)
			: base(memberInfo)
		{
			Inverse = IsInverse();
			Lazy = memberInfo.GetAttribute<NotLazyAttribute>().IsNull();
		}

		public bool Lazy { get; private set; }

		public bool Inverse { get; private set; }

		private bool IsInverse()
		{
			var collectionGenericType =
				Type.GetGenericArguments().Single();

			var members =
				collectionGenericType.GetAnyInstanceFieldsOrProperties();

			var collectionTypeContainsReferenceToThisType =
				members.Any(member => new MemberInfoMetadata(member).Type == member.DeclaringType);

			return collectionTypeContainsReferenceToThisType;
		}
	}
}