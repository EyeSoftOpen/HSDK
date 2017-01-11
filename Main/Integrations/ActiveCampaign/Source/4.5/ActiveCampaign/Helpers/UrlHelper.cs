namespace EyeSoft.ActiveCampaign.Helpers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

    internal static class UrlHelper
    {
        const string FilterFieldPropertyName = "FilterField";

        const string FilterValuesPropertyName = "FilterValues";

        private static readonly ISet<string> knownProperties =
               new HashSet<string> { "Error", "UserDescription", "Version" };

        public static string ToQueryString(this object request)
        {
            if (request == null)
            {
                return string.Empty;
            }

            var requestString = request as string;

            if (requestString != null)
            {
                return requestString;
            }

            // Get all properties on the object
            var properties =
                request.GetType()
                    .GetProperties()
                    .Where(x => x.CanRead)
                    .Where(x => x.GetValue(request, null) != null)
                    .ToDictionary(
                        k => k.Name,
                        v =>
                        {
                            var value = v.GetValue(request, null);
                            return value.GetType().IsEnum ? (int)value : value;
                        });

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable).Select(x => x.Key).ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();

                var valueElemType = valueType.IsGenericType ? valueType.GetGenericArguments()[0] : valueType.GetElementType();

                var isEnum = valueElemType.IsEnum;

                Func<object, string> toString = x => isEnum ? ((int)x).ToString() : x.ToString();

                var isEnumerable = properties[key] is IEnumerable;

                if (!valueElemType.IsPrimitive && valueElemType != typeof(string) && !isEnumerable)
                {
                    continue;
                }

                var enumerable = properties[key] as IEnumerable;

                Func<IEnumerable, IEnumerable<string>> enumerableValues = x => x.Cast<object>().Select(i => toString(i));

                string propertyName;

                if (key == FilterValuesPropertyName)
                {
                    var filterField = properties[FilterFieldPropertyName];

                    propertyName = $"filters[{filterField}]";
                }
                else
                {
                    propertyName = key;
                }

                var tempName = CamelCaseToUnderscorePropertyName(propertyName);

                var tempValues = enumerableValues(enumerable).Select(x => x + "&").ToList();

                string value;

                if (tempValues.Count() == 1)
                {
                    value = $"{tempName}={string.Join("", tempValues)}";
                }
                else
                {
                    value = string.Join(tempName + "=", tempValues);
                }

                properties[key] = value.TrimEnd('&');
            }

            var propertiesValues = properties.Where(p => p.Key != FilterFieldPropertyName).Select(x => ResolvePropertyName(x.Key, x.Value.ToString()));

            return string.Join("&", propertiesValues);
        }

        public static string ToQueryString(this IEnumerable enumerbale, string enumerableName)
        {
            return string.Join("&", enumerbale.Cast<object>().Select(x => string.Concat(enumerableName, "=", x.ToString())));
        }

        private static string ResolvePropertyName(string propertyName, string propertyValue)
        {
            if (propertyName == FilterValuesPropertyName)
            {
                return propertyValue;
            }

            return string.Concat(CamelCaseToUnderscorePropertyName(propertyName), "=", propertyValue);
        }

        private static string CamelCaseToUnderscorePropertyName(string propertyName)
        {
            if (propertyName == "LogContext")
            {
                return propertyName;
            }

            var newPropertyName =
                Regex.Replace(propertyName, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();

            if (knownProperties.Contains(propertyName))
            {
                newPropertyName = string.Concat("@", newPropertyName);
            }

            return newPropertyName;
        }
    }
}
