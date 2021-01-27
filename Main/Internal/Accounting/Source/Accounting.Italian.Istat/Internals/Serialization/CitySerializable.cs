namespace EyeSoft.Accounting.Italian.Istat.Internals.Serialization
{
	internal class CitySerializable
	{
		public string Name { get; set; }

		public int Population { get; set; }

		public double AreaPercentage { get; set; }

		public int AreaTotal { get; set; }

		public double Density { get; set; }

		public short Towns { get; set; }

		public string Province { get; set; }
	}
}
