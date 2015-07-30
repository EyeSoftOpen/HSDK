namespace EyeSoft.Nuget.Publisher.Shell
{
	public class PackageUpdate
	{
		public PackageUpdate(string id, string previous, string current)
		{
			Id = id;
			Previous = previous;
			Current = current;
		}

		public string Id { get; private set; }

		public string Previous { get; private set; }

		public string Current { get; private set; }

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", Id, Previous, Current);
		}
	}
}