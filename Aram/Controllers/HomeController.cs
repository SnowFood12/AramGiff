using Aram.Data;
using Aram.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Linq;

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
		public IActionResult Index()
		{
            var LoaiSanPham = _context.LoaiSP.ToList();

			var TongTinSanPham = _context.SanPham.Where( a => a.TrangThai == true).Include(a => a.CuaHang).OrderByDescending(a => a.Id).ToList();

			ViewBag.LoaiSanPham = LoaiSanPham;
			ViewBag.ThongTinSanPham = TongTinSanPham;

			ViewBag.An = "visibility: hidden;";

			return View();
		}

		// gợi ý tìm kiếm
		public JsonResult Search( string search)
		{
			var ListProduct = _context.SanPham.Where( a => a.Ten.Contains(search) && a.TrangThai == true).ToList();

			return Json(ListProduct);
		}

		// tìm kiếm sản phẩm theo tên
		public IActionResult SearchName(string search)
		{
			if (search == null)
			{
				Index();
				ViewBag.SapXep = "Giá: Cao đến thấp";

				HttpContext.Session.Remove("NameProduct"); // xoá đi khi tìm kiếm == null

				return RedirectToAction("Index", "Home");
			}
			else
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderByDescending(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "visibility: visible;";

				HttpContext.Session.SetString("NameProduct", search); // ==> lọc sản phẩm tìm kiếm theo giá

				ViewBag.SapXep = "Giá: Cao đến thấp";

				return View("Index");
			}
			

		}
		// lọc sản phẩm theo giá
		public IActionResult LocSanPhamTheoGia(int id)
		{
			string search = HttpContext.Session.GetString("NameProduct"); 

			if ( id == 1  )
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderBy(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "visibility: visible;";

				ViewBag.SapXep = "Giá: Thấp đến cao ";

				return View("Index");
			}
			else
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderByDescending(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "visibility: visible;";

				ViewBag.SapXep = "Giá: Cao đến thấp";

				return View("Index");
			}
		}

		// lọc sản phẩm theo loại sản phẩm
		public IActionResult LocSanPham (int id)
		{
            var LoaiSanPham = _context.LoaiSP.ToList();

			var SanPham = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == id).Include(a => a.CuaHang).OrderByDescending(a => a.Id).ToList();

            ViewBag.LoaiSanPham = LoaiSanPham;

			ViewBag.ThongTinSanPham = SanPham;

			return View("Index");
		}



        public IActionResult MainHome() // trang chủ
		{

			var ListProduct = _context.SanPham.Where(a => a.TrangThai == true)
							.Join(_context.DonHang_ChiTiet, a => a.Id, dh => dh.SanPhamId, (a, dh) => new
							{
								a.Ten, 
								a.PicData, 
								a.CuaHang, 
								a.Gia, 
								a.Id,  
								a.LoaiSPId, 
								dh.SoLuong
							}).OrderByDescending( a => a.SoLuong).ToList();

			var List = ListProduct.DistinctBy(a => a.Id).Take(8).ToList();


			var LastProduct = _context.SanPham.Where( a => a.TrangThai == true).Include(a => a.CuaHang).OrderBy( a => a.Id).Last();


            ViewBag.LastProduct = LastProduct;

			ViewBag.ListProduct = List;

            return View();
		}

		public IActionResult Product( int id)
		{

            var ThongTinSanPhamId = _context.SanPham.Where( a => a.Id == id).Include( a => a.CuaHang).FirstOrDefault() ;

			var SanPhamInShop = _context.SanPham.Where(a => a.TrangThai == true && a.Ten.Contains(ThongTinSanPhamId.Ten) || a.LoaiSPId == ThongTinSanPhamId.LoaiSPId).Include(a => a.CuaHang).OrderByDescending(a => a.Gia).Take(16).ToList();

            var Shop = _context.CuaHang.FirstOrDefault(a => a.Id  == ThongTinSanPhamId.CuaHangId);

			var ListShop = _context.SanPham.Where(a => a.Ten.Contains(ThongTinSanPhamId.Ten) && a.CuaHang.TrangThai == true).Include(a => a.CuaHang).ToList();

			ViewBag.ThongTinSanPham = SanPhamInShop;

			ViewBag.ThongTinSanPhamId = ThongTinSanPhamId;

			ViewBag.Shop = Shop;

			ViewBag.ListShop = ListShop;

			return View();
		}

        public IActionResult Shop(int id)
        {
            var Shop = _context.CuaHang.FirstOrDefault(a => a.Id == id);

            var ListProduct = _context.SanPham.Where(a => a.TrangThai == true && a.CuaHangId == id).ToList();

            ViewBag.Shop = Shop;

            ViewBag.ListProduct = ListProduct;

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