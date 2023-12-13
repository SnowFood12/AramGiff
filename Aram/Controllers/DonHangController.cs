﻿using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult ChiTietDonHangDangGiao( int id)
		{
			var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).ToList();



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
