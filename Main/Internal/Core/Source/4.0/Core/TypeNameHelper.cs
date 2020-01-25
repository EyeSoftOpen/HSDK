namespace EyeSoft
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;

    public static class TypeNameHelper
    {
        public static string FriendlyName(Type type, IEnumerable<string> namespaceNameCollection)
        {
            var name = FriendlyName(type);

            var typeName =
                namespaceNameCollection
                    .OrderByDescending(namespaceName => namespaceName)
                    .Aggregate(name, (current, namespaceName) => current.Replace(namespaceName + ".", null));

            return typeName;
        }

        public static string FriendlyShortName(Type type)
        {
            var fullName = FriendlyName(type);
            return ShortName(fullName);
        }

        public static string ShortName(string fullName)
        {
            var friendlyName =
                fullName
                    .Replace(">", null)
                    .Split('<')
                    .Select(item => item.Substring(item.LastIndexOf('.') + 1))
                    .Aggregate((current, next) => current + '<' + next);

            return friendlyName + new string('>', fullName.Count('>', true));
        }

        public static string FriendlyName(Type type)
        {
            var typeRef = new CodeTypeReference(type);
            var friendlyName = new TypeName().GetTypeOutput(typeRef);
            return friendlyName;
        }


    }
}