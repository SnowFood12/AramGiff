using Aram.Data;
using Aram.Infrastructure;
using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace Aram.Controllers
{
	public class DonHangController : Controller
	{
		public GioHang? GioHang { get; set; }
        private readonly AramContext _context;

		public DonHangController( AramContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
            var ListDonHang = new List<DonHang>();

            ListDonHang = _context.DonHang.Where(a => a.TrangThaiDH == "Chờ duyệt" && a.TrangThai == true)
                    .OrderByDescending(a => a.ThoiGianTaoDon)
                    .Include(x => x.ThongTin_NhanHangs)
                    .Include(x => x.DonHang_ChiTiets)
                    .ThenInclude(x=> x.SanPham)
                    .ToList();

            var DangGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đang giao" && a.TrangThai == true)
                    .OrderByDescending(a => a.ThoiGianTaoDon)
                    .Include(a => a.ThongTin_NhanHangs)
                    .Include(a => a.DonHang_ChiTiets)
                    .ThenInclude(a => a.SanPham)
                    .ToList();


			ViewBag.ChoDuyet = ListDonHang;
            

            ViewBag.DangGiao = DangGiao; 

			ViewBag.DemChoDuyet = ListDonHang.Count();
			ViewBag.DemDangGiao = DangGiao.Count();

            return View();
		}

        public int Tong(int Id)
        {
            var ListDonHang = new List<DonHang>();

            ListDonHang = _context.DonHang.Where(x => x.Id == Id)
                .Include(a => a.DonHang_ChiTiets)
                .ThenInclude(a => a.SanPham).ToList();
            return ListDonHang.Count();
        }


        // duyệt đơn
        public IActionResult DuyetDon(int id)
		{
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThaiDH = "Đang giao";

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();
            TempData["Message"] = "Duyệt đơn hàng thành công!!";
            Index();
            return RedirectToAction("Index", "DonHang");
		}

        // chi tiết đơn hàng chờ duyệt
        public IActionResult Details(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.IdDonHang = id;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

            return View();
		}

		// đơn hàng đang giao
		public IActionResult DonHangDangGiao()
		{
            return View();
		}

		// Đơn hàng đã giao
		public IActionResult DaGiao(int id)
		{
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThaiDH = "Đã giao";

            DonHang.ThoiGianTaoDon = DateTime.Now;

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();

            Index();
            TempData["Message"] = "Giiao hàng thành công";

            return RedirectToAction("Index", "DonHang");
		}

        // Xác nhận huỷ đơn hàng
		public IActionResult XacNhanHuy(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

			

            return View();
		}

		// huỷ đơn hàng
        public IActionResult HuyDonHang(int id)
        {
            var DonHang = _context.DonHang.FirstOrDefault(a => a.Id == id);

            DonHang.TrangThai = false;

            DonHang.ThoiGianTaoDon = DateTime.Now;

            _context.DonHang.Update(DonHang);
            _context.SaveChanges();
			TempData["Message"] = "Huỷ đơn hàng thành công";
            Index();
            return RedirectToAction("Index", "DonHang");
        }

        // Chi tiết đơn hàng đang giao
        public IActionResult ChiTietDonHangDangGiao(int id)
		{
			var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude( a => a.CuaHang).ToList();

			var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault( a => a.DonHangId == id);

			var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

			ViewBag.TamTinh = TamTinh;

            ViewBag.IdDonHang = id;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

			ViewBag.ListProduct = DonHangChiTiet;

            return View();
		}

		// lịch sử đơn hàng
		public IActionResult LichSuDonHang()
		{
            var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã giao" || a.TrangThai == false)
                        .OrderByDescending(a => a.ThoiGianTaoDon)
                        .Include(a => a.ThongTin_NhanHangs)
                        .Include(a => a.DonHang_ChiTiets)
                        .ThenInclude(a => a.SanPham)
                        .ToList();

            ViewBag.DaGiao = DaGiao;

            ViewBag.DemDaGiao = DaGiao.Count();

            ViewBag.TinhTrang = "Tất cả";
            return View();
		}

        // đơn hàng huỷ
        public IActionResult HuyDon()
        {


            var DaGiao = _context.DonHang.Where(a => a.TrangThai == false)
                    .OrderByDescending(a => a.ThoiGianTaoDon)
                    .Include(a => a.ThongTin_NhanHangs)
                    .Include(a => a.DonHang_ChiTiets)
                    .ThenInclude(a => a.SanPham)
                    .ToList();

            ViewBag.DaGiao = DaGiao;

            ViewBag.DemDaGiao = DaGiao.Count();

            ViewBag.TinhTrang = "Đã huỷ";

            return View("LichSuDonHang");
        }

        // đơn hàng giao thành công
        public IActionResult GiaoThanhCong()
        {
            var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã giao" && a.TrangThai == true)
                    .OrderByDescending(a => a.ThoiGianTaoDon)
                    .Include(a => a.ThongTin_NhanHangs)
                    .Include(a => a.DonHang_ChiTiets)
                    .ThenInclude(a => a.SanPham)
                    .ToList();

            ViewBag.DaGiao = DaGiao;

            ViewBag.DemDaGiao = DaGiao.Count();

            ViewBag.TinhTrang = "Đã giao";

            return View("LichSuDonHang");
        }
        //==============================================================================================

        // chi tiết lịch sử đơn hàng
        public IActionResult ChiTietDonHangDaGiao(int id)
		{
            var DonHangChiTiet = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).ThenInclude(a => a.CuaHang).ToList();

            var ThongTinGiaoHang = _context.ThongTin_NhanHang.FirstOrDefault(a => a.DonHangId == id);

            var TamTinh = _context.DonHang_ChiTiet.Where(a => a.DonHangId == id).Include(a => a.SanPham).Sum(a => a.SoLuong * a.SanPham.Gia);

            ViewBag.TamTinh = TamTinh;

            ViewBag.ThongTinGiaoHang = ThongTinGiaoHang;

            ViewBag.ListProduct = DonHangChiTiet;

            return View();
        }

        //====================================================================

        // tìm kiếm đơn hàng theo ngay
        public IActionResult Search( DateTime search)
        {
            var DaGiao = _context.DonHang.Where(a => a.TrangThaiDH == "Đã giao" || a.TrangThai == false && 
                        a.ThoiGianTaoDon.Day == search.Day && 
                        a.ThoiGianTaoDon.Month == search.Month && 
                        a.ThoiGianTaoDon.Year == search.Year)
                        .OrderByDescending(a => a.ThoiGianTaoDon)
                        .Include(a => a.ThongTin_NhanHangs)
                        .Include(a => a.DonHang_ChiTiets)
                        .ThenInclude(a => a.SanPham)
                        .ToList();
            ViewBag.DaGiao = DaGiao;

            ViewBag.Search = search; 

            ViewBag.TinhTrang = "Tất cả";
            return View("LichSuDonHang");
        }


        // Thống kê doanh thu
        public IActionResult ThongKe()
        {
            var ListDonHang = _context.DonHang.Where(a => a.TrangThai == true && a.TrangThaiDH == "Đã giao" && a.ThoiGianTaoDon.Month == DateTime.Now.Month).ToList();

            var HomNay = _context.DonHang.Where(a => a.TrangThai == true && a.TrangThaiDH == "Đã giao" && a.ThoiGianTaoDon.Date == DateTime.Now.Date).ToList();

            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            var HomQua = _context.DonHang.Where(a => a.TrangThai == true && a.TrangThaiDH == "Đã giao" && a.ThoiGianTaoDon.Date == yesterday).ToList();

            ViewBag.DemTong = ListDonHang.Count; 

            ViewBag.DemDay = HomNay.Count;

            ViewBag.HomQua = HomQua.Count;

            return View();
        }

        // lấy dữ liệu để vẽ biểu đồ
        [HttpGet]
        public JsonResult ThongKeDoanhThu()
        {
            var revenueData = _context.DonHang.Where(a => a.TrangThai == true && a.TrangThaiDH == "Đã giao" && a.ThoiGianTaoDon.Month == DateTime.Now.Month)
                                              .GroupBy(a => a.ThoiGianTaoDon.Day).Select(s => new
                                              {
                                                  Day = s.Select(a => new
                                                  {
                                                      a.ThoiGianTaoDon,
                                                  }),
                                                  Count = s.Count()
                                              }).ToList(); 

            return Json(revenueData);
        }
        // lấy đơn hàng đã huỷ
        [HttpGet]
        public JsonResult DonHangDaHuy()
        {
            var revenueData = _context.DonHang.Where(a => a.TrangThai == false && a.ThoiGianTaoDon.Month == DateTime.Now.Month)
                                              .GroupBy(a => a.ThoiGianTaoDon.Day).Select(s => new
                                              {
                                                  Day = s.Select(a => new
                                                  {
                                                      a.ThoiGianTaoDon,
                                                  }),
                                                  Count = s.Count()
                                              }).ToList();

            return Json(revenueData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string HoTen, string SoDT, string DiaChi, string GhiChu)
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
            _context.Add(TT_NH);
            _context.SaveChanges();
            return RedirectToAction("DonHangDangGiao", "GioHang");
        }

    }
}
