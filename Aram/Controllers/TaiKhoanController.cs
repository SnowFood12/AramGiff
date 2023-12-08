using Aram.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Aram.Data;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aram.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AramContext _context;
        private readonly IConfiguration _config;
		private static string LuuTenTK;
        private static string LuuMK;
		private static string LuuHoTen;
		private static string LuuSDT;
        private static string LuuEmail;
		private static string currentOTP;

		public TaiKhoanController (AramContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IActionResult Index()
        {
            if (LuuTenTK == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            ViewBag.TenTK = LuuTenTK;
            ViewBag.HoTen = LuuHoTen;
			ViewBag.SDT = LuuSDT;
            ViewBag.Email = LuuEmail;
			return View();
        }
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(TaiKhoan taiKhoan, string XacNhanMatKhau)
        {
            //Kiểm tra tên đăng nhập
            var ktTenDangNhap = _context.TaiKhoan.FirstOrDefault(t => t.TenTK == taiKhoan.TenTK);
            var ktEmail = _context.TaiKhoan.FirstOrDefault(e => e.Email == taiKhoan.Email);
            if (taiKhoan.TenTK == null)
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập không thể trống!");
            }
            else if (taiKhoan.TenTK.Length < 6 || taiKhoan.TenTK.Length > 15)
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập phải từ 6 đến 15 ký tự!");
            }
            else if (ktTenDangNhap != null)
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập đã tồn tại!");
            }
            else if (Regex.IsMatch(taiKhoan.TenTK, @"\s") || Regex.IsMatch(taiKhoan.TenTK, @"[^a-zA-Z0-9]"))
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập không được chứa ký tự đặc biệt!");
            }

            // Kiểm tra email đúng định dạng
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
            if (taiKhoan.MatKhau == null || taiKhoan.MatKhau.Trim() == "")
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu không được để trống!");
            }
            else if (taiKhoan.MatKhau.Length < 6)
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu phải từ 6 ký tự trở lên!");
            }

            // Kiểm tra xác thực mật khẩu
            if (XacNhanMatKhau == null || XacNhanMatKhau.Trim() == "")
            {
                ViewBag.LoiMK = "Xác nhận mật khẩu không được để trống!";
            }
            else if (taiKhoan.MatKhau != XacNhanMatKhau)
            {
                ViewBag.LoiMK = "Xác nhận mật khẩu không trùng khớp mật khẩu!";
            }

            // Mọi điều kiện hợp lệ, tiến hành thêm tài khoản vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                LuuTenTK = taiKhoan.TenTK;
                LuuEmail = taiKhoan.Email;
                LuuMK = taiKhoan.MatKhau;
                string maOTP = GenerateOTP();
                SendEmail(LuuEmail, maOTP);
                currentOTP = maOTP;
                // Đặt hẹn giờ reset OTP sau 2 phút
                otpResetTimer = new System.Timers.Timer(2 * 60 * 1000); // 2 phút = 2 * 60 * 1000 miligiây
                otpResetTimer.Elapsed += (sender, e) => ResetOTP();
                otpResetTimer.AutoReset = true; // Đặt lại thành true để hẹn giờ tự động lặp lại
                otpResetTimer.Start();
                return RedirectToAction("XacNhanDangKy", "TaiKhoan");
            }
            else
            {
                return View(taiKhoan);
            }
        }
        public IActionResult XacNhanDangKy(string otp)
        {
            string storeOTP = currentOTP;

            ViewBag.ShowError = false; // Biến để xác định xem có hiển thị thông báo lỗi hay không

            if (string.IsNullOrEmpty(otp))
            {
                ViewBag.ShowError = true;
                ViewBag.LoiNhapOTP = "Mã OTP không được để trống!";
            }
            else if (otp.Length != 6)
            {
                ViewBag.ShowError = true;
                ViewBag.LoiNhapOTP = "Mã OTP phải có đúng 6 số!";
            }
            else if (otp != storeOTP)
            {
                ViewBag.ShowError = true;
                ViewBag.LoiNhapOTP = "Mã xác thực OTP không trùng khớp!";
            }
            else
            {
                ViewBag.LoiNhapOTP = null;
            }

            if (ModelState.IsValid)
            {
                if (otp == storeOTP)
                {
                    TaiKhoan taiKhoan = new TaiKhoan()
                    {
                        TenTK = LuuTenTK,
                        MatKhau = LuuMK,
                        Email = LuuEmail
                    };
                    taiKhoan.NgayTao = DateTime.Now;
                    taiKhoan.SoDT = null;
                    taiKhoan.LoaiTK = true;
                    taiKhoan.TrangThai = true;
                    _context.Add(taiKhoan);
                    _context.SaveChangesAsync();
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }
            }
            return View();
        }
        
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap([Bind("TenTK,MatKhau,HoTen,GioiTinh,Email,SoDT,NgayTao,LoaiTK,TrangThai")] TaiKhoan taiKhoan)
        {
            if (taiKhoan.TenTK == null)
			{
                ModelState.AddModelError("TenTK","Tên đăng nhập không được để trống!");
            }
            else if(taiKhoan.TenTK.Length < 6 || taiKhoan.TenTK.Length > 15)
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập phải từ 6 kí tự trờ lên!");

			}
            else if (Regex.IsMatch(taiKhoan.TenTK, @"\s") || Regex.IsMatch(taiKhoan.TenTK, @"[^a-zA-Z0-9]"))
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập không được chứa ký tự đặc biệt!");
            }
            if (taiKhoan.MatKhau == null)
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu không được để trống!");
            }
            else if (taiKhoan.MatKhau.Length < 6)
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu phải từ 6 kí tự trở lên!");
            }
            if (ModelState.IsValid)
            {
                try
                {
					// Kiểm tra tài khoản có tồn tại trong CSDL không
					taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.TenTK == taiKhoan.TenTK && x.MatKhau == taiKhoan.MatKhau);
					if (taiKhoan != null)
                    {
						LuuTenTK = taiKhoan.TenTK;
                        LuuHoTen = taiKhoan.HoTen;
						LuuSDT = taiKhoan.SoDT;
                        LuuEmail = taiKhoan.Email;
						return RedirectToAction("MainHome", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("MatKhau", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex);
                }
            }

            return View();
        }
		//Phương thức gửi email chứa OTP
		private void SendEmail(string Email, string otp)
		{
			string smtpServer = _config.GetValue<string>("EmailSettings:SmtpServer");
			int smtpPort = _config.GetValue<int>("EmailSettings:SmtpPort");
			string userName = _config.GetValue<string>("EmailSettings:UserName");
			string password = _config.GetValue<string>("EmailSettings:Password");

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("AramGift", userName));
			message.To.Add(new MailboxAddress("", Email));
			message.Subject = "Mã xác thực OTP";
			message.Body = new TextPart("plain")
			{
				Text = $"Mã OTP của bạn là: {otp}"
			};
			using (var client = new MailKit.Net.Smtp.SmtpClient())
			{
				client.Connect(smtpServer, smtpPort, false);
				client.Authenticate(userName, password);
				client.Send(message);
				client.Disconnect(true);
			}
		}
		//Hàm tạo OTP ngẫu nhiên
		private string GenerateOTP()
		{
			//Tạo một số ngẫu nhiên 
			Random random = new Random();
			int otpNumber = random.Next(100000, 999999);
			return otpNumber.ToString();
		}
		public void ResetOTP()
		{
			// Tạo mã OTP mới
			string newOTP = GenerateOTP();
			currentOTP = newOTP;
		}
		//Phương thức gửi lại mã OTP
		public IActionResult SendOTP()
		{
			string email = LuuEmail;
			string maOTP = GenerateOTP();
			currentOTP = maOTP;
			SendEmail(email, maOTP);
			return RedirectToAction("NhapOTP", "TaiKhoan");
		}
		private System.Timers.Timer otpResetTimer;
        public IActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
		public IActionResult QuenMatKhau(TaiKhoan taiKhoan)
        {
            if(taiKhoan.TenTK == null)
            {
                ModelState.AddModelError("TenTK"," Tên đăng nhập không được để trống!");
            }
            else if (taiKhoan.TenTK.Length < 8 || taiKhoan.TenTK.Length > 15)
            {
                ModelState.AddModelError("TenTK", "Tên đăng nhập phải từ 8 kí tự trở lên!");
            }
            else if (taiKhoan.Email == null)
            {
                ModelState.AddModelError("Email", "Email không được để trống!");
            }
            else if (!taiKhoan.Email.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("Email", "Email không đúng định dạng @gmail.com!");
            }
             taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.TenTK == taiKhoan.TenTK && x.Email == taiKhoan.Email);
			if (taiKhoan != null)
            {
                string maOTP = GenerateOTP();
                SendEmail(taiKhoan.Email, maOTP);
                LuuTenTK = taiKhoan.TenTK;
                LuuEmail = taiKhoan.Email;
				currentOTP = maOTP;
				// Đặt hẹn giờ reset OTP sau 2 phút
				otpResetTimer = new System.Timers.Timer(2 * 60 * 1000); // 2 phút = 2 * 60 * 1000 miligiây
				otpResetTimer.Elapsed += (sender, e) => ResetOTP();
				otpResetTimer.AutoReset = true; // Đặt lại thành true để hẹn giờ tự động lặp lại
				otpResetTimer.Start();
				return RedirectToAction("NhapOTP", "TaiKhoan");
			}
				return View();
		}
        public IActionResult NhapOTP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NhapOTP(string otp)
        {
			string storeOTP = currentOTP;
			if (string.IsNullOrEmpty(otp))
			{
				ViewBag.LoiNhapOTP = "Mã OTP không được để trống!";
			}
			else if (otp.Length != 6)
			{
				ViewBag.LoiNhapOTP = "Mã OTP phải có đúng 6 số!";
			}
			else if (otp != storeOTP)
			{
				ViewBag.LoiNhapOTP = "Mã xác thực OTP không trùng khớp!";
			}

			if (ModelState.IsValid)
			{
				if (otp == storeOTP)
				{
					return RedirectToAction("DoiMatKhauMoi", "TaiKhoan");
				}
			}

			return View();
        }
        public IActionResult DoiMatKhauMoi(string MatKhauMoi)
        {
			string tenDangNhap = LuuTenTK;
			TaiKhoan taiKhoan = _context.TaiKhoan.FirstOrDefault(x => x.TenTK == tenDangNhap);
            if (taiKhoan != null)
            {
                if (!string.IsNullOrEmpty(MatKhauMoi))
                {
					taiKhoan.MatKhau = MatKhauMoi;
					_context.SaveChanges();
					return RedirectToAction("DangNhap", "TaiKhoan");
				}
			}
			return View();
        }
        public IActionResult DangXuat()
        {
            return RedirectToAction("DangNhap", "TaiKhoan");
		}
        
    }
}
