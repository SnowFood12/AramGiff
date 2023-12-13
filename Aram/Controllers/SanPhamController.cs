using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aram.Data;
using Aram.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace Aram.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly AramContext _context;

        public SanPhamController(AramContext context)
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
        // GET: SanPhams
        public IActionResult Index(int id)
        {
            HttpContext.Session.SetInt32("id", id); // => lưu vào sesion để làm tìm kiếm

            var aramContext = _context.SanPham.Where(x => x.CuaHangId == id).Include(x => x.LoaiSP);

            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;

            ViewBag.ListProduct = aramContext;

            ViewBag.TrangThai = "Tất cả";
            return View();
        }

        // tìm kiếm thông tin sản phẩm
        public async Task<IActionResult> Search(string search)
        {
            PhanQuyen();
            int id = HttpContext.Session.GetInt32("id") ?? 0;

            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;

            if (!string.IsNullOrEmpty(search))
            {
                var ListProduct = _context.SanPham.Where(a => a.Ten.Contains(search) && a.CuaHangId == id);

                ViewBag.SearchShop = search;

                ViewBag.ListProduct = ListProduct;

                return View("Index");
            }
            else
            {
                var ListProduct = _context.SanPham.Where( a => a.CuaHangId == id);

                ViewBag.SearchShop = search;

                ViewBag.ListProduct = ListProduct;

                return View("Index");
            }
        }
        // =================================================

        // lọc sản phẩm 
        // => còn hàng 
        public IActionResult ConHang()
        {
            PhanQuyen();
            int id = HttpContext.Session.GetInt32("id") ?? 0;

            var aramContext = _context.SanPham.Where(a => a.TrangThai == true && a.CuaHangId == id); 

            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;

            ViewBag.ListProduct = aramContext;

            ViewBag.TrangThai = "Còn hàng";
            return View("Index");
        }
        // => hết hàng 
        public IActionResult HetHang()
        {
            PhanQuyen();
            int id = HttpContext.Session.GetInt32("id") ?? 0;

            var aramContext = _context.SanPham.Where(a => a.TrangThai == false && a.CuaHangId == id);

            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;
            ViewBag.ListProduct = aramContext;

            ViewBag.TrangThai = "Hết hàng";
            return View("Index");
        }

        // =================================================

        public IActionResult GetImage(int id)
        {
            PhanQuyen();
            SanPham image = _context.SanPham.Find(id);
            if (image.PicData != null)
            {
                return File(image.PicData, "image/jpeg"); // Trả về hình ảnh dưới dạng file
            }
            else
            {
                return NotFound();
            }
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PhanQuyen();
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.CuaHang)
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create(int chID)
        {
            PhanQuyen();
            ViewBag.chID = chID;
           /* ViewData["CuaHangId"] = new SelectList(_context.CuaHang, "Id", "Id", chID);*/
            var loaisps = new SelectList(_context.LoaiSP, "Id", "Ten");
            ViewData["LoaiSP"] = loaisps;
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile? imageSP)
        {
            PhanQuyen();
            if (sanPham.Ten != null)
            {
                sanPham.Ten = Regex.Replace(sanPham.Ten.Trim(), @"\s+", " ");
            }
            /*Regex kuTuDacBiet = new Regex("^[A-Za-zÀ-ỹĐđĂăÂâÁáÀàẢảẠạẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẾếỀềỂểỄễỆệÊêÍíÌìỈỉỊịỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợÚúÙùỦủỤụỨứỪừỬửỮữỰựỶỷỴỵÝý\\s0-9]+$");
            //kiểm lỗi ten san pham
            if (sanPham.Ten == null)
            {
                ModelState.AddModelError("Ten", "Tên sản phẩm không được để trống");
            }
            else if (sanPham.Ten.Length > 50)
            {
                ModelState.AddModelError("Ten", "Tên sản phẩm không được dài quá 50 kí tự");
            }
            else if (!kuTuDacBiet.IsMatch(sanPham.Ten))
            {
                ModelState.AddModelError("Ten", "Tên sản phẩm không được chứa ký tự đặc biệt hoặc số");
            }*/
            //kiểm lỗi ten gia
            /*if(sanPham.Gia == null)
            {
                ModelState.AddModelError("Gia", "Giá không được để trống");
            } else if (sanPham.Gia < 10)
            {
                ModelState.AddModelError("Gia", "Giá phải lớn hơn 10.000đ");
            }*/

            if (ModelState.IsValid)
            {
                if(imageSP != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageSP.CopyToAsync(memoryStream);
                        sanPham.PicData = memoryStream.ToArray();
                    }
                }
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Thêm thông tin sản phẩm thành công";
                return RedirectToAction(nameof(Index), new { id = sanPham.CuaHangId });
            }
            /*ViewData["CuaHangId"] = new SelectList(_context.CuaHang, "Id", "Id", sanPham.CuaHangId);*/
            var loaisps = new SelectList(_context.LoaiSP, "Id", "Ten");
            ViewData["LoaiSP"] = loaisps;
            ViewBag.chID = sanPham.CuaHangId;
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id, int chID)
        {
            PhanQuyen();
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            /*ViewData["CuaHangId"] = new SelectList(_context.CuaHang, "Id", "Id", sanPham.CuaHangId);*/
            ViewData["LoaiSPId"] = new SelectList(_context.Set<LoaiSP>(), "Id", "Ten", sanPham.LoaiSPId);
            ViewBag.spID = id;
            ViewBag.chID = chID;    
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sanPham, IFormFile? imageSP)
        {
            SanPham sp = _context.SanPham.FirstOrDefault(x => x.Id == id);
            sp.Ten = sanPham.Ten;
            sp.Gia = sanPham.Gia;
            sp.LoaiSPId = sanPham.LoaiSPId;
            if (id != sanPham.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageSP != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            imageSP.CopyToAsync(memoryStream);
                            sp.PicData = memoryStream.ToArray();
                        }
                    }
                    
                    _context.Update(sp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Cập nhật thông tin sản phẩm thành công";
                return RedirectToAction(nameof(Index) , new { id = sp.CuaHangId});
            }
            /*ViewData["CuaHangId"] = new SelectList(_context.CuaHang, "Id", "Id", sanPham.CuaHangId);*/
            ViewData["LoaiSPId"] = new SelectList(_context.Set<LoaiSP>(), "Id", "Ten", sanPham.LoaiSPId);
            ViewBag.chID = sanPham.CuaHangId;
            ViewBag.spID = id;
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id, int chID)
        {
            PhanQuyen();
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.CuaHang)
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewBag.chID = chID;
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int chID)
        {
            PhanQuyen();
            if (_context.SanPham == null)
            {
                return Problem("Entity set 'AramContext.SanPham'  is null.");
            }
            var sanPham = await _context.SanPham.Include(x => x.LoaiSP).FirstOrDefaultAsync(m => m.Id == id); ;
            if (sanPham != null)
            {
                if ( sanPham.TrangThai == true )
                {
                    sanPham.TrangThai = false;
                    _context.SanPham.Update(sanPham);
                    _context.SaveChanges();
                }
                else
                {
                    sanPham.TrangThai = true;
                    _context.SanPham.Update(sanPham);
                    _context.SaveChanges();
                }
                
/*                _context.SanPham.Remove(sanPham);*/
            }

            /*            await _context.SaveChangesAsync();*/

            TempData["Message"] = "Đổi trạng thái sản phẩm thành công";
            return RedirectToAction(nameof(Index), new { id = chID });
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPham?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public JsonResult Check()
        {
            int id = HttpContext.Session.GetInt32("id") ?? 0;
            var TrangThai = _context.SanPham.Where( a => a.CuaHangId == id).Select(a => new
            {
                a.TrangThai,
                a.Id
            });
            return Json(TrangThai); 
        }

        [HttpGet]
        public JsonResult CapNhatTrangThai(int id)
        {
            var sanPham = _context.SanPham.FirstOrDefault( s => s.Id == id);
            if (sanPham.TrangThai == true)
            {
                sanPham.TrangThai = false;
                _context.SanPham.Update(sanPham);
                _context.SaveChanges();
                var TrangThai = _context.SanPham.Where(a => a.CuaHangId == id).Select(a => new
                {
                    a.TrangThai,
                    a.Id
                });
                return Json(TrangThai);
            }
            else
            {
                sanPham.TrangThai = true;
                _context.SanPham.Update(sanPham);
                _context.SaveChanges();
                var TrangThai = _context.SanPham.Where(a => a.CuaHangId == id).Select(a => new
                {
                    a.TrangThai,
                    a.Id
                });
                return Json(TrangThai);
            }
        }
        public  IActionResult TestIndex(int id)
        {
            HttpContext.Session.SetInt32("id", id); // => lưu vào sesion để làm tìm kiếm

            var aramContext = _context.SanPham.Where(x => x.CuaHangId == id).Include(x => x.LoaiSP);

            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;

            ViewBag.ListProduct = aramContext;
            return View();
        }

        public IActionResult FormInput()
        {
            return View();
        }
    }
}
