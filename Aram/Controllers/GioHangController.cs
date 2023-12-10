﻿using Aram.Data;
using Aram.Models;
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

            var tenTK = HttpContext.Session.GetString("Name");
			if (tenTK == null)
			{
				return RedirectToAction("DangNhap", "TaiKhoan");
			}
			var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
			if (gioHang == null)
			{
                gioHang = new GioHang { TenTK = tenTK };
                _context.GioHang.Add(gioHang);
                _context.SaveChanges();
            }

			GioHang gioHangs = _context.GioHang.Include(p => p.GioHang_ChiTiets)
				.ThenInclude(p => p.SanPham)
				.Where(p => p.TenTK == tenTK)
				.FirstOrDefault();
            /*ViewBag.TamTinh = (int)gioHangs.Sum(p => p.SanPham.Gia * p.SoLuong);*/
            return View(gioHangs);
        }
        public IActionResult AddToGioHang(int Id)
        {
            PhanQuyen();

            var tenTK = HttpContext.Session.GetString("Name");
			int soLuong = 1;
			GioHang gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
			if (gioHang == null)
			{

				gioHang = new GioHang { TenTK = tenTK };
				_context.GioHang.Add(gioHang);
				_context.SaveChanges();
			}
			GioHang_ChiTiet gioHang_ChiTiet = _context.GioHang_ChiTiet.Where(p => p.GioHangId == gioHang.Id &&  p.SanPhamId == Id).FirstOrDefault();
			
			//
			if (gioHang_ChiTiet == null)
            {
                gioHang_ChiTiet = new GioHang_ChiTiet { GioHangId = gioHang.Id, SanPhamId = Id, SoLuong = soLuong };
				_context.GioHang_ChiTiet.Add(gioHang_ChiTiet);
			}
            else
            {
                gioHang_ChiTiet.SoLuong += soLuong;
				_context.GioHang_ChiTiet.Update(gioHang_ChiTiet);
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public int TongTien()
		{
			var tenTK = HttpContext.Session.GetString("Name");
            var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
            var gioHang_ChiTiet = _context.GioHang_ChiTiet.Include(p => p.SanPham).Where(p => p.GioHangId == gioHang.Id).ToList();
			return (int)gioHang_ChiTiet.Sum(p => p.SanPham.Gia * p.SoLuong);
        }
        //=======================================================

        // ===> tăng số lượng 
        [HttpGet]
        public JsonResult TangSoLuong(int id)
        {
            var DonHang = _context.GioHang_ChiTiet.FirstOrDefault(a => a.Id == id);

            int soluong = DonHang.SoLuong;

            var SanPham = _context.SanPham.FirstOrDefault(a => a.Id == DonHang.SanPhamId);

            int TongTien = (soluong + 1) * (int)SanPham.Gia;

            DonHang.SoLuong = soluong + 1;
            _context.GioHang_ChiTiet.Update(DonHang);
            _context.SaveChanges();

            // tính tổng tiền giỏ hàng
			var tenTK = HttpContext.Session.GetString("Name");
			var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
			var gioHang_ChiTiet = _context.GioHang_ChiTiet.Include(p => p.SanPham).Where(p => p.GioHangId == gioHang.Id).ToList();
            // tạm tính
			int TamTinh = (int)gioHang_ChiTiet.Sum(p => p.SanPham.Gia * p.SoLuong);


			var json = new
			{
				TongTien = TongTien,
				TamTinh = TamTinh
			};

			return Json(json);
            
        }

        // giảm số lượng
		[HttpGet]
		public JsonResult GiamSoLuong(int id)
		{
			var DonHang = _context.GioHang_ChiTiet.FirstOrDefault(a => a.Id == id);
			int soluong = DonHang.SoLuong;

			var SanPham = _context.SanPham.FirstOrDefault(a => a.Id == DonHang.SanPhamId);

			int TongTien = (soluong - 1) * (int)SanPham.Gia;

			DonHang.SoLuong = soluong - 1;
			_context.GioHang_ChiTiet.Update(DonHang);
			_context.SaveChanges();

			// tính tổng tiền giỏ hàng
			var tenTK = HttpContext.Session.GetString("Name");
			var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
			var gioHang_ChiTiet = _context.GioHang_ChiTiet.Include(p => p.SanPham).Where(p => p.GioHangId == gioHang.Id).ToList();
			// tạm tính
			int TamTinh = (int)gioHang_ChiTiet.Sum(p => p.SanPham.Gia * p.SoLuong);


			var json = new
			{
				TongTien = TongTien,
				TamTinh = TamTinh
			};

			return Json(json);

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
