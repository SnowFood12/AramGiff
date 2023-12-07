using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Aram.Data;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace Aram.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AramContext _context;
        private readonly IConfiguration _config;

        public TaiKhoanController (AramContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangKy([Bind("ID,Name,Password,Email,SoDT,NgayTao,LoaiTK,TrangThai")] TaiKhoan taiKhoan, string XacNhanMatKhau)
        {
            //Kiểm tra tên đăng nhập
            var ktTenDangNhap = _context.TaiKhoan.FirstOrDefault(t => t.Name == taiKhoan.Name);
            var ktEmail = _context.TaiKhoan.FirstOrDefault(e => e.Email == taiKhoan.Email);
            if (taiKhoan.Name == null)
            {
                ModelState.AddModelError("Name", "Tên đăng nhập không thể trống!");
            }
            else if (taiKhoan.Name.Length < 8 || taiKhoan.Name.Length > 15)
            {
                ModelState.AddModelError("Name", "Tên đăng nhập phải từ 8 đến 15 ký tự!");
            }
            else if (ktTenDangNhap != null)
            {
                ModelState.AddModelError("Name", "Tên đăng nhập đã tồn tại!");
            }

            // Kiểm tra email
            if (taiKhoan.Email == null || taiKhoan.Email.Trim() == "")
            {
                ModelState.AddModelError("Email", "Email không được để trống!");
            }
            else if (!taiKhoan.Email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email phải có định dạng đúng @gmail.com!");
            }
            else if (ktEmail != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại!");
            }

            // Kiểm tra mật khẩu
            if (taiKhoan.Password == null || taiKhoan.Password.Trim() == "")
            {
                ModelState.AddModelError("Password", "Mật khẩu không được để trống.");
            }
            else if (taiKhoan.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Mật khẩu phải từ 8 ký tự trở lên.");
            }

            // Kiểm tra xác thực mật khẩu
            if (XacNhanMatKhau == null || XacNhanMatKhau.Trim() == "")
            {
                ModelState.AddModelError("XacNhanMatKhau", "Xác nhận mật khẩu không được để trống.");
            }
            else if (taiKhoan.Password != XacNhanMatKhau)
            {
                ModelState.AddModelError("XacNhanMatKhau", "Xác nhận mật khẩu không trùng khớp với mật khẩu!");
            }

            // Mọi điều kiện hợp lệ, tiến hành thêm tài khoản vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                // Mọi điều kiện hợp lệ, tiến hành thêm tài khoản vào cơ sở dữ liệu
                taiKhoan.NgayTao = DateTime.Now;
                taiKhoan.SoDT = null;
                taiKhoan.LoaiTK = true;
                taiKhoan.TrangThai = true;
                _context.Add(taiKhoan);
                _context.SaveChangesAsync();
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                return View(taiKhoan);
            }
        }
        public IActionResult DangNhap([Bind("ID,Name,Password,Email,SoDT,NgayTao,LoaiTK,TrangThai")] TaiKhoan taiKhoan)
        {
            taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.Name == taiKhoan.Name && x.Password == taiKhoan.Password);
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra tài khoản có tồn tại trong CSDL không
                  
                    if (taiKhoan != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex);
                }
            }

            return View();
        }
        public IActionResult QuenMatKhau()
        {
            return View();
        }
        public IActionResult NhapOTP()
        {
            return View();
        }
        public IActionResult DoiMatKhauMoi()
        {
            return View();
        }
    }
}
