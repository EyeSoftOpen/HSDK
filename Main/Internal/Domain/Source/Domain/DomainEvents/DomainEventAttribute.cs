namespace EyeSoft.Domain
{
	using System;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class DomainEventAttribute : Attribute
	{
	}
}