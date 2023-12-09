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

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
			return View();
		}
		public IActionResult ChiTietDonHangDangGiao()
		{
			return View();
		}

		// đơn hàng đã giao
		public IActionResult DonHangDaGiao()
		{
			return View();
		}
		public IActionResult ChiTietDonHangDaGiao()
		{
			return View();
		}
	}
}
