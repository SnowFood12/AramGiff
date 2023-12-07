using Microsoft.AspNetCore.Mvc;

namespace Aram.Controllers
{
	public class DonHangController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Details()
		{
			return View();
		}
	}
}
