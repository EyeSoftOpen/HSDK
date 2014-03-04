namespace EyeSoft.EntityFramework.Caching.Demo.Mvc.Controllers
{
	using System.Linq;
	using System.Web.Mvc;

	using EyeSoft.EntityFramework.Caching.Demo.Domain;
	using EyeSoft.EntityFramework.Caching.Demo.Mvc.Models;

	public class HomeController : Controller
	{
		private readonly IDbContext dataContext;

		public HomeController(IDbContext context)
		{
			dataContext = context;
		}

		public ActionResult Index()
		{
			var model = GetHomeModel(string.Empty);
			return View(model);
		}

		[HttpPost]
		public ActionResult Index(string searchTerm)
		{
			var model = GetHomeModel(searchTerm);
			return View("Index", model);
		}

		private HomeViewModel GetHomeModel(string searchTerm)
		{
			var query = dataContext.Table<Customer>();
			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(c => c.ContactName.Contains(searchTerm));
			}

			var customerList =
				query
					.OrderBy(c => c.ContactName)
					.Select(c => new CustomerModel { Id = c.CustomerId, Name = c.ContactName })
					.ToList();

			var model = new HomeViewModel(
				customerList,
				MvcApplication.CacheWithStatistics.Hits,
				MvcApplication.CacheWithStatistics.Misses,
				MvcApplication.CacheWithStatistics.ItemAdds);

			return model;
		}
	}
}