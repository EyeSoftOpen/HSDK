namespace EyeSoft.Core.Test.Collections.Generic
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using EyeSoft.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class CollectionExtensionsTest
	{
		[TestMethod]
		public void SynchronizeHomogeneousCollection()
		{
			var collection = new Collection<Customer> { new Customer("1"), new Customer("4"), new Customer("5") };

			var synchronizeWith = new[] { new Customer("1"), new Customer("5"), new Customer("6") };

			collection.Synchronize(synchronizeWith);

			collection.Should().Have.SameSequenceAs(new Customer("1"), new Customer("5"), new Customer("6"));
		}

		[TestMethod]
		public void SynchronizeHomogeneousCollectionWithActionOnDelete()
		{
			var collection = new Collection<Customer> { new Customer("1"), new Customer("4"), new Customer("5") };

			var synchronizeWith = new[] { new Customer("1"), new Customer("5"), new Customer("6") };

			collection.Synchronize(synchronizeWith, SetCustomerAsDeleted);

			collection.Should().Have.SameSequenceAs(collection);

			foreach (var item in collection.Except(synchronizeWith))
			{
				item.IsDeleted.Should().Be.True();
			}
		}

		[TestMethod]
		public void SynchronizeHeterogeneousCollection()
		{
			var collection = new Collection<Customer> { new Customer("1"), new Customer("4"), new Customer("5") };

			var synchronizeWith = new[] { new CustomerDto("1"), new CustomerDto("5"), new CustomerDto("6") };

			collection.Synchronize(synchronizeWith, x => new Customer(x.Name));

			collection.Should().Have.SameSequenceAs(new Customer("1"), new Customer("5"), new Customer("6"));
		}

		[TestMethod]
		public void SynchronizeHeterogeneousCollectionWithActionOnDelete()
		{
			var collection = new Collection<Customer> { new Customer("1"), new Customer("4"), new Customer("5") };

			var synchronizeWith = new[] { new CustomerDto("1"), new CustomerDto("5"), new CustomerDto("6") };

			collection.Synchronize(synchronizeWith, x => new Customer(x.Name), null, SetCustomerAsDeleted);

			collection.Should().Have.SameSequenceAs(collection);

			foreach (var item in collection.Where(x => synchronizeWith.All(s => s.Name != x.Name)))
			{
				item.IsDeleted.Should().Be.True();
			}
		}

		[TestMethod]
		public void SynchronizeHeterogeneousCollectionWithTheSameElementsTheCollectionMustNotChange()
		{
			var list = new List<Customer> { new Customer("1"), new Customer("4"), new Customer("5") };

			var collection = new Collection<Customer>(list);

			var synchronizeWith = new[] { new CustomerDto("1"), new CustomerDto("4"), new CustomerDto("5") };

			collection.Synchronize(synchronizeWith, x => new Customer(x.Name));

			for (var i = 0; i < list.Count; i++)
			{
				list[i].Should().Be.SameInstanceAs(collection[i]);
			}
		}

		private void SetCustomerAsDeleted(Customer customer)
		{
			customer.IsDeleted = true;
		}

		private class Customer
		{
			public Customer(string name)
			{
				Name = name;
			}

			public string Name { get; }

			public bool IsDeleted { get; set; }

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (Customer)obj;
				return Name.Equals(other.Name);
			}

			public override int GetHashCode()
			{
				return Name.GetHashCode();
			}

			public override string ToString()
			{
				return "Customer - " + Name;
			}
		}

		private class CustomerDto
		{
			public CustomerDto(string name)
			{
				Name = name;
			}

			public string Name { get; }

			public override string ToString()
			{
				return "CustomerDto - " + Name;
			}

			public override int GetHashCode()
			{
				return Name.GetHashCode();
			}
		}
	}
}
