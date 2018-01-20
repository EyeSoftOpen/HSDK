namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
    using EyeSoft.ActiveCampaign.Commanding;

    internal class PaginatedContactsRequest : ActiveCampaignRequest
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Filter { get; set; }

        public int Public { get; set; }
    }
}