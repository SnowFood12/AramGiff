using Aram.Data;
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

            
            /*ViewBag.TamTinh = (int)gioHangs.Sum(p => p.SanPham.Gia * p.SoLuong);*/
            return View();
        }
		
        //=======================================================

 /*       // ===> tăng số lượng 
        [HttpGet]
        public JsonResult TangSoLuong(int id)
        {
           *//* var DonHang = _context.GioHang_ChiTiet.FirstOrDefault(a => a.Id == id);

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
			};*//*

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
        public IActionResult XoaSPGioHang(int Id)
        {
            var DonHan_CT = _context.GioHang_ChiTiet.FirstOrDefault(a => a.Id == Id);
            if(DonHan_CT != null)
            {
                _context.GioHang_ChiTiet.Remove(DonHan_CT);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
		}
            // đơn hàng đang chờ admin duyệt
            public IActionResult GioHangDangChoDuyet()
		{
            PhanQuyen();

            return View();
		}*/

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
