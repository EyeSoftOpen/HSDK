namespace EyeSoft.Windows.Model.Demo.Contract
{
	using System;

	public class CustomerDto
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public decimal Turnover { get; set; }

		public int Visits { get; set; }

		public DateTime? Visited { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is CustomerDto other))
			{
				return false;
			}

			return Id.Equals(other.Id);
		}
	}
}