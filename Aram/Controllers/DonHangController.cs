using Microsoft.AspNetCore.Mvc;

namespace Aram.Controllers
{
	public class DonHangController : Controller
	{
        public void PhanQuyen()
        {
            string Name = HttpContext.Session.GetString("Name");
            if (Name == "admin1234" && Name != null)
            {
                ViewBag.PhanQuyen = true;
            }
            else
            {
                ViewBag.PhanQuyen = false;
            }
        }
        public IActionResult Index()
		{
			PhanQuyen();

            return View();
		}
		public IActionResult Details()
		{
            PhanQuyen();
            return View();
		}

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
            PhanQuyen();
            return View();
		}
		public IActionResult ChiTietDonHangDangGiao()
		{
            PhanQuyen();
            return View();
		}

		// đơn hàng đã giao
		public IActionResult DonHangDaGiao()
		{
            PhanQuyen();
            return View();
		}
		public IActionResult ChiTietDonHangDaGiao()
		{
			PhanQuyen();
			return View();
		}
	}
}
