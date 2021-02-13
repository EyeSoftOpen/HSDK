namespace EyeSoft.Data.SqlClient.Maintenance
{
	public class DbMaintenanceSettings
	{
		public DbMaintenanceSettings(string name, string path)
		{
			Name = name;
			Path = path;
		}

		public string Name { get; }

		public string Path { get; }
	}
}