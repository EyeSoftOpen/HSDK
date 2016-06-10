namespace EyeSoft.ActiveCampaign.Shell
{
	using System;
	using System.IO;

	using EyeSoft.ActiveCampaign;
	using EyeSoft.ActiveCampaign.Commanding.Automation;
	using EyeSoft.ActiveCampaign.Commanding.Contact;
	using EyeSoft.ActiveCampaign.Commanding.Contact.Models;
	using EyeSoft.ActiveCampaign.Query.Contact;
	using EyeSoft.ActiveCampaign.Shell.Helpers;

	using Newtonsoft.Json;

	public static class Program
	{
		public static void Main()
		{
			var testData = ReadActiveCampaignTestData(@"P:\ActiveCampaign\ActiveCampaignTestData.json");

			using (var connection = new ActiveCampaignConnection(testData.Account, testData.ApiKey))
			{
				//Commanding(connection, testData);

				Query(connection, testData);
			}

			Console.ReadLine();
		}

		private static ActiveCampaignTestData ReadActiveCampaignTestData(string activeCampaignTestDataJson)
		{
			var file = new FileInfo(activeCampaignTestDataJson);

			if (!file.Exists)
			{
				throw new FileNotFoundException($"Cannot find the ActiveCampaign test data file in '{file.FullName}'.");
			}

			var json = File.ReadAllText(activeCampaignTestDataJson);

			var activeCampaignTestData = JsonConvert.DeserializeObject<ActiveCampaignTestData>(json);

			return activeCampaignTestData;
		}

		private static void Query(ActiveCampaignConnection connection, ActiveCampaignTestData testData)
		{
			IContactQueryClient contactQueryClient = new ContactQueryClient(connection);

			contactQueryClient.Get(testData.ContactEmail).Dump();

			contactQueryClient.GetAllContacts().Dump();

			contactQueryClient.GetContacts(testData.ContactId).Dump();
		}

		private static void Commanding(ActiveCampaignConnection connection, ActiveCampaignTestData testData)
		{
			IContactCommandingClient contactCommandingClient = new ContactCommandingClient(connection);

			contactCommandingClient.Delete(testData.ContactId).Dump();

			contactCommandingClient.Add(new ContactAdd(testData.ContactId, testData.ContactEmail)).Dump();

			IAutomationCommandingClient automationCommandingClient = new AutomationCommandingClient(connection);

			automationCommandingClient.AddContact(testData.ContactEmail, testData.Automation).Dump();

			automationCommandingClient.RemoveContact(testData.ContactEmail, testData.Automation).Dump();
		}
	}
}