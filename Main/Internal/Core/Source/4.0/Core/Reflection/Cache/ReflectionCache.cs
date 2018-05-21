namespace EyeSoft.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionCache
    {
        private static readonly object lockInstance = new object();

        private static readonly MembersCache<FieldInfo> typeFieldInfoCache =
            new MembersCache<FieldInfo>();

        private static readonly MembersCache<MethodInfo> typeMethodInfoCache =
            new MembersCache<MethodInfo>();

        public static FieldInfo Field(this Type type, string propertyName)
        {
            var key = new MemberKey(type, propertyName);

            lock (lockInstance)
            {
                if (!typeFieldInfoCache.ContainsKey(key))
                {
                    var cachedField = GetFieldByPropertyName(type, propertyName);

                    typeFieldInfoCache.Add(key, cachedField);
                }
            }

            var field = typeFieldInfoCache[key];
            return field;
        }

        public static void Execute(this object obj, string methodName, params object[] parameters)
        {
            var method = GetMethodInfo(obj.GetType(), methodName, parameters);

            method.Invoke(obj, parameters);
        }

        private static MethodInfo GetMethodInfo(Type type, string methodName, IEnumerable<object> parameters)
        {
            var parametersType = parameters.Select(p => p.GetType()).ToArray();

            var parametersTypeName = parametersType.Select(p => p.Name).Join("/");

            var key = new MemberKey(type, string.Concat(methodName, parametersTypeName));

            lock (lockInstance)
            {
                if (!typeMethodInfoCache.ContainsKey(key))
                {
                    var methodInfo =
                        type.GetMethod(
                            methodName,
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                            null,
                            new[] { typeof(string) },
                            null);

                    if (methodInfo == null)
                    {
                        var message = $"Cannot find the method '{methodName}' into the type '{type}'.";

                        new ArgumentException(message).Throw();
                    }

                    typeMethodInfoCache.Add(key, methodInfo);
                }
            }

            var method = typeMethodInfoCache[key];

            return method;
        }

        private static FieldInfo GetFieldByPropertyName(IReflect viewModelType, string propertyName)
        {
            var charList = new List<char> { char.ToLower(propertyName[0]) };
            charList.AddRange(propertyName.Substring(1));
            var fieldName = new string(charList.ToArray());

            var field = viewModelType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            return field;
        }
    }
}