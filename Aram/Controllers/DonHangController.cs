using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace Aram.Controllers
{
	public class DonHangController : Controller
	{
		public GioHang? GioHang { get; set; }
		private readonly AramContext _context;

		public DonHangController( AramContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var ListDonHang = _context.DonHang.Where(a => a.TrangThaiDH == "Chờ duyệt" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

			var DangGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đang giao" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

			var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã giao" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

			ViewBag.ChoDuyet = ListDonHang; 

			ViewBag.DangGiao = DangGiao; 

			ViewBag.DaGiao = DaGiao;

			ViewBag.DemChoDuyet = ListDonHang.Count();
			ViewBag.DemDangGiao = DangGiao.Count();
			ViewBag.DemDaGiao = DaGiao.Count();

            return View();
		}

		// duyệt đơn
		public IActionResult DuyetDon(int id)
		{
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThaiDH = "Đang giao";

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();

            Index();
            return RedirectToAction("Index", "DonHang");
		}
        
        public IActionResult Details(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

            return View();
		}

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
            return View();
		}

		public IActionResult ChiTietDonHangDangGiao(int id)
		{
			var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude( a => a.CuaHang).ToList();

			var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault( a => a.DonHangId == id);

			var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

			ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

			ViewBag.ListProduct = DonHangChiTiet;

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
