namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
    using EyeSoft.ActiveCampaign.Commanding;

    internal class PaginatedContactsRequest : ActiveCampaignRequest
    {
        public int Offset { get; set; }

        public int PageSize { get; set; }

        public string Filter { get; set; }
    }
}