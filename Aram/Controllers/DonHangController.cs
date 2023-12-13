using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aram.Controllers
{
	public class DonHangController : Controller
	{
		public GioHang? GioHang { get; set; }
		private readonly AramContext _context;

		public DonHangController(AramContext context)
		{
			_context = context;
		}
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

            return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string HoTen, string SoDT, string DiaChi, string GhiChu)
        {
            var donHang = new DonHang();
			donHang.TenTK = HttpContext.Session.GetString("Name");
			_context.Add(donHang);
			_context.SaveChanges();
			GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
			foreach (var item in GioHang.Lines)
			{
				var donHang_chiTiet = new DonHang_ChiTiet();
				donHang_chiTiet.SanPhamId = item.SanPham.Id;
				donHang_chiTiet.DonHangId = donHang.Id;
				donHang_chiTiet.SoLuong = item.SoLuong;
				_context.Add(donHang_chiTiet);
				_context.SaveChanges();
			}
			var TT_NH = new ThongTin_NhanHang();
			TT_NH.DonHangId = donHang.Id;
			TT_NH.HoTen = HoTen;
			TT_NH.SoDT = SoDT;
			TT_NH.DiaChi = DiaChi;
			TT_NH.GhiChu = GhiChu;
			_context.Add(TT_NH);
			_context.SaveChanges();
			return RedirectToAction("DonHangDangGiao", "GioHang");
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
