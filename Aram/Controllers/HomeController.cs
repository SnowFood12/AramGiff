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

			HttpContext.Session.Remove("IdLSP");

			HttpContext.Session.Remove("NameProduct");

			ViewBag.An = "display: none;";

			ViewBag.Loc = "display: none;";

			ViewBag.Active = "active";

			// Thay đổi trạng thái tất cả thành true khi vào trang index
			var NoneBck = _context.LoaiSP.ToList();
			foreach (var item in NoneBck)
			{
				item.TrangThai = true;
				_context.LoaiSP.Update(item);
				_context.SaveChanges();
			}

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

				ViewBag.Active = "active";

				// Thay đổi trạng thái tất cả thành true khi vào trang index
				var NoneBck = _context.LoaiSP.ToList();
				foreach (var item in NoneBck)
				{
					item.TrangThai = true;
					_context.LoaiSP.Update(item);
					_context.SaveChanges();
				}


				return RedirectToAction("Index", "Home");
			}
			else
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderByDescending(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "display: block;";

				ViewBag.Loc = "display: none;";

				HttpContext.Session.SetString("NameProduct", search); // ==> lọc sản phẩm tìm kiếm theo giá

				ViewBag.SapXep = "Giá: Cao đến thấp";

				ViewBag.Active = "active";

				// Thay đổi trạng thái tất cả thành true khi vào trang index
				var NoneBck = _context.LoaiSP.ToList();
				foreach (var item in NoneBck)
				{
					item.TrangThai = true;
					_context.LoaiSP.Update(item);
					_context.SaveChanges();
				}


				return View("Index");
			}
			

		}

		// lọc sản phẩm theo giá khi tìm kiếm theo tên
		public IActionResult LocSanPhamTheoGia(int id)
		{
			string search = HttpContext.Session.GetString("NameProduct");

			int idLSDP = HttpContext.Session.GetInt32("IdLSP") ?? 0;

			if ( id == 1  )
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderBy(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "display: block;";

				ViewBag.Loc = "display: none;";

				ViewBag.SapXep = "Giá: Thấp đến cao ";

				return View("Index");
			}
			else if ( id == 2 ) 
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var ListProduct = _context.SanPham.OrderByDescending(a => a.Gia).Where(a => a.Ten.Contains(search) && a.TrangThai == true).Include(a => a.CuaHang).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = ListProduct;

				ViewBag.Txt = search;

				ViewBag.An = "display: block;";

				ViewBag.Loc = "display: none;";

				ViewBag.SapXep = "Giá: Cao đến thấp";

				return View("Index");
			}
			else if ( id == 3)
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var SanPham = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == idLSDP).Include(a => a.CuaHang).OrderByDescending(a => a.Gia).ToList();


				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = SanPham;

				ViewBag.Loc = "display: block;";

				ViewBag.An = "display: none;";

				ViewBag.SapXep = "Giá: Cao đến thấp";

				return View("Index");
			}
			else
			{
				var LoaiSanPham = _context.LoaiSP.ToList();

				var SanPham = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == idLSDP).Include(a => a.CuaHang).OrderBy(a => a.Gia).ToList();

				ViewBag.LoaiSanPham = LoaiSanPham;

				ViewBag.ThongTinSanPham = SanPham;

				ViewBag.Loc = "display: block;";

				ViewBag.An = "display: none;";

				ViewBag.SapXep = "Giá: Thấp đến cao";

				return View("Index");
			}
		}

		// lọc sản phẩm theo loại sản phẩm
		public IActionResult LocSanPham (int id)
		{
            var LoaiSanPham = _context.LoaiSP.ToList();

			var SanPham = _context.SanPham.Where(a => a.TrangThai == true && a.LoaiSPId == id).Include(a => a.CuaHang).OrderByDescending(a => a.Gia).ToList();

			HttpContext.Session.SetInt32("IdLSP", id);

            ViewBag.LoaiSanPham = LoaiSanPham;

			ViewBag.ThongTinSanPham = SanPham;

			ViewBag.Loc = "display: block;";

			ViewBag.An = "display: none;";

			ViewBag.SapXep = "Giá: Cao đến thấp";

			ViewBag.Active = "";

			// Thay đổi trạng thái để hiển thị màu nền
			var Loai = _context.LoaiSP.FirstOrDefault(a => a.Id == id);

			Loai.TrangThai = false;

			_context.LoaiSP.Update(Loai);
			_context.SaveChanges();

			// thay đổi trạng thái thành true để không hiển thị màu nền
			var NoneBck = _context.LoaiSP.Where(a => a.Id != id).ToList(); 
			foreach( var item in NoneBck)
			{
				item.TrangThai = true;
				_context.LoaiSP.Update(item);
				_context.SaveChanges();
			}


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