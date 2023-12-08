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

namespace Aram.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly AramContext _context;

        public SanPhamController(AramContext context)
        {
            _context = context;
        }
        // GET: SanPhams
        public async Task<IActionResult> Index(int id)
        {
            var aramContext = _context.SanPham.Where(x => x.CuaHangId == id).Include(x => x.LoaiSP);
            
            ViewBag.chName = _context.CuaHang.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.chID = id;
            return View(await aramContext.ToListAsync());
        }

        public IActionResult GetImage(int id)
        {
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
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile imageSP)
        {
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
        public async Task<IActionResult> Edit(int id, SanPham sanPham, IFormFile imageSP)
        {
            SanPham sp = _context.SanPham.FirstOrDefault(x => x.Id == id);
            sanPham.PicData = sp.PicData;
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
                            sanPham.PicData = memoryStream.ToArray();
                        }
                    }
                    _context.Update(sanPham);
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
                return RedirectToAction(nameof(Index) , new { id = sanPham.CuaHangId});
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
            if (_context.SanPham == null)
            {
                return Problem("Entity set 'AramContext.SanPham'  is null.");
            }
            var sanPham = await _context.SanPham.Include(x => x.LoaiSP).FirstOrDefaultAsync(m => m.Id == id); ;
            if (sanPham != null)
            {
                
                _context.SanPham.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = chID });
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPham?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public JsonResult Check( int id)
        {
            var TrangThai = _context.SanPham.Where( a => a.CuaHangId == id).Select(a => new
            {
                a.TrangThai
            });
            return Json(TrangThai); 
        }
    }
}
