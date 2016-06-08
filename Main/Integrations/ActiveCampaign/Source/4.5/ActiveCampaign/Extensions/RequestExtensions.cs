namespace EyeSoft.ActiveCampaign.Extensions
{
	using System.Collections.Specialized;
	using System.Linq;

	using EyeSoft.ActiveCampaign.Commanding;

	public static class RequestExtensions
	{
		public static NameValueCollection GetNamedValueCollection(this ActiveCampaignRequest request)
		{
			const string PValuePropertyName = "PValues";

			var formFields = new NameValueCollection();

			request.GetType().GetProperties()
				.ToList()
				.ForEach(
					pi =>
						{
							var propertyValue = pi.GetValue(request, null);

							if (propertyValue == null)
							{
								return;
							}

							var propertyName = pi.Name == PValuePropertyName ?
								$"P[{propertyValue}]" :
								pi.Name.CamelCaseToUnderscore();

							formFields.Add(propertyName, propertyValue.ToString());
						});

			return formFields;
		}
	}
}