using Aram.Data;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aram.Components
{
    public class GiohangWidget : ViewComponent
    {
        private readonly AramContext _context;

        public GiohangWidget(AramContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var tenTK = HttpContext.Session.GetString("Name");
            var gioHang = _context.GioHang.Where(p => p.TenTK == tenTK).FirstOrDefault();
            int slItem = 0;
            if (tenTK != null)
            {
                if (gioHang == null)
                {
                    gioHang = new GioHang { TenTK = tenTK };
                    _context.GioHang.Add(gioHang);
                    _context.SaveChanges();
                }
                var gioHang_chiTiet = _context.GioHang_ChiTiet
                    .Include(p => p.SanPham)
                    .Where(p => p.GioHangId == gioHang.Id).ToList();
                slItem = gioHang_chiTiet.Count();

            }
            return View("Default", slItem);
        }
    }
}
