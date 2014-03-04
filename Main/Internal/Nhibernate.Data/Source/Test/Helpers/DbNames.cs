namespace EyeSoft.Data.Nhibernate.Test.Helpers
{
	internal static class DbNames
	{
		public const string Factory = Prefix + "Factory";

		public const string Mapping = Prefix + "Mapping";

		public const string UnitOfWork = Prefix + "UnitOfWork";

		public const string Cache = Prefix + "Cache";

		private const string Prefix = "Nhibernate.";
	}
}