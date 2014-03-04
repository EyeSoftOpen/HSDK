namespace EyeSoft.Accounting
{
	using System;

	public class NaturalPerson
	{
		public NaturalPerson(string firstName, string lastName, DateTime birthDate, Sex sex)
		{
			Enforce
				.Argument(() => firstName)
				.Argument(() => lastName)
				.Argument(() => birthDate);

			FirstName = firstName;
			LastName = lastName;
			BirthDate = birthDate;
			Sex = sex;
		}

		public string FirstName { get; private set; }

		public string LastName { get; private set; }

		public DateTime BirthDate { get; private set; }

		public Sex Sex { get; private set; }

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
			return
				"{FirstName} {LastName} {BirthDate} {Sex}"
				.NamedFormat(
					FirstName,
					LastName,
					BirthDate.ToInvariantCultureDateString(),
					Enum.GetName(typeof(Sex), Sex));
		}
	}
}