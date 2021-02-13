namespace EyeSoft.Accounting.Italian.Istat
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
    using EyeSoft.Extensions;
    using Internals.Serialization;

    public class CityRepository
	{
		private static ReadOnlyCollection<Town> townCollection;

		private static ReadOnlyCollection<City> cityCollection;

		private static IEnumerable<Town> TownCollection => AccountingSerializer.Collection<TownSerializable, Town>(ref townCollection, Convert);

        private static IEnumerable<City> CityCollection => AccountingSerializer.Collection<CitySerializable, City>(ref cityCollection, Convert);

        public IEnumerable<Town> AllTown()
		{
			return TownCollection;
		}

		public IEnumerable<Town> TownsContains(string partialName)
		{
			return AllTown().Where(town => town.Name.ContainsInvariant(partialName));
		}

		public IEnumerable<City> AllCity()
		{
			return CityCollection;
		}

		public IEnumerable<City> CitiesContains(string partialName)
		{
			return AllCity().Where(city => city.Name.Contains(partialName));
		}

		public City CityByName(string cityName)
		{
			var cityByName = AllCity().Single(city => city.Name.IgnoreCaseEquals(cityName));

			return cityByName;
		}

		public IEnumerable<Town> TownsInCity(string cityName)
		{
			var city = CityByName(cityName);

			var towns = AllTown().Where(town => town.City.IgnoreCaseEquals(city.Province));

			return towns;
		}

		public IEnumerable<Town> TownsInCity(string cityName, string townName)
		{
			var towns = TownsInCity(cityName).Where(town => town.Name.IgnoreCaseEquals(townName));

			return towns;
		}

		public IEnumerable<Town> TownsByName(string townName)
		{
			var towns = AllTown().Where(town => town.Name.IgnoreCaseEquals(townName));

			return towns;
		}

		private static Town Convert(TownSerializable x)
		{
			return new Town(x.Istat, x.Name, x.City, x.Region, x.Prefix, x.Zip, x.Area, x.Population);
		}

		private static City Convert(CitySerializable x)
		{
			return new City(x.Name, x.Population, x.AreaPercentage, x.AreaTotal, x.Density, x.Towns, x.Province);
		}
	}
}