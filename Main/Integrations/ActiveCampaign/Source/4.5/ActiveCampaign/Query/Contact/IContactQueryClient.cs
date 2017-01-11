namespace EyeSoft.ActiveCampaign.Query.Contact
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.Contact.Models;

    public interface IContactQueryClient : IDisposable
    {
        Contact Get(string email);

        IEnumerable<Contact> GetAll(int page = 0, string filterField = null, object[] filterValues = null);

        Contacts GetContacts(int page = 0, params int[] ids);
    }
}