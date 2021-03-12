namespace EyeSoft.Core.Test
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Extensions;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void CheckElementIsOneOfSequence()
        {
            typeof(ReadOnlyCollection<string>).IsOneOf(typeof(ReadOnlyCollection<>)).Should().BeTrue();
        }

        [TestMethod]
        public void CheckCollectionsAreEnumerableOfBaseInterface()
        {
            CheckIsEnumerableOfBase<IEnumerable<Customer>>();
            CheckIsEnumerableOfBase<List<Customer>>();
            CheckIsEnumerableOfBase<Collection<Customer>>();
            CheckIsEnumerableOfBase<ObservableCollection<Customer>>();
        }

        [TestMethod]
        public void CheckCollectionsAreEnumerableOfSameClass()
        {
            CheckIsEnumerableOfSame<IEnumerable<Customer>>();
            CheckIsEnumerableOfSame<List<Customer>>();
            CheckIsEnumerableOfSame<Collection<Customer>>();
            CheckIsEnumerableOfSame<ObservableCollection<Customer>>();
        }

        [TestMethod]
        public void CheckCollectionsImplementsListInterface()
        {
            var typeName = typeof(IList<>).Name;
            typeof(IEnumerable<string>).Implements(typeName).Should().BeFalse();
            typeof(List<string>).Implements(typeName).Should().BeTrue();
            typeof(Collection<string>).Implements(typeName).Should().BeTrue();
            typeof(ObservableCollection<string>).Implements(typeName).Should().BeTrue();
        }

        [TestMethod]
        public void CheckTypeIsEqualsToItSelf()
        {
            typeof(Teacher).EqualsOrSubclassOf(typeof(Teacher)).Should().BeTrue();
        }

        [TestMethod]
        public void CheckDerivedTypeIsSubclassOfBaseType()
        {
            typeof(Teacher).EqualsOrSubclassOf<Person>().Should().BeTrue();
        }

        [TestMethod]
        public void CheckArrayIsSubclassOfArray()
        {
            var type = typeof(byte[]);

            type.EqualsOrSubclassOf(typeof(string))
                .Should().BeFalse();
        }

        [TestMethod]
        public void CheckBaseTypeIsNotSubclassOfDerivedType()
        {
            typeof(Person).EqualsOrSubclassOf<Teacher>().Should().BeFalse();
        }

        [TestMethod]
        public void CheckTypeIsSubclassOfObject()
        {
            typeof(Person).EqualsOrSubclassOf<object>().Should().BeTrue();
        }

        [TestMethod]
        public void CheckParametrizedCollectionIsSubclassOfGenericCollection()
        {
            typeof(ReadOnlyCollection<string>)
                .EqualsOrSubclassOf(typeof(ReadOnlyCollection<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void CheckSameTypeIsReturnedOnNotEnumerable()
        {
            var type = typeof(Customer);

            type.TypeOrGenericParameterOfEnumerable()
                .Should().Be(type);
        }

        [TestMethod]
        public void CheckGenericTypeIsReturnedOnEnumerable()
        {
            var type = typeof(List<Customer>);

            type.TypeOrGenericParameterOfEnumerable()
                .Should().Be(typeof(Customer));
        }

        [TestMethod]
        public void CheckGenericCollectionIsNotSubclassOfParametrizedCollection()
        {
            typeof(ReadOnlyCollection<>)
                .EqualsOrSubclassOf<ReadOnlyCollection<string>>(false)
                .Should().BeFalse();
        }

        [TestMethod]
        public void RelatedTypesCheckOnEmptyClass()
        {
            typeof(Person).RelatedTypes()
                .Should().BeEmpty();
        }

        [TestMethod]
        public void RelatedTypesCheck()
        {
            var expected = new[] { typeof(Person), typeof(LocalAddress), typeof(City) };

            typeof(BusinessUnit).RelatedTypes()
                .Should().BeSameAs(expected);
        }

        [TestMethod]
        public void CreateInstanceOfSameTypeOfTheConstructorParameter()
        {
            typeof(PersonFactory)
                .CreateInstance<PersonFactory>(new Teacher())
                .Should().BeOfType<PersonFactory>();
        }

        [TestMethod]
        public void VerifyNullableTypeIsTrue()
        {
            typeof(int?).IsNullable().Should().BeTrue();
        }

        [TestMethod]
        public void VerifyNotNullableTypeIsFalse()
        {
            typeof(int).IsNullable().Should().BeFalse();
        }

        [TestMethod]
        public void GetConstructorFromParameterTypes()
        {
            typeof(ConstructorFromParameters)
                .ConstructorMatchingWithParameters(typeof(string))
                .Should().NotBeNull();
        }

        [TestMethod]
        public void GetConstructorFromParameterTypesWithEnumerable()
        {
            typeof(ConstructorFromParametersWithEnumerable)
                .ConstructorMatchingWithParameters(typeof(List<string>), typeof(bool))
                .Should().NotBeNull();
        }

        private void CheckIsEnumerableOfSame<T>()
        {
            CheckIsEnumerableOf<T, Customer>();
        }

        private void CheckIsEnumerableOfBase<T>()
        {
            CheckIsEnumerableOf<T, IEntity>();
        }

        private void CheckIsEnumerableOf<T, TTypeCheck>() where TTypeCheck : class
        {
            var type = typeof(T);

            var message = $"{type} is not recognized as IEnumerable<TInterface>.";

            type.IsEnumerableOf<TTypeCheck>().Should().BeTrue(message);
        }

        private class Person
        {
        }

        private class Teacher : Person
        {
        }

        private abstract class BusinessUnit
        {
            public Person Manager { get; set; }

            public IList<Person> Employees { get; set; }

            public LocalAddress Address { get; set; }

            public IEnumerable<LocalAddress> LocalAddresses { get; set; }
        }

        private abstract class LocalAddress
        {
            public City City { get; set; }

            public Person Person { get; set; }
        }

        private abstract class City
        {
        }

        private class PersonFactory
        {
            public PersonFactory(Person person)
            {
            }
        }

        private class ConstructorFromParameters
        {
            public ConstructorFromParameters(string param)
            {
            }
        }

        private class ConstructorFromParametersWithEnumerable
        {
            public ConstructorFromParametersWithEnumerable(IEnumerable<string> param, bool param2)
            {
            }
        }
    }
}