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

		public string Name { get; private set; }

		public int Population { get; private set; }

		public double AreaPercentage { get; private set; }

		public int AreaTotal { get; private set; }

		public double Density { get; private set; }

		public short Towns { get; private set; }

		public string Province { get; private set; }
	}
}
