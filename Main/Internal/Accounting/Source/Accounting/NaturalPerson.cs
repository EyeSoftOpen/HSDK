namespace EyeSoft.Accounting
{
	using System;
    using EyeSoft.Extensions;

    public class NaturalPerson
	{
		public NaturalPerson(string firstName, string lastName, DateTime birthDate, Sex sex)
		{
			FirstName = firstName;
			LastName = lastName;
			BirthDate = birthDate;
			Sex = sex;
		}

		public string FirstName { get; }

		public string LastName { get; }

		public DateTime BirthDate { get; }

		public Sex Sex { get; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (NaturalPerson)obj;
			return ToString().Equals(other.ToString());
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
		    return $"{FirstName} {LastName} {BirthDate.ToInvariantCultureDateString()} {Enum.GetName(typeof(Sex), Sex)}";
		}
	}
}