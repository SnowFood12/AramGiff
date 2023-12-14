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

			var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã giao" || a.TrangThai == false).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

			ViewBag.ChoDuyet = ListDonHang; 

			ViewBag.DangGiao = DangGiao; 

			ViewBag.DaGiao = DaGiao;

			ViewBag.DemChoDuyet = ListDonHang.Count();
			ViewBag.DemDangGiao = DangGiao.Count();
			ViewBag.DemDaGiao = DaGiao.Count();

			ViewBag.TinhTrang = "Tất cả";

            return View();
		}

		// lọc đơn hàng
		// đơn hàng huỷ
		public IActionResult HuyDon()
		{
            var ListDonHang = _context.DonHang.Where(a => a.TrangThaiDH == "Chờ duyệt" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            var DangGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đang giao" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            var DaGiao = _context.DonHang.Where(a => a.TrangThai == false).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            ViewBag.ChoDuyet = ListDonHang;

            ViewBag.DangGiao = DangGiao;

            ViewBag.DaGiao = DaGiao;

            ViewBag.DemChoDuyet = ListDonHang.Count();
            ViewBag.DemDangGiao = DangGiao.Count();
            ViewBag.DemDaGiao = DaGiao.Count();

            ViewBag.TinhTrang = "Đã huỷ";

            return View("Index");
		}

		// đơn hàng giao thành công
		public IActionResult GiaoThanhCong()
		{
            var ListDonHang = _context.DonHang.Where(a => a.TrangThaiDH == "Chờ duyệt" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            var DangGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đang giao" && a.TrangThai == true).Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã Giao").Include(a => a.DonHang_ChiTiets).Include(a => a.ThongTin_NhanHangs).ToList();

            ViewBag.ChoDuyet = ListDonHang;

            ViewBag.DangGiao = DangGiao;

            ViewBag.DaGiao = DaGiao;

            ViewBag.DemChoDuyet = ListDonHang.Count();
            ViewBag.DemDangGiao = DangGiao.Count();
            ViewBag.DemDaGiao = DaGiao.Count();

            ViewBag.TinhTrang = "Đã giao";

            return View("Index");
        }
        //==============================================================================================


        // duyệt đơn
        public IActionResult DuyetDon(int id)
		{
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThaiDH = "Đang giao";

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();
            TempData["Message"] = "Duyệt đơn hàng thành công!!";
            Index();
            return RedirectToAction("Index", "DonHang");
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


        public IActionResult Details(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.IdDonHang = id;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

            return View();
		}

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
            return View();
		}

		// Đơn hàng đã giao
		public IActionResult DaGiao(int id)
		{
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThaiDH = "Đã giao";

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();

            Index();
            TempData["Message"] = "Giiao hàng thành công";

            return RedirectToAction("Index", "DonHang");
		}

		public IActionResult XacNhanHuy(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

			

            return View();
		}

		// huỷ đơn hàng
        public IActionResult HuyDonHang(int id)
        {
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThai = false;

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();
			TempData["Message"] = "Huỷ đơn hàng thành công";
            Index();
            return RedirectToAction("Index", "DonHang");
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
		public IActionResult ChiTietDonHangDaGiao(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

            return View();
        }
	}
}
