namespace EyeSoft.Testing.Domain.Helpers.Domain5
{
	using EyeSoft.Domain;

	public abstract class AdministrativeDocument : Aggregate
	{
		public virtual string Customer { get; protected internal set; }
	}
}