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

		public string Istat { get; private set; }

		public string Name { get; }

		public string City { get; private set; }

		public string Region { get; private set; }

		public string Prefix { get; private set; }

		public string Zip { get; private set; }

		public string Area { get; private set; }

		public int Population { get; private set; }

		public override string ToString()
		{
			return Name;
		}
	}
}