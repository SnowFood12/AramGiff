using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aram.Components
{
    public class GiohangWidget : ViewComponent
    {
        private readonly AramContext _context;
		public GioHang? GioHang { get; set; }
		public GiohangWidget(AramContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {

			GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
			return View("Default", GioHang);
        }
    }
}
