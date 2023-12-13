using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aram.Data;
using Aram.Models;
using System.Text.RegularExpressions;
using System.Collections;
namespace Aram.Controllers
{
    public class CuaHangController : Controller
    {
        private readonly AramContext _context;

        public CuaHangController(AramContext context)
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

        // GET: CuaHangs
        public async Task<IActionResult> Index()
        {
            PhanQuyen();
              return _context.CuaHang != null ? 
                          View(await _context.CuaHang.Where(x => x.TrangThai == true).ToListAsync()) :
                          Problem("Entity set 'AramContext.CuaHang'  is null.");
        }

        // tìm kiếm thông tin cửa hàng
        public async Task<IActionResult> SearchShop(string search)
        {
            PhanQuyen();
            if (!string.IsNullOrEmpty(search))
            {
                return _context.CuaHang != null ?
                       View("Index", await _context.CuaHang
                       .Where(a => a.Ten.Contains(search) 
                       && a.TrangThai == true 
                       || a.SoDT.Contains(search))
                       .ToListAsync()): 
                       Problem("Entity set 'AramContext.CuaHang'  is null.");
            }
            else
            {
                var Shop = _context.CuaHang.Where(a => a.TrangThai == true);
                return View("Index", await Shop.ToListAsync());
            }

        }

        // GET: CuaHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PhanQuyen();
            if (id == null || _context.CuaHang == null)
            {
                return NotFound();
            }

            var cuaHang = await _context.CuaHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuaHang == null)
            {
                return NotFound();
            }

            return View(cuaHang);
        }

        // GET: CuaHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuaHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CuaHang cuaHang)
        {
			if (cuaHang.Ten != null)
            {
                cuaHang.Ten = Regex.Replace(cuaHang.Ten.Trim(), @"\s+", " ");
            }
			if (cuaHang.DiaChi != null)
			{
				cuaHang.DiaChi = Regex.Replace(cuaHang.DiaChi.Trim(), @"\s+", " ");
			}
			var ktDT = _context.CuaHang.FirstOrDefault(x => x.SoDT == cuaHang.SoDT);
            if (ktDT != null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại đã được sử dụng");
            }
            
            //hết kiểm lỗi
            string tenTK = HttpContext.Session.GetString("Name");
			cuaHang.NgayTaoCuaHang = DateTime.Now;
			cuaHang.TenTK = tenTK;
			if (ModelState.IsValid)
            {
                _context.Add(cuaHang);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Thêm thông tin cửa hàng thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(cuaHang);
        }

        // GET: CuaHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            PhanQuyen();
            if (id == null || _context.CuaHang == null)
            {
                return NotFound();
            }

            var cuaHang = await _context.CuaHang.FindAsync(id);
            if (cuaHang == null)
            {
                return NotFound();
            }
            return View(cuaHang);
        }

        // POST: CuaHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CuaHang cuaHang)
        {
            

            //kiểm lỗi cửa hàng
/*            Regex kuTuDacBiet = new Regex("^[A-Za-zÀ-ỹĐđĂăÂâÁáÀàẢảẠạẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẾếỀềỂểỄễỆệÊêÍíÌìỈỉỊịỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợÚúÙùỦủỤụỨứỪừỬửỮữỰựỶỷỴỵÝý\\s0-9]+$");
*/           if(cuaHang.Ten != null)
            {
                cuaHang.Ten = Regex.Replace(cuaHang.Ten.Trim(), @"\s+", " ");
            }
			if (cuaHang.DiaChi != null)
			{

				cuaHang.DiaChi = Regex.Replace(cuaHang.DiaChi.Trim(), @"\s+", " ");
			}
			var ktDT = _context.CuaHang.Where(x => x.Id != cuaHang.Id).FirstOrDefault(x => x.SoDT == cuaHang.SoDT);
            if (ktDT != null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại đã được sử dụng");
            }
			//kiểm lỗi địa chỉ
			
			//hết kiểm lỗi
			cuaHang.TenTK = HttpContext.Session.GetString("Name");
			if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(cuaHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuaHangExists(cuaHang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Cập nhật thông tin của hàng thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(cuaHang);
        }

        // GET: CuaHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            PhanQuyen();
            if (id == null || _context.CuaHang == null)
            {
                return NotFound();
            }

            var cuaHang = await _context.CuaHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuaHang == null)
            {
                return NotFound();
            }

            return View(cuaHang);
        }

        // POST: CuaHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PhanQuyen();
            if (_context.CuaHang == null)
            {
                return Problem("Entity set 'AramContext.CuaHang'  is null.");
            }
            var cuaHang = await _context.CuaHang.FindAsync(id);
            if (cuaHang != null)
            {
                _context.CuaHang.Remove(cuaHang);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đổi trạng thái cửa hàng thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool CuaHangExists(int id)
        {
            PhanQuyen();
            return (_context.CuaHang?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
