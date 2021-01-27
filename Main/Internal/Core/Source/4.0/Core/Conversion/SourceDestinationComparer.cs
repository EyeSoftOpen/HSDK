namespace EyeSoft.Core
{
    using System.Collections.Generic;

    internal class SourceDestinationComparer
		: IEqualityComparer<SourceDestination>
	{
		public bool Equals(SourceDestination x, SourceDestination y)
		{
			return
				x.sourceType == y.sourceType &&
				x.destinationType == y.destinationType;
		}

		public int GetHashCode(SourceDestination obj)
		{
			return
				(obj.sourceType.GetHashCode() * 31) + obj.destinationType.GetHashCode();
		}
	}
}