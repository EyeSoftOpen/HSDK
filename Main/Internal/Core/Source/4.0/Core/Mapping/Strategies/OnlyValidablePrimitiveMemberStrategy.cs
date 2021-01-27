namespace EyeSoft.Core.Mapping.Strategies
{
    using System.ComponentModel.DataAnnotations;
    using Extensions;
    using Reflection;

    public class OnlyValidablePrimitiveMemberStrategy : IMemberStrategy
	{
		public bool HasToMap(MemberInfoMetadata memberInfoMetadata)
		{
			if (memberInfoMetadata.Type == typeof(string))
			{
				return
					memberInfoMetadata
						.GetAttribute<StringLengthAttribute>()
						.IsNotNull();
			}

			return false;
		}
	}
}