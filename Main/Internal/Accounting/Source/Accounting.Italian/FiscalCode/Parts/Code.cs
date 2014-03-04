namespace EyeSoft.Accounting.Italian
{
	public abstract class Code
	{
		private string value;

		private string Value
		{
			get { return value ?? (value = GetCode().ToUpper()); }
		}

		public override string ToString()
		{
			return Value;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (NameCode)obj;
			return Value.Equals(other.Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		protected internal abstract string GetCode();
	}
}