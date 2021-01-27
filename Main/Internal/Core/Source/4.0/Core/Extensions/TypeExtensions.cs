namespace EyeSoft.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Collections.Generic;
    using Mapping;
    using Reflection;

    public static class TypeExtensions
    {
        public static readonly Type ObjectType = typeof(object);

        public static readonly Type StringType = typeof(string);

        private const BindingFlags InstanceBindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

        private static readonly IEqualityComparer<Type> typeEqualityComparer =
            new TypeEqualityComparer();

        public static string FriendlyShortName(this Type type)
        {
            return TypeNameHelper.FriendlyShortName(type);
        }

        public static string FriendlyName(this Type type, IEnumerable<string> namespaceNameCollection)
        {
            return TypeNameHelper.FriendlyName(type, namespaceNameCollection);
        }

        public static string FriendlyName(this Type type, params string[] namespaceNameCollection)
        {
            return TypeNameHelper.FriendlyName(type, namespaceNameCollection);
        }

        public static bool IsValueTypeOrString(this Type type)
        {
            return type.IsValueType || type == StringType;
        }

        public static bool IsString(this Type type)
        {
            return type == StringType;
        }

        public static bool IsOneOf(this Type type, params Type[] types)
        {
            return type.IsOneOf(types.AsEnumerable());
        }

        public static bool IsOneOf(this Type type, IEnumerable<Type> types)
        {
            return types.Any(type.EqualsOrSubclassOf);
        }

        public static IEnumerable<PropertyInfo> GetCollectionProperties(this Type type)
        {
            return type.GetPublicProperties().Where(property => property.PropertyType.IsEnumerable());
        }

        public static IEnumerable<PropertyInfo> GetNotPrimitiveProperties(this Type type)
        {
            return type.GetPublicProperties().Where(x => !x.PropertyType.IsPrimitiveType());
        }

        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static PropertyInfo GetPublicProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        }

        public static PropertyInfo GetAnyInstanceProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName, InstanceBindingFlags);
        }

        public static IEnumerable<MemberInfo> GetAnyInstanceFieldsOrProperties(this Type type)
        {
            var members = type.GetMembers(InstanceBindingFlags).AsEnumerable();

            members = members.Where(member => (member.MemberType == MemberTypes.Property) || (member.MemberType == MemberTypes.Field));

            members = members.Where(member => member.GetAttribute<CompilerGeneratedAttribute>().IsNull());

            return members;
        }

        public static IEnumerable<Type> RelatedTypes(this Type typeToInspect)
        {
            return typeToInspect.RelatedTypes(new HashSet<Type>(), type => true);
        }

        public static IEnumerable<Type> RelatedTypes(this Type typeToInspect, Func<Type, bool> predicate)
        {
            return typeToInspect.RelatedTypes(new HashSet<Type>(), predicate);
        }

        public static IEnumerable<PropertyInfo> GetAnyInstanceProperties(this Type type)
        {
            return type.GetProperties(InstanceBindingFlags);
        }

        public static IEnumerable<PropertyInfo> GetReferenceProperties(this Type type)
        {
            return
                type.GetPublicProperties()
                    .Where(property => !property.PropertyType.IsEnumerable() && !property.PropertyType.IsPrimitiveType());
        }

        public static bool IsEnumerable(this Type type)
        {
            if (type.IsPrimitiveType())
            {
                return false;
            }

            return
                (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))) ||
                type.GetInterfaces().Any(x => (x.IsGenericType && (x.GetGenericTypeDefinition() == typeof(IEnumerable<>))));
        }

        public static bool IsPrimitiveType(this Type type)
        {
            return
                type.IsEnum ||
                type.IsPrimitive ||
                ((type == typeof(Guid)) ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime));
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsOfType<T>(this Type type)
        {
            return type == typeof(T);
        }

        public static bool Implements<TInterface>(this Type type)
            where TInterface : class
        {
            return type.Implements(typeof(TInterface));
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            if (interfaceType.IsGenericTypeDefinition)
            {
                return type.GetInterfaces().Any(c => c.IsGenericType && c.GetGenericTypeDefinition() == interfaceType);
            }

            return type.GetInterfaces().Any(item => item == interfaceType);
        }

        public static bool Implements(this Type type, string typeName)
        {
            return type.GetInterface(typeName).IsNotNull();
        }

        public static bool IsEnumerableOf<TInterface>(this Type type) where TInterface : class
        {
            if (!type.IsEnumerable())
            {
                return false;
            }

            var genericArguments = type.GetGenericArguments();

            if (genericArguments.Length > 1)
            {
                throw new ArgumentException("The generic argument must be a single type.");
            }

            var genericArgument = genericArguments.Single();

            return
                genericArgument == typeof(TInterface) ||
                genericArgument.IsSubclassOf(typeof(TInterface)) ||
                genericArgument.Implements<TInterface>();
        }

        public static bool EqualsOrSubclassOf<T>(this Type type)
        {
            return EqualsOrSubclassOf(type, typeof(T), true);
        }

        public static bool EqualsOrSubclassOf<T>(this Type type, bool simplifyTypeToCompare)
        {
            return EqualsOrSubclassOf(type, typeof(T), simplifyTypeToCompare);
        }

        public static bool EqualsOrSubclassOf(this Type type, Type typeToCheck)
        {
            return EqualsOrSubclassOf(type, typeToCheck, true);
        }

        public static bool EqualsOrSubclassOf(this Type type, Type typeToCheck, bool simplifyTypeToCompare)
        {
            var localType = type.ToBase();
            var localTypeToCheck = simplifyTypeToCompare ? typeToCheck.ToBase() : typeToCheck;

            if (localType == localTypeToCheck)
            {
                return true;
            }

            if (localTypeToCheck.IsAssignableFrom(localType))
            {
                return true;
            }

            if (localTypeToCheck.IsInterface)
            {
                if (localType.GetInterfaces().Any(typeToCheck.EqualsOrSubclassOf))
                {
                    return true;
                }
            }

            if (!localType.IsEnumerable())
            {
                return localType.IsSubclassOf(localTypeToCheck);
            }

            var baseType = type;
            var genericTypeToCheckArguments = typeToCheck.IsGenericType ? typeToCheck.GetGenericArguments() : null;

            var genericArgumentsAreGenericType =
                genericTypeToCheckArguments != null &&
                genericTypeToCheckArguments.All(argument => argument.IsGenericParameter);

            var objectType = typeof(object);

            while (baseType != null && baseType != objectType)
            {
                if (!baseType.IsGenericType || !typeToCheck.IsGenericType || !genericArgumentsAreGenericType)
                {
                    if (baseType == typeToCheck)
                    {
                        return true;
                    }
                }
                else
                {
                    var genericArguments = baseType.GetGenericArguments();

                    if (genericArguments.Length == genericTypeToCheckArguments.Length)
                    {
                        var typedTypeToCheck = typeToCheck.MakeGenericType(genericArguments);

                        if (typedTypeToCheck == baseType)
                        {
                            return true;
                        }
                    }
                }

                if (baseType == type.BaseType)
                {
                    return false;
                }

                baseType = type.BaseType;
            }

            return false;
        }

        public static Type ToBase(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition();
            }

            return type;
        }

        public static Field<T> GetField<T>(this Type type)
        {
            var fieldInfo =
                type
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                    .Single(f => f.FieldType == typeof(T));

            return
                new Field<T>(fieldInfo);
        }

        public static Type TypeOrGenericParameterOfEnumerable(this Type type)
        {
            var enumerableType = typeof(IEnumerable<>);

            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == enumerableType)
                {
                    return type.GetGenericArguments().Single();
                }
            }

            var enumerable =
                type
                    .GetInterfaces()
                        .Where(itemType => itemType.IsGenericType)
                        .SingleOrDefault(itemType => itemType.GetGenericTypeDefinition() == enumerableType);

            if (enumerable.IsNull())
            {
                return type;
            }

            return
                enumerable
                    .GetGenericArguments()
                    .Single();
        }

        public static Type RootType(this Type type)
        {
            if (type.IsValueType)
            {
                return type;
            }

            var baseType = type;

            while (baseType != ObjectType)
            {
                if ((baseType.BaseType == ObjectType) || (baseType.BaseType == null))
                {
                    break;
                }

                baseType = baseType.BaseType;
            }

            return baseType;
        }

        public static object CreateInstance(this Type type, params object[] parameters)
        {
            return CreateInstance<object>(type, parameters);
        }

        public static T CreateInstance<T>(this Type type, params object[] arguments)
            where T : class
        {
            return CreateInstanceBuilder(type, arguments).Create<T>();
        }

        public static InstanceBuilder CreateInstanceBuilder(this Type type, params object[] arguments)
        {
            var argumentArray = (arguments == null) ? Enumerable.Empty<object>().ToArray() : arguments;

            var argumentValues = argumentArray.Select(arg => arg).ToArray();
            var argumentTypes = argumentValues.Select(arg => arg.GetType()).ToArray();

            var constructorMatchingWithParameters =
                type.ConstructorContainsParameters(argumentTypes);

            return new InstanceBuilder(type, constructorMatchingWithParameters, argumentArray);
        }

        public static ConstructorInfo ConstructorMatchingWithParameters(
            this Type type,
            params Type[] argumentTypes)
        {
            Func<ConstructorInfo, bool> findMatchingConstructor =
                constructor =>
                    constructor.GetParameters().Select(p => p.ParameterType)
                        .SequenceEqual(argumentTypes, typeEqualityComparer);

            var matchingConstructor =
                type.GetConstructors().SingleOrDefault(findMatchingConstructor);

            return matchingConstructor;
        }

        public static ConstructorInfo ConstructorContainsParameters(
            this Type type,
            params Type[] argumentTypes)
        {
            Func<ConstructorInfo, bool> findMatchingConstructor =
                constructor =>
                    constructor.GetParameters().Select(p => p.ParameterType)
                        .ContainsSequence(argumentTypes, typeEqualityComparer);

            var matchingConstructor =
                type.GetConstructors().FirstOrDefault(findMatchingConstructor);

            return matchingConstructor;
        }

        public static Stream GetResourceStream(this Type type, string resourceKey, bool isFullName = false)
        {
            return type.Assembly.GetResourceStream(resourceKey, isFullName);
        }

        public static string ReadResourceText(this Type type, string resourceKey, bool isFullName = false)
        {
            return type.Assembly.ReadResourceText(resourceKey, isFullName);
        }

        internal static IEnumerable<Type> RelatedTypes(
            this Type typeToInspect,
            HashSet<Type> processedTypes,
            Func<Type, bool> predicate)
        {
            var relatedTypes =
                typeToInspect
                    .GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                    .Select(member => new MemberInfoMetadata(member))
                    .Select(currentType => currentType.Type.TypeOrGenericParameterOfEnumerable())
                    .Where(predicate)
                    .DistinctBy(memberType => memberType)
                    .ToList();

            if (relatedTypes.Empty())
            {
                return Enumerable.Empty<Type>();
            }

            if (processedTypes.ContainsSequence(relatedTypes))
            {
                return Enumerable.Empty<Type>();
            }

            processedTypes.AddRange(relatedTypes);

            foreach (var relatedType in relatedTypes.Where(relatedType => relatedType != typeToInspect))
            {
                var relatedTypesNotYetProcessed =
                    relatedType.RelatedTypes(processedTypes, predicate)
                    .Where(type => !processedTypes.Contains(type));

                foreach (var type in relatedTypesNotYetProcessed)
                {
                    type.RelatedTypes(processedTypes, predicate);
                }
            }

            return processedTypes;
        }
    }
}