namespace EyeSoft.ActiveCampaign.Helpers
{
	using EyeSoft.ActiveCampaign.Extensions;

	using Newtonsoft.Json.Serialization;

	internal class UnderscoreMappingResolver : DefaultContractResolver
	{
		protected override string ResolvePropertyName(string propertyName)
		{
			return propertyName.CamelCaseToUnderscore();
		}
	}
}