namespace EyeSoft.ActiveCampaign.Extensions
{
	using System.Collections.Specialized;
	using System.Linq;

	using EyeSoft.ActiveCampaign.Commanding;

    internal static class RequestExtensions
    {
        public static NameValueCollection GetNamedValueCollection(this ActiveCampaignRequest request)
        {
            const string PValuePropertyName = "PValues";

            const string FilterFieldPropertyName = "FilterField";

            const string FilterValuePropertyName = "FilterValues";

            var formFields = new NameValueCollection();

            var properties = request
                        .GetType()
                        .GetProperties()
                        .ToList();

            properties
                .Where(p => p.Name != FilterFieldPropertyName)
                .ToList()
                .ForEach(
                    pi =>
                    {
                        var propertyValue = pi.GetValue(request, null);

                        if (propertyValue == null)
                        {
                            return;
                        }

                        string propertyName;

                        if (pi.Name == PValuePropertyName)
                        {
                            propertyName = $"p[{propertyValue}]";
                        }

                        if (pi.Name == FilterValuePropertyName)
                        {
                            var filterField = properties.Single(p => p.Name == FilterFieldPropertyName).GetValue(request, null);

                            propertyName = $"filters[{filterField}]";
                        }

                        propertyName = pi.Name.CamelCaseToUnderscore();

                        formFields.Add(propertyName, propertyValue.ToString());
                    });

            return formFields;
        }
    }
}