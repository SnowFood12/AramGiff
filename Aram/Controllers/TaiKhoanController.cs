using Microsoft.AspNetCore.Mvc;

namespace Aram.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangKy()
        {
            return View();
        }
    }
}
