using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text.Json.Nodes;

namespace Aram.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AramContext _context;

        public GioHangController(AramContext context)
        {
            _context = context;
        }
		public GioHang? GioHang { get; set; }

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

			GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
			/*ViewBag.TamTinh = (int)gioHangs.Sum(p => p.SanPham.Gia * p.SoLuong);*/
			return View(GioHang);
        }
		public IActionResult AddToGioHang(int sanPhamId)
		{
			SanPham? sanPham = _context.SanPham.FirstOrDefault(s => s.Id == sanPhamId);
			if (sanPham != null)
			{
				GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
				GioHang.AddItem(sanPham, 1);
				HttpContext.Session.SetJson("giohang", GioHang);
			}
			return View("Index", GioHang);
		}
		//=======================================================

		// ===> tăng số lượng 
		[HttpGet]
		public JsonResult TangSoLuong(int id)
		{
			var GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
			var GioHang_line = GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == id);
			GioHang_line.SoLuong++;
			HttpContext.Session.SetJson("giohang", GioHang);

			var DonGia = GioHang_line.SanPham.Gia;

			var json = new
			{
                SoLuong = GioHang_line.SoLuong,
                TongTienSanPham = GioHang_line.TongTienSp(),
                TamTinh = GioHang.TamTinh(),
                TongTien = GioHang.TongTien(),
            };

			return Json(json);

		}

		// giảm số lượng
		[HttpGet]
		public JsonResult GiamSoLuong(int id)
		{
			GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
			var GioHang_line = GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == id);
			GioHang_line.SoLuong--;
			HttpContext.Session.SetJson("giohang", GioHang);

            var DonGia = _context.SanPham.FirstOrDefault( a => a.Id == id);
            var json = new
			{
                SoLuong = GioHang_line.SoLuong,
                TongTienSanPham = GioHang_line.TongTienSp(),
                TamTinh = GioHang.TamTinh(),
                TongTien = GioHang.TongTien(),
            };

			return Json(json);

		}
		public IActionResult XoaSPGioHang(int Id)
		{
            GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
            GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == Id);
            if (GioHang.Lines != null)
			{
				GioHang.RemoveSanPham(Id);
                HttpContext.Session.SetJson("giohang", GioHang);
            }
			return RedirectToAction(nameof(Index), GioHang);
		}
		// đơn hàng đang chờ admin duyệt
		public IActionResult GioHangDangChoDuyet()
		{
			PhanQuyen();

			return View();
		}

		// chi tiết đơn hàng đang chở duyệt

		public IActionResult ChiTietGioHangDangChoDuyet()
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
        // Chi tiết đơn hàng đang giao
        public IActionResult ChiTietDonHangDangGiao()
        {
            PhanQuyen();

            return View();
        }


        // đơn hàng đã nhận 
        public IActionResult DonHangDaNhan()
        {
            PhanQuyen();

            return View();
        }

        // Chi tiết đơn hàng đã nhận
        public IActionResult ChiTietDonHangDaNhan()
        {
            PhanQuyen();

            return View();
        }

    }
}
