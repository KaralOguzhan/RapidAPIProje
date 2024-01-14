using Microsoft.AspNetCore.Mvc;

namespace MyAkademiRapidApi.Controllers
{
	public class AdminLayoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
