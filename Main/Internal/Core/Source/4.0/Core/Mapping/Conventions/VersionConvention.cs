namespace EyeSoft.Core.Mapping.Conventions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Extensions;
    using Reflection;

    public class VersionConvention
		: IVersionConvention
	{
		public bool CanBeTheVersion(MemberInfoMetadata memberInfoMetadata)
		{
			var isAcceptedType =
				memberInfoMetadata
					.Type.IsOneOf(typeof(DateTime), typeof(byte[]));

			if (!isAcceptedType)
			{
				return false;
			}

			return
				memberInfoMetadata.Name == "Version" ||
				memberInfoMetadata.GetAttribute<TimestampAttribute>().IsNotNull();
		}
	}
}