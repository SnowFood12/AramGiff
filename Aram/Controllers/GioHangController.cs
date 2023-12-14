using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Aram.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AramContext _context;

        public GioHangController(AramContext context)
        {
            _context = context;
        }
		public GioHang? GioHang { get; set; }

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
        public IActionResult Index()
        {
            PhanQuyen();

			GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
			/*ViewBag.TamTinh = (int)gioHangs.Sum(p => p.SanPham.Gia * p.SoLuong);*/
			return View(GioHang);
        }
		public IActionResult AddToGioHang(int sanPhamId, int? quantity)
		{
			SanPham? sanPham = _context.SanPham.FirstOrDefault(s => s.Id == sanPhamId);
			if (sanPham != null)
			{
				GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
                if(quantity != null)
                {
                    GioHang.AddItem(sanPham, (int)quantity);
                }
                else
                {
                    GioHang.AddItem(sanPham, 1);
                }
				HttpContext.Session.SetJson("giohang", GioHang);
			}
			return View("Index", GioHang);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddToDonHang(string HoTen, string SoDT, string DiaChi, string GhiChu)
		{
			GioHang = HttpContext.Session.GetJson<GioHang>("giohang") ?? new GioHang();
			bool kt = true;
			string kiemLoiTen = @"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$";
			string kiemLoiDT = @"^0\d{9}$";
			if (HoTen == null)
			{
				ViewBag.ktHoTen = "Vui lòng nhập họ Tên";
				kt = false;
				return View("Index",GioHang);
			} else if(HoTen.Length > 50)
			{
				ViewBag.ktHoTen = "Tên không được dài quá 50 Ký tự!";
				kt = false;
				return View("Index", GioHang);
			}
			else if (!Regex.IsMatch(HoTen, kiemLoiTen))
			{
				ViewBag.ktHoTen = "Tên không được chứa ký tự đặc biệt!";
				kt = false;
				return View("Index", GioHang);
			}

			if (SoDT == null)
			{
				ViewBag.ktDT = "Số điện thoại không được trống!";
				kt = false;
				return View("Index", GioHang);
			}
			else if (!Regex.IsMatch(SoDT, kiemLoiDT))
			{
				ViewBag.ktDT = "Số điện thoại không hợp lệ!";
				kt = false;
				return View("Index", GioHang);
			}
			if (DiaChi == null)
			{
				ViewBag.ktDiaChi = "Địa chỉ không được trống!";
				kt = false;
				return View("Index", GioHang);
			}
				if (kt == true)
				{
					var donHang = new DonHang();
					donHang.TenTK = HttpContext.Session.GetString("Name");
					_context.Add(donHang);
					_context.SaveChanges();
					GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
					foreach (var item in GioHang.Lines)
					{
						var donHang_chiTiet = new DonHang_ChiTiet();
						donHang_chiTiet.SanPhamId = item.SanPham.Id;
						donHang_chiTiet.DonHangId = donHang.Id;
						donHang_chiTiet.SoLuong = item.SoLuong;
						_context.Add(donHang_chiTiet);
						_context.SaveChanges();
					}
					var TT_NH = new ThongTin_NhanHang();
					TT_NH.DonHangId = donHang.Id;
					TT_NH.HoTen = HoTen;
					TT_NH.SoDT = SoDT;
					TT_NH.DiaChi = DiaChi;
					TT_NH.GhiChu = GhiChu;
					TT_NH.DonHangId = donHang.Id;
					_context.Add(TT_NH);
					_context.SaveChanges();
					HttpContext.Session.Remove("giohang");
				return RedirectToAction("DH_ChoDuyet", "GioHang");
				}
			return RedirectToAction("Index");
		}
		//=======================================================

		// ===> tăng số lượng 
		[HttpGet]
		public JsonResult TangSoLuong(int id)
		{
			var GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
			var GioHang_line = GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == id);
			GioHang_line.SoLuong++;
			HttpContext.Session.SetJson("giohang", GioHang);

			var DonGia = GioHang_line.SanPham.Gia;

			var json = new
			{
                SoLuong = GioHang_line.SoLuong,
                TongTienSanPham = GioHang_line.TongTienSp(),
                TamTinh = GioHang.TamTinh(),
                TongTien = GioHang.TongTien(),
            };

			return Json(json);

		}

		// giảm số lượng
		[HttpGet]
		public JsonResult GiamSoLuong(int id)
		{
			GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
			var GioHang_line = GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == id);
			GioHang_line.SoLuong--;
			HttpContext.Session.SetJson("giohang", GioHang);

            var DonGia = _context.SanPham.FirstOrDefault( a => a.Id == id);
            var json = new
			{
                SoLuong = GioHang_line.SoLuong,
                TongTienSanPham = GioHang_line.TongTienSp(),
                TamTinh = GioHang.TamTinh(),
                TongTien = GioHang.TongTien(),
            };

			return Json(json);

		}
		public IActionResult XoaSPGioHang(int Id)
		{
            GioHang = HttpContext.Session.GetJson<GioHang>("giohang");
            GioHang.Lines.FirstOrDefault(p => p.SanPham.Id == Id);
            if (GioHang.Lines != null)
			{
				GioHang.RemoveSanPham(Id);
                HttpContext.Session.SetJson("giohang", GioHang);
            }
			return RedirectToAction(nameof(Index), GioHang);
		}
		// đơn hàng đang chờ admin duyệt
		public IActionResult DH_ChoDuyet()
		{
			var tenTK = HttpContext.Session.GetString("Name");
			var donHang = new List<DonHang>();
			var donHang_choDuyet = new List<DonHang>();
            if (tenTK == null)
			{
				return RedirectToAction("DangNhap", "TaiKhoan");
			}
			else 
			{
				donHang = _context.DonHang.Where(x => x.TenTK == tenTK).Include(x => x.ThongTin_NhanHangs).Include(x => x.DonHang_ChiTiets).ThenInclude(a =>a.SanPham).ToList();
                donHang_choDuyet = donHang.Where(x => x.TrangThai == true && x.TrangThaiDH == "Chờ duyệt").ToList();
            }
			return View(donHang_choDuyet);
		}

		// chi tiết đơn hàng đang chở duyệt

		public IActionResult CT_DH_ChoDuyet(int Id)
		{
			var DH_CT = _context.DonHang_ChiTiet.Where(x => x.DonHangId == Id).Include(x => x.SanPham).ToList();
			int TamTinh = 0;
			if(DH_CT != null)
			{
                TamTinh = (int)DH_CT.Sum(x => x.SanPham.Gia * x.SoLuong);
                ViewBag.TamTinh = TamTinh;
				ViewBag.DonhangId = Id;
			}
            return View(DH_CT);
		}
		public IActionResult HuyDon_ChuaDuyet(int Id)
		{
            var donHang = _context.DonHang.Where(x=> x.Id == Id && x.TrangThaiDH == "Chờ duyệt").FirstOrDefault();
			if (donHang != null && donHang.TrangThai == true)
			{
				donHang.TrangThai = false;
				_context.DonHang.Update(donHang);
				TempData["Message"] = "huỷ đơn hàng thành công!";
				_context.SaveChanges();
				return RedirectToAction(nameof(DH_ChoDuyet));
			}
			else
			{
				TempData["Message"] = "Đơn hàng đang được duyệt, không thể hủy đơn!";
                return RedirectToAction(nameof(CT_DH_ChoDuyet));
            }
        }
        // đơn hàng đang giao
        public IActionResult DH_DangGiao()
		{
			var tenTK = HttpContext.Session.GetString("Name");
			var donHang = new List<DonHang>();
			var donHang_dangGiao = new List<DonHang>();
			if (tenTK == null)
			{
				return RedirectToAction("DangNhap", "TaiKhoan");
			}
			else
			{
				donHang = _context.DonHang.Where(x => x.TenTK == tenTK).Include(x => x.ThongTin_NhanHangs).Include(x => x.DonHang_ChiTiets).ThenInclude(a => a.SanPham).ToList();
				donHang_dangGiao = donHang.Where(x => x.TrangThai == true && x.TrangThaiDH == "Đang giao").ToList();
			}
			return View(donHang_dangGiao);
		}
        // Chi tiết đơn hàng đang giao
        public IActionResult CT_DH_DangGiao()
        {

            return View();
        }


        // đơn hàng đã nhận 
        public IActionResult DH_DaNhan()
        {
            var tenTK = HttpContext.Session.GetString("Name");
            var donHang = new List<DonHang>();
            var donHang_daGiao = new List<DonHang>();
            if (tenTK == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                donHang = _context.DonHang.Where(x => x.TenTK == tenTK).Include(x => x.ThongTin_NhanHangs).Include(x => x.DonHang_ChiTiets).ThenInclude(a => a.SanPham).ToList();
                donHang_daGiao = donHang.Where(x => x.TrangThai == true && x.TrangThaiDH == "Đã giao").ToList();
            }
            return View(donHang_daGiao);
        }

        // Chi tiết đơn hàng đã nhận
        public IActionResult CT_DH_DaNhan()
        {
            PhanQuyen();

            return View();
        }

    }
}
