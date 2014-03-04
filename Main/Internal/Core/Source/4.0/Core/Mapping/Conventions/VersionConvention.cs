namespace EyeSoft.Mapping.Conventions
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Extensions;
	using EyeSoft.Reflection;

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