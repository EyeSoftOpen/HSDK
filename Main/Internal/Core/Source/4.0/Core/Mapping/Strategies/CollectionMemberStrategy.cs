namespace EyeSoft.Mapping.Strategies
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	public class CollectionMemberStrategy : IMemberStrategy
	{
		private static readonly HashSet<Type> notAllowedCollectionTypeDictionary =
			new HashSet<Type>
			{
				typeof(ReadOnlyCollection<>),
				typeof(ReadOnlyObservableCollection<>)
			};

		public bool HasToMap(MemberInfoMetadata memberInfoMetadata)
		{
			if (new PrimitiveMemberStrategy().HasToMap(memberInfoMetadata))
			{
				return false;
			}

			var memberType = memberInfoMetadata.Type;

			if (memberType.IsOneOf(notAllowedCollectionTypeDictionary))
			{
				return false;
			}

			if (memberType.IsEnumerable())
			{
				var genericArguments = memberType.GetGenericArguments();

				if (genericArguments.Count() > 1)
				{
					return false;
				}

				var genericArgument = genericArguments.Single();

				return !genericArgument.IsPrimitiveType();
			}

			return false;
		}
	}
}