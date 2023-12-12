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

           
            return View("Default");
        }
    }
}
