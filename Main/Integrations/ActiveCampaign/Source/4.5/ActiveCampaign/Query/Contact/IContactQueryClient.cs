namespace EyeSoft.ActiveCampaign.Query.Contact
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.Contact.Models;

	public interface IContactQueryClient : IDisposable
	{
		Contact Get(string email);

		IEnumerable<Contact> GetAll();

		Contacts GetContacts(params int[] ids);
	}
}