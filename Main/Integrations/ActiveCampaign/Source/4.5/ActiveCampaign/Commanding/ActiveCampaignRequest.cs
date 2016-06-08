namespace EyeSoft.ActiveCampaign.Commanding
{
	public abstract class ActiveCampaignRequest
	{
		public string ApiKey { get; set; }

		public string ApiAction { get; set; }

		public string ApiOutput { get; set; }
	}
}