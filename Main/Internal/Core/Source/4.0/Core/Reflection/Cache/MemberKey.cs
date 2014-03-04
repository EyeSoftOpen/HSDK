namespace EyeSoft.Reflection
{
	using System;

	internal class MemberKey
	{
		public MemberKey(Type type, string memberName)
		{
			Type = type;
			MemberName = memberName;
		}

		public Type Type { get; private set; }

		public string MemberName { get; private set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (MemberKey)obj;
			return Type == other.Type && MemberName.Equals(other.MemberName);
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}", Type, MemberName);
		}
	}
}