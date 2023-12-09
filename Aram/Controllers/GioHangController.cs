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
			var gioHang = _context.GioHang.Where(p => p.TenTK == "admin").FirstOrDefault();
            var gioHang_ChiTiet = _context.GioHang_ChiTiet.Include(p => p.SanPham).Where(p => p.GioHangId == gioHang.Id).ToList();
            return View(gioHang_ChiTiet);
        }
        public IActionResult AddToGioHang(int Id)
        {
            int soLuong = 1;
			GioHang gioHang = _context.GioHang.Where(p => p.TenTK == "admin").FirstOrDefault();
			GioHang_ChiTiet gioHang_ChiTiet = _context.GioHang_ChiTiet.Where(p => p.GioHangId == gioHang.Id &&  p.SanPhamId == Id).FirstOrDefault();
			if (gioHang == null)
            {
                gioHang = new GioHang { TenTK = "admin" };
                _context.GioHang.Add(gioHang);
				_context.SaveChanges();
			}
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
    }
}
