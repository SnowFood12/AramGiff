using Aram.Data;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aram.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AramContext _context;

        public GioHangController(AramContext context)
        {
            _context = context;
        }
		
		public IActionResult Index()
        {
			var tenTK = HttpContext.Session.GetString("Name");
			if (tenTK == null)
			{
				return RedirectToAction("DangNhap", "TaiKhoan");
			}
			var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
			if (gioHang == null)
			{
				return NotFound();
			}

			var gioHang_ChiTiet = _context.GioHang_ChiTiet.Include(p => p.SanPham).Where(p => p.GioHangId == gioHang.Id).ToList();

            return View(gioHang_ChiTiet);
        }
        public IActionResult AddToGioHang(int Id)
        {
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

		//=======================================================
		// đơn hàng đang chờ admin duyệt
		public IActionResult GioHangDangChoDuyet()
		{
			return View();
		}

		// chi tiết đơn hàng đang chở duyệt

		public IActionResult ChiTietGioHangDangChoDuyet()
		{
			return View();
		}

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
			return View();
		}
        // Chi tiết đơn hàng đang giao
        public IActionResult ChiTietDonHangDangGiao()
        {
            return View();
        }


        // đơn hàng đã nhận 
        public IActionResult DonHangDaNhan()
        {
            return View();
        }

        // Chi tiết đơn hàng đã nhận
        public IActionResult ChiTietDonHangDaNhan()
        {
            return View();
        }

    }
}
