namespace EyeSoft.Core.Test
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Extensions;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void CheckElementIsOneOfSequence()
        {
            typeof(ReadOnlyCollection<string>).IsOneOf(typeof(ReadOnlyCollection<>)).Should().Be.True();
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
            typeof(IEnumerable<string>).Implements(typeName).Should().Be.False();
            typeof(List<string>).Implements(typeName).Should().Be.True();
            typeof(Collection<string>).Implements(typeName).Should().Be.True();
            typeof(ObservableCollection<string>).Implements(typeName).Should().Be.True();
        }

        [TestMethod]
        public void CheckTypeIsEqualsToItSelf()
        {
            typeof(Teacher).EqualsOrSubclassOf(typeof(Teacher)).Should().Be.True();
        }

        [TestMethod]
        public void CheckDerivedTypeIsSubclassOfBaseType()
        {
            typeof(Teacher).EqualsOrSubclassOf<Person>().Should().Be.True();
        }

        [TestMethod]
        public void CheckArrayIsSubclassOfArray()
        {
            var type = typeof(byte[]);

            type.EqualsOrSubclassOf(typeof(string))
                .Should().Be.False();
        }

        [TestMethod]
        public void CheckBaseTypeIsNotSubclassOfDerivedType()
        {
            typeof(Person).EqualsOrSubclassOf<Teacher>().Should().Be.False();
        }

        [TestMethod]
        public void CheckTypeIsSubclassOfObject()
        {
            typeof(Person).EqualsOrSubclassOf<object>().Should().Be.True();
        }

        [TestMethod]
        public void CheckParametrizedCollectionIsSubclassOfGenericCollection()
        {
            typeof(ReadOnlyCollection<string>)
                .EqualsOrSubclassOf(typeof(ReadOnlyCollection<>))
                .Should().Be.True();
        }

        [TestMethod]
        public void CheckSameTypeIsReturnedOnNotEnumerable()
        {
            var type = typeof(Customer);

            type.TypeOrGenericParameterOfEnumerable()
                .Should().Be.EqualTo(type);
        }

        [TestMethod]
        public void CheckGenericTypeIsReturnedOnEnumerable()
        {
            var type = typeof(List<Customer>);

            type.TypeOrGenericParameterOfEnumerable()
                .Should().Be.EqualTo(typeof(Customer));
        }

        [TestMethod]
        public void CheckGenericCollectionIsNotSubclassOfParametrizedCollection()
        {
            typeof(ReadOnlyCollection<>)
                .EqualsOrSubclassOf<ReadOnlyCollection<string>>(false)
                .Should().Be.False();
        }

        [TestMethod]
        public void RelatedTypesCheckOnEmptyClass()
        {
            typeof(Person).RelatedTypes()
                .Should().Be.Empty();
        }

        [TestMethod]
        public void RelatedTypesCheck()
        {
            var expected = new[] { typeof(Person), typeof(LocalAddress), typeof(City) };

            typeof(BusinessUnit).RelatedTypes()
                .Should().Have.SameSequenceAs(expected);
        }

        [TestMethod]
        public void CreateInstanceOfSameTypeOfTheConstructorParameter()
        {
            typeof(PersonFactory)
                .CreateInstance<PersonFactory>(new Teacher())
                .Should().Be.InstanceOf<PersonFactory>();
        }

        [TestMethod]
        public void VerifyNullableTypeIsTrue()
        {
            typeof(int?).IsNullable().Should().Be.True();
        }

        [TestMethod]
        public void VerifyNotNullableTypeIsFalse()
        {
            typeof(int).IsNullable().Should().Be.False();
        }

        [TestMethod]
        public void GetConstructorFromParameterTypes()
        {
            typeof(ConstructorFromParameters)
                .ConstructorMatchingWithParameters(typeof(string))
                .Should().Not.Be.Null();
        }

        [TestMethod]
        public void GetConstructorFromParameterTypesWithEnumerable()
        {
            typeof(ConstructorFromParametersWithEnumerable)
                .ConstructorMatchingWithParameters(typeof(List<string>), typeof(bool))
                .Should().Not.Be.Null();
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

            type.IsEnumerableOf<TTypeCheck>().Should(message).Be.True();
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