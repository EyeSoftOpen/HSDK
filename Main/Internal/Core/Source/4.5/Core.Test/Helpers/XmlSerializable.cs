namespace EyeSoft.Core.Test.Helpers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Title}")]
	public class XmlSerializable
	{
		public string Title { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (XmlSerializable)obj;
			return Comparer<string>.Default.Compare(Title, other.Title) == 0;
		}

		public override int GetHashCode()
		{
			return Title != null ? Title.GetHashCode() : base.GetHashCode();
		}
	}
}