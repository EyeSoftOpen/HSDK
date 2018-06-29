namespace EyeSoft.Data.Common
{
	using System;

	public static class DatabaseSchemaExtensions
	{
		public static string DropCreateSchema(this IDatabaseSchema databaseSchema)
		{
			return $"{databaseSchema.Drop()} {Environment.NewLine} {databaseSchema.Create()}";
		}
	}
}