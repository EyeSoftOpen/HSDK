namespace EyeSoft.Data.Common
{
	using System.Data;

	using EyeSoft.Extensions;

	public static class DbCommandExtensions
	{
		public static T ExecuteScalar<T>(this IDbCommand command)
		{
			return command.ExecuteScalar().Convert<T>();
		}
	}
}