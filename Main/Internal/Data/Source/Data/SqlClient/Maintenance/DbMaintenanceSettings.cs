namespace EyeSoft.Data.SqlClient.Maintenance
{
	public class DbMaintenanceSettings
	{
		public DbMaintenanceSettings(string name, string path)
		{
			Name = name;
			Path = path;
		}

		public string Name { get; private set; }

		public string Path { get; private set; }
	}
}