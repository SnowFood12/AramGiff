using Aram.Data;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
			var TongTinSanPham = _context.SanPham.Select(a => new
			{
				a.Id,
				a.Ten, 
				a.PicData, 
				a.TrangThai,
				a.Gia
			}); 
			ViewBag.ThongTinSanPham = TongTinSanPham;
			return View();
		}

		public IActionResult MainHome() // trang chủ
		{
			return View();
		}

		public IActionResult Product( int id)
		{
			var ThongTinSanPhamId = _context.SanPham.FirstOrDefault( a => a.Id == id );

			var SanPhamInShop = _context.SanPham.Where(a => a.CuaHangId == ThongTinSanPhamId.CuaHangId);

			var Shop = _context.CuaHang.FirstOrDefault(a => a.Id == ThongTinSanPhamId.CuaHangId);

			ViewBag.ThongTinSanPham = SanPhamInShop;

			ViewBag.ThongTinSanPhamId = ThongTinSanPhamId;

			ViewBag.Shop = Shop;

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