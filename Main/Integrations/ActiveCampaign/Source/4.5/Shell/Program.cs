namespace EyeSoft.ActiveCampaign.Shell
{
	using System;
	using System.IO;

	using EyeSoft.ActiveCampaign;
	using EyeSoft.ActiveCampaign.Commanding.Automation;
	using EyeSoft.ActiveCampaign.Commanding.Contact;
	using EyeSoft.ActiveCampaign.Commanding.Contact.Models;
    using EyeSoft.ActiveCampaign.Commanding.List;
    using EyeSoft.ActiveCampaign.Commanding.List.Models;
    using EyeSoft.ActiveCampaign.Query.Automation;
	using EyeSoft.ActiveCampaign.Query.Contact;
	using EyeSoft.ActiveCampaign.Shell.Helpers;

	using Newtonsoft.Json;

	public static class Program
	{
		public static void Main()
		{
			////var testData = ReadActiveCampaignTestData(@"P:\ActiveCampaign\ActiveCampaignTestData.json");

			using (var connection = new ActiveCampaignConnection("https://picoware.api-us1.com", "7015c381d7246c6c4de1a9ed714e9dbd7ebc37dd9296b2116240b9d7ddd6923163a47dae"))
			{
				Commanding(connection, null);

				////Query(connection, testData);
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

			contactQueryClient.GetAll().Dump();

			return;

			contactQueryClient.GetContacts(testData.ContactId).Dump();

			IAutomationQueryClient automationQueryClient = new AutomationQueryClient(connection);

			automationQueryClient.GetAll().Dump();
		}

		private static void Commanding(ActiveCampaignConnection connection, ActiveCampaignTestData testData)
        {
            var client = new ListCommandingClient(connection);

            var command = new AddListCommand("New list", "LEGALDESK", "Via Morgagni", "Verona", "IT");

            client.Add(command);

            ////var contactCommandingClient = new ContactCommandingClient(connection);

            ////         var addTagCommand = new AddTagsCommand("ing.vicentini@gmail.com", "tag1", "tag2", "tag3") ;

            ////contactCommandingClient.AddTags(addTagCommand);

            ////var removeTagsCommand = new RemoveTagsCommand("ing.vicentini@gmail.com", "tag2");

            ////contactCommandingClient.RemoveTags(removeTagsCommand);

            ////var contactQueryClient = new ContactQueryClient(connection);

            ////var contact = contactQueryClient.Get(testData.ContactEmail).Dump();

            ////contactCommandingClient.Delete(contact.Id).Dump();

            ////contactCommandingClient.Add(new AddContactCommand(testData.ContactId, testData.ContactEmail)).Dump();

            ////contact = contactQueryClient.Get(testData.ContactEmail).Dump();

            ////contactCommandingClient.Sync(new SyncContactCommand(testData.ContactEmail) { FirstName = "Bill", LastName = "White" });

            ////IAutomationCommandingClient automationCommandingClient = new AutomationCommandingClient(connection);

            ////automationCommandingClient.AddContact(testData.ContactEmail, testData.Automation).Dump();

            ////automationCommandingClient.RemoveContact(testData.ContactEmail, testData.Automation).Dump();
        }
	}
}