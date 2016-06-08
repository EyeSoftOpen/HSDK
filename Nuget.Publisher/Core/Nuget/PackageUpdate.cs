namespace EyeSoft.Nuget.Publisher.Core.Nuget
{
	public class PackageUpdate
	{
		public PackageUpdate(string id, string previous, string current)
		{
			Id = id;
			Previous = previous;
			Current = current;
		}

		public string Id { get; }

		public string Previous { get; }

		public string Current { get; }

		public override string ToString()
		{
			return $"{Id} {Previous} {Current}";
		}
	}
}