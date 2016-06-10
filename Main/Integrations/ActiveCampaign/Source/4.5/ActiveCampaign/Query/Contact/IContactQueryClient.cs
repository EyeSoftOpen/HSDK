namespace EyeSoft.ActiveCampaign.Query.Contact
{
	using System;

	using EyeSoft.ActiveCampaign.Query.Contact.Models;

	public interface IContactQueryClient : IDisposable
	{
		Contacts GetContacts(params string[] ids);

		Contacts GetAllContacts();

		Contact Get(string email);
	}
}