namespace EyeSoft.Windows.Model.Demo.Configuration.Helpers
{
	using System;
	using System.Linq;
    using EyeSoft.AutoMapper;
    using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels;

	public static class Known
	{
		public static class CustomerModel
		{
			public static readonly IQueryable<CustomerViewModel> All =
				new AutoMapperProjection().Project<CustomerViewModel>(Customer.All);

		    public static readonly CustomerViewModel Main = new AutoMapperMapper().Map<CustomerViewModel>(Customer.Main);
		}

		public static class Customer
		{
			public static readonly CustomerDto Bill =
				new CustomerDto
					{
						Id = new Guid("bf5a532a-ed98-4348-b5cc-839d86fc8ad0"),
						FirstName = "Bill",
						LastName = "White",
						Turnover = 1000
					};

			public static readonly CustomerDto Steve =
				new CustomerDto
					{
						Id = new Guid("f6a4b487-7fdb-4994-bb43-2eb6eff425eb"),
						FirstName = "Steve",
						LastName = "Red",
						Turnover = 500
					};

			public static readonly CustomerDto James =
				new CustomerDto
					{
						Id = new Guid("e1444c35-3857-4af5-86b8-16c45a55ca61"),
						FirstName = "James",
						LastName = "Black",
						Turnover = 1200
					};

			public static readonly CustomerDto Main = Bill;

			public static readonly IQueryable<CustomerDto> All =
				new[] { Bill, Steve, James }.OrderBy(x => x.FirstName).AsQueryable();
		}
	}
}