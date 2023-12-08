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

namespace Aram.Controllers
{
    public class CuaHangController : Controller
    {
        private readonly AramContext _context;

        public CuaHangController(AramContext context)
        {
            _context = context;
        }

        // GET: CuaHangs
        public async Task<IActionResult> Index()
        {
              return _context.CuaHang != null ? 
                          View(await _context.CuaHang.Where(x => x.TrangThai == true).ToListAsync()) :
                          Problem("Entity set 'AramContext.CuaHang'  is null.");
        }

        // GET: CuaHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            if(cuaHang.Ten != null)
            {
                cuaHang.Ten = Regex.Replace(cuaHang.Ten.Trim(), @"\s+", " ");

            }

            //kiểm lỗi cửa hàng
            Regex kuTuDacBiet = new Regex("^[A-Za-zÀ-ỹĐđĂăÂâÁáÀàẢảẠạẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẾếỀềỂểỄễỆệÊêÍíÌìỈỉỊịỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợÚúÙùỦủỤụỨứỪừỬửỮữỰựỶỷỴỵÝý\\s0-9]+$");
            if (cuaHang.Ten == null)
            {
                ModelState.AddModelError("Ten", "Tên cửa hàng không được để trống");
            }
            else if (cuaHang.Ten.Length > 50)
            {
                ModelState.AddModelError("Ten", "Tên cửa hàng không được dài quá 50 kí tự");
            } else if (!kuTuDacBiet.IsMatch(cuaHang.Ten))
            {
                ModelState.AddModelError("Ten", "Tên cửa hàng không được chứa ký tự đặc biệt");
            }
            //kiểm lỗi số điện thoại
            Regex KTsoDT = new Regex(@"^0[0-9]{9}$");
            var ktDT = _context.CuaHang.FirstOrDefault(x => x.SoDT == cuaHang.SoDT);
            if (cuaHang.SoDT == null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại không được để trống");
            } else if (!KTsoDT.IsMatch(cuaHang.SoDT))
            {
                ModelState.AddModelError("SoDT", "Số điện thoại không hợp lệ");
            } else if(ktDT != null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại đã được sử dụng");
            }
            if(cuaHang.DiaChi != null)
            {

                cuaHang.DiaChi = Regex.Replace(cuaHang.DiaChi.Trim(), @"\s+", " ");
            }
                //kiểm lỗi địa chỉ
            if (cuaHang.DiaChi == null)
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được để trống");
            }
            else if (cuaHang.DiaChi.Length > 200)
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được quá 200 ký tự");
            } else if(!kuTuDacBiet.IsMatch(cuaHang.DiaChi))
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được chứa ký tự đặc biệt");
            }
            //hết kiểm lỗi

             if (ModelState.IsValid)
            {
                cuaHang.NgayTaoCuaHang = DateTime.Now;
                cuaHang.TenTK = "admin";
                _context.Add(cuaHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuaHang);
        }

        // GET: CuaHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
            if (id != cuaHang.Id)
            {
                return NotFound();
            }

            //kiểm lỗi cửa hàng
            Regex kuTuDacBiet = new Regex("^[A-Za-zÀ-ỹĐđĂăÂâÁáÀàẢảẠạẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẾếỀềỂểỄễỆệÊêÍíÌìỈỉỊịỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợÚúÙùỦủỤụỨứỪừỬửỮữỰựỶỷỴỵÝý\\s0-9]+$");
            if(cuaHang.Ten != null)
            {
                cuaHang.Ten = Regex.Replace(cuaHang.Ten.Trim(), @"\s+", " ");
            }
            if (cuaHang.DiaChi != null)
            {
                cuaHang.DiaChi = Regex.Replace(cuaHang.DiaChi.Trim(), @"\s+", " ");
            }
            
            if (cuaHang.Ten == null)
            {
                ModelState.AddModelError("Ten", "Tên cửa hàng không được để trống");
            }
            else if (cuaHang.Ten.Length > 50)
            {

                ModelState.AddModelError("Ten", "Tên cửa hàng không được dài quá 50 kí tự");
            }
            else if (!kuTuDacBiet.IsMatch(cuaHang.Ten))
            {
                ModelState.AddModelError("Ten", "Tên cửa hàng không được chứa ký tự đặc biệt");
            }
            //kiểm lỗi số điện thoại
            Regex KTsoDT = new Regex(@"^0[0-9]{9}$");
            var ktDT = _context.CuaHang.FirstOrDefault(x => x.SoDT == cuaHang.SoDT);
            if (cuaHang.SoDT == null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại không được để trống");
            }
            else if (!KTsoDT.IsMatch(cuaHang.SoDT))
            {
                ModelState.AddModelError("SoDT", "Số điện thoại không hợp lệ");
            }
            else if (ktDT != null)
            {
                ModelState.AddModelError("SoDT", "Số điện thoại đã được sử dụng");
            }
            //kiểm lỗi địa chỉ
            if (cuaHang.DiaChi == null)
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được để trống");
            }
            else if (cuaHang.DiaChi.Length > 200)
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được quá 200 ký tự");
            }
            else if (!kuTuDacBiet.IsMatch(cuaHang.DiaChi))
            {
                ModelState.AddModelError("DiaChi", "Địa Chỉ không được chứa ký tự đặc biệt");
            }
            //hết kiểm lỗi


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
                return RedirectToAction(nameof(Index));
            }
            return View(cuaHang);
        }

        // GET: CuaHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
            return RedirectToAction(nameof(Index));
        }

        private bool CuaHangExists(int id)
        {
          return (_context.CuaHang?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
