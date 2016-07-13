namespace EyeSoft.ActiveCampaign.Query.Automation.Models
{
	public class Automation
	{
		public Automation(int id, string name, AutomationStatus status)
		{
			Id = id;
			Name = name;
			Status = status;
		}

		public int Id { get; }
		public string Name { get; }
		public AutomationStatus Status { get; }
	}
}