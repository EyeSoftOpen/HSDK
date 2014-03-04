namespace EyeSoft.Data.Common
{
	using System.Data.Common;

	public static class DbDataReaderExtensions
	{
		public static T Column<T>(this DbDataReader reader, string columnName)
		{
			return (T)reader[columnName];
		}
	}
}