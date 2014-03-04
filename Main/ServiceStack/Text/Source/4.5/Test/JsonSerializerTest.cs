namespace EyeSoft.ServiceStack.Text.Test
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Extensions;
	using EyeSoft.Serialization;
	using EyeSoft.ServiceStack.Text;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class JsonSerializerTest
	{
		[TestMethod]
		public void SerializeToJsonCheckDeserializationInstanceIsTheSame()
		{
			Serializer.Set<JsonSerializerFactory>();

			const string Expected = "Bill";

			var person = new Person(Expected, new Account(5), new Account(12));

			var json = Serializer.SerializeToString(person);

			var deserialized = Serializer.DeserializeFromString<Person>(json);

			deserialized.Should().Be.EqualTo(person);

			deserialized.Name.Should().Be.EqualTo(Expected);
			deserialized.Accounts.Should().Have.Count.EqualTo(2);
		}

		private class Person
		{
			public Person(string name, params Account[] accounts)
			{
				Name = name;
				Accounts = accounts;
			}

			public string Name { get; private set; }

			public IEnumerable<Account> Accounts { get; private set; }

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (Person)obj;

				return Name == other.Name && Accounts.SequenceEqual(other.Accounts);
			}

			public override int GetHashCode()
			{
				var list = new List<object> { Name };
				list.AddRange(Accounts);
				return ObjectHash.Combine(list.ToArray());
			}
		}

		private class Account
		{
			public Account(int deposit)
			{
				Deposit = deposit;
			}

			public int Deposit { get; private set; }

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (Account)obj;
				return Deposit.Equals(other.Deposit);
			}

			public override int GetHashCode()
			{
				return Deposit.GetHashCode();
			}
		}
	}
}