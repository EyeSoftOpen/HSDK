namespace EyeSoft.Core.Mapping.Conventions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Extensions;
    using Reflection;

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