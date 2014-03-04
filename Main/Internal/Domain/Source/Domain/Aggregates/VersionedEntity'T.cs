namespace EyeSoft.Domain
{
	using System;

	[Serializable]
	public abstract class VersionedEntity<T> : Entity<T> where T : IComparable<T>
	{
		public virtual DateTime Version
		{
			get; private set;
		}
	}
}