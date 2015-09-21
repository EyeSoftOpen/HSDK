using System;

namespace EyeSoft.Architecture.Prototype.Accounting.Commanding
{
	public abstract class IdentityCommand : Command
	{
		public Guid Id { get; set; }
	}
}