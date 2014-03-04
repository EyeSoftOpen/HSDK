namespace EyeSoft.Mapping.Strategies
{
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

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