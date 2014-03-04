namespace EyeSoft.Data.Nhibernate
{
	using EyeSoft.Data.Common;

	using NHibernate.Cfg;

	public interface IConfigurationApplier
	{
		SchemaAction SchemaAction { get; }

		void Apply(Configuration configuration, IDatabaseProvider databaseProvider);
	}
}