namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System;

	public class ContactAutomation
	{
		public ContactAutomation(int id, string name, ContactAutomationStatus status, DateTime adddate, DateTime? remdate, int lastblock, DateTime lastdate)
		{
			Id = id;
			Name = name;
			Status = status;
			AddDate = adddate;
			LastDate = lastdate;
			RemovedDate = remdate;
			LastBlock = lastblock;
		}

		public int Id { get; }

		public string Name { get; }

		public ContactAutomationStatus Status { get; }

		public DateTime AddDate { get; }

		public DateTime? RemovedDate { get; }

		public DateTime LastDate { get; }

		public int LastBlock { get; }
	}
}