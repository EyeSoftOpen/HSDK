namespace EyeSoft.Accounting.Italian.Istat
{
	public class City
	{
		public City(
			string name,
			int population,
			double areaPercentage,
			int areaTotal,
			double density,
			short towns,
			string province)
		{
			Name = name;
			Population = population;
			AreaPercentage = areaPercentage;
			AreaTotal = areaTotal;
			Density = density;
			Towns = towns;
			Province = province;
		}

		public string Name { get; }

		public int Population { get; }

		public double AreaPercentage { get; }

		public int AreaTotal { get; }

		public double Density { get; }

		public short Towns { get; }

		public string Province { get; }
	}
}
