namespace Commanding
{
	using System;

	public abstract class IdentityCommand : Command
	{
		public Guid Id { get; set; }
	}
}