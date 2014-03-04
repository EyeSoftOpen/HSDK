namespace EyeSoft.Mapping.Conventions
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

	public class KeyConvention
		: IKeyConvention
	{
		public bool CanBeTheKey(MemberInfoMetadata memberInfoMetadata)
		{
			var isAcceptedType =
				memberInfoMetadata
					.Type.IsOneOf(typeof(Guid), typeof(int));

			if (!isAcceptedType)
			{
				return false;
			}

			return
				memberInfoMetadata.Name == "Id" ||
				memberInfoMetadata.GetAttribute<KeyAttribute>().IsNotNull();
		}
	}
}