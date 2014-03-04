namespace EyeSoft.Test.Normalization
{
	using EyeSoft.Normalization;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class NormalizerTest
	{
		private readonly DefaultNormalizer normalizer = new DefaultNormalizer();

		[TestMethod]
		public void NormalizeNullPropertiesMustDoNothing()
		{
			var obj = new Person();
			normalizer.TrimProperties(obj);
			obj.FirstName.Should().Be.Null();
			obj.LastName.Should().Be.Null();
			obj.Age.Should().Be.EqualTo(0);
			obj.FullName.Should().Be.Null();
		}

		[TestMethod]
		public void NormalizePropertyWithSpaceShouldBeTrimmed()
		{
			var obj = new Person { FirstName = "Bill " };
			normalizer.TrimProperties(obj);
			obj.FirstName.Should().Be.EqualTo("Bill");
			obj.LastName.Should().Be.Null();
			obj.Age.Should().Be.EqualTo(0);
			obj.FullName.Should().Be.EqualTo("Bill");
		}

		[TestMethod]
		public void NormalizeWithSpaceShouldBeNull()
		{
			var obj = new Person { FirstName = " " };
			normalizer.TrimProperties(obj);
			obj.FirstName.Should().Be.Null();
			obj.LastName.Should().Be.Null();
			obj.Age.Should().Be.EqualTo(0);
			obj.FullName.Should().Be.Null();
		}

		[TestMethod]
		public void NormalizeFirstLevelPropertyShouldWork()
		{
			var obj = new Person { LastName = " White " };
			normalizer.TrimProperties(obj);
			obj.FirstName.Should().Be.Null();
			obj.LastName.Should().Be.EqualTo("White");
			obj.Age.Should().Be.EqualTo(0);
			obj.FullName.Should().Be.EqualTo("White");
		}

		[TestMethod]
		public void NormalizeMultiplePropertiesShouldWork()
		{
			var obj = new Person("Bill ", " White ");
			normalizer.TrimProperties(obj);
			obj.FirstName.Should().Be.EqualTo("Bill");
			obj.LastName.Should().Be.EqualTo("White");
			obj.Age.Should().Be.EqualTo(0);
			obj.FullName.Should().Be.EqualTo("Bill White");
		}

		[TestMethod]
		public void NormalizeHierarchyInstancesWithCircularReferencesShouldWork()
		{
			var category = Category.CreateHierarchy();

			normalizer.Normalize(category);

			category.Name.Should().Be.EqualTo("Root");
			category.Children[0].Name.Should().Be.EqualTo("Root 1");
			category.Children[0].Children[0].Name.Should().Be.EqualTo("Root 1.1");
			category.Children[1].Name.Should().Be.EqualTo("Root 2");
		}

		[TestMethod]
		public void RemoveNormalizerForATypeTheInstanceShouldNotBeChanged()
		{
			const string FirstName = "Bill ";
			const string LastName = " White ";

			var person = new Person(FirstName, LastName);

			normalizer.Remove<Person>();

			normalizer.Normalize(person);

			person.FirstName.Should().Be.EqualTo(FirstName);
			person.LastName.Should().Be.EqualTo(LastName);
		}

		[TestMethod]
		public void CustomerNormalizerForATypeTheInstanceShouldBeCorrect()
		{
			const string FirstName = "Bill ";
			const string LastName = " White ";

			var person = new Person(FirstName, LastName) { Age = -1 };

			normalizer.Register<Person, PersonNormalizer>();

			normalizer.Normalize(person);

			person.Age.Should().Be.EqualTo(0);
			person.FirstName.Should().Be.EqualTo("BILL ");
			person.LastName.Should().Be.EqualTo(" WHITE ");
		}

		[TestMethod]
		public void CustomerNormalizerForAChildTypeTheInstanceShouldBeCorrect()
		{
			const string Street = " Address 1 ";
			var foo = new Foo("Acme ", Street);

			normalizer.Remove<Address>();

			normalizer.Normalize(foo);

			foo.Name.Should().Be.EqualTo("Acme");
			foo.Address.Street.Should().Be.EqualTo(Street);
		}

		private abstract class Party
		{
			public string FirstName { get; set; }
		}

		private class Person : Party
		{
			public Person()
			{
			}

			public Person(string firstName, string lastName)
			{
				LastName = lastName;
				FirstName = firstName;
			}

			public string LastName { get; set; }

			public int Age { get; set; }

			public string FullName
			{
				get
				{
					if (FirstName == null && LastName == null)
					{
						return null;
					}

					return string.Concat(FirstName, " ", LastName).Trim();
				}
			}
		}

		private class PersonNormalizer : Normalizer<Person>
		{
			public override void Normalize(Person obj)
			{
				ObjectTree.Traverse(obj, UpperProperties);
			}

			private void UpperProperties(object obj)
			{
				Normalizer.NormalizeProperties<object>(obj, UpperString);
			}

			private object UpperString(object arg)
			{
				var s = arg as string;

				if (s != null)
				{
					return s.ToUpper();
				}

				if (arg is int)
				{
					return ((int)arg) < 0 ? 0 : arg;
				}

				return arg;
			}
		}

		private class Foo
		{
			public Foo(string name, string street)
			{
				Name = name;
				Address = new Address(street);
			}

			public string Name { get; private set; }

			public Address Address { get; private set; }
		}

		private class Address
		{
			public Address(string street)
			{
				Street = street;
			}

			public string Street { get; private set; }
		}
	}
}