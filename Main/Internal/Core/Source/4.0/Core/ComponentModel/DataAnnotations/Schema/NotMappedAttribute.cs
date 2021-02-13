namespace EyeSoft.ComponentModel.DataAnnotations.Schema
{
    using System;

    // TODO: remove this class with .NET 4.5
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class NotMappedAttribute
		: Attribute
	{
	}
}