using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Aram.Models
{
    [Table("CUA_HANG")]
    public class CuaHang
    {
        [Key]
        public int Id { get; set; }
		[RegularExpression(@"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
		[StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
        [Required(ErrorMessage = "Tên không được để trống")]
		public string? Ten { get; set; }
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 số")]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có 10 số")]
		[Column(TypeName = "char(10)")]
		public string? SoDT { get; set; }
        public DateTime NgayTaoCuaHang { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
		[RegularExpression(@"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$", ErrorMessage = "Địa chỉ không được chứa ký tự đặc biệt")]
		[Column(TypeName = "ntext")]
		public string? DiaChi { get; set; }
        [Column(TypeName = "text")]
        public string? LinkMap { get; set; }
        public bool? TrangThai { get; set; } = true;
		[Column(TypeName = "varchar(15)")]
		[ForeignKey("TaiKhoan")]
		public string? TenTK { get; set; }
		public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
