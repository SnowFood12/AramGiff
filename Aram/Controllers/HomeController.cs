using Aram.Data;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;

namespace Aram.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AramContext _context;
 

		public HomeController(ILogger<HomeController> logger, AramContext context)
		{
			_logger = logger;
            _context = context;
        }

		public void PhanQuyen()
		{
            string Name = HttpContext.Session.GetString("Name");
			if ( Name == "admin1234" && Name != null) 
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
            var LoaiSanPham = _context.LoaiSP.ToList();

			var TongTinSanPham = _context.SanPham.Where( a => a.TrangThai == true).Include(a => a.CuaHang).OrderByDescending(a => a.Id).ToList();

			ViewBag.LoaiSanPham = LoaiSanPham;
			ViewBag.ThongTinSanPham = TongTinSanPham;
			return View();
		}

		// gợi ý tìm kiếm
		public JsonResult Search( string search)
		{
			var ListProduct = _context.SanPham.Where( a => a.Ten.Contains(search)).ToList();

			return Json(ListProduct);
		}

		// tìm kiếm sản phẩm theo tên
		public IActionResult SearchName(string search)
		{
			PhanQuyen();
			if ( search == null)
			{
				Index();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include( a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				return View("Index");
			}
		}

		// lọc sản phẩm 
		public IActionResult LocSanPham (int id)
		{
            PhanQuyen();
            var LoaiSanPham = _context.LoaiSP.ToList();

			var SanPham = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == id).Include(a => a.CuaHang).OrderByDescending(a => a.Id).ToList();

            ViewBag.LoaiSanPham = LoaiSanPham;

			ViewBag.ThongTinSanPham = SanPham;

			return View("Index");
		}


		public IActionResult MainHome() // trang chủ
		{
			PhanQuyen();

            var ListProduct = _context.SanPham.Where(a => a.TrangThai == true).Include(a => a.CuaHang).OrderByDescending(a => a.Id).Take(8).ToList();

			var LastProduct = _context.SanPham.Where( a => a.TrangThai == true).Include(a => a.CuaHang).OrderBy( a => a.Id).Last();


            ViewBag.LastProduct = LastProduct;

			ViewBag.ListProduct = ListProduct;

            return View();
		}

		public IActionResult Product( int id)
		{
            PhanQuyen();

            var ThongTinSanPhamId = _context.SanPham.Where( a => a.Id == id).Include( a => a.CuaHang).FirstOrDefault() ;

			var SanPhamInShop = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == ThongTinSanPhamId.LoaiSPId).Include(a => a.CuaHang).OrderByDescending(a => a.Id).Take(16).ToList();

            var Shop = _context.CuaHang.FirstOrDefault(a => a.Id == ThongTinSanPhamId.CuaHangId);

			var ListShop = _context.SanPham.Where(a => a.Ten.Contains(ThongTinSanPhamId.Ten) && a.CuaHang.TrangThai == true).Include(a => a.CuaHang).ToList();

			ViewBag.ThongTinSanPham = SanPhamInShop;

			ViewBag.ThongTinSanPhamId = ThongTinSanPhamId;

			ViewBag.Shop = Shop;

			ViewBag.ListShop = ListShop;

			return View();
		}
		public IActionResult Invoice()
		{
			return View();
		}
        public IActionResult ListInvoice()
        {
            return View();
        }

		public IActionResult Shop(int id)
		{
			var Shop = _context.CuaHang.FirstOrDefault( a => a.Id == id);

			var ListProduct = _context.SanPham.Where( a => a.TrangThai == true && a.CuaHangId == id).ToList();

			ViewBag.Shop = Shop;	

			ViewBag.ListProduct = ListProduct;	

			return View();
		}

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}