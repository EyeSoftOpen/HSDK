namespace EyeSoft.Testing.Domain.Helpers.Domain4
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Domain;

	public class Post : VersionedEntity
	{
		protected internal Post()
		{
		}

		[Required]
		public virtual string Title { get; protected internal set; }

		[Required]
		public virtual DateTime Created { get; protected internal set; }

		[Required]
		public virtual Blog Blog { get; protected internal set; }

		[Required]
		public virtual byte[] Data { get; protected internal set; }

		public virtual DateTime? Modified { get; protected internal set; }
	}
}