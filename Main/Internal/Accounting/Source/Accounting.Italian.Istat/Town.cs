namespace EyeSoft.Accounting.Italian.Istat
{
	public class Town
	{
		internal Town(string istat, string name, string city, string region, string prefix, string zip, string area, int population)
		{
			Istat = istat;
			Name = name;
			City = city;
			Region = region;
			Prefix = prefix;
			Zip = zip;
			Area = area;
			Population = population;
		}

		public string Istat { get; }

		public string Name { get; }

		public string City { get; }

		public string Region { get; }

		public string Prefix { get; }

		public string Zip { get; }

		public string Area { get; }

		public int Population { get; }

		public override string ToString()
		{
			return Name;
		}
	}
}