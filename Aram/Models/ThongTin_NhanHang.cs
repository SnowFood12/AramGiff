using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("THONGTIN_NHANHANG")] 
    public class ThongTin_NhanHang
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
        [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
		[Required(ErrorMessage = "Tên không được để trống")]
		public string? HoTen { get; set; }
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 số")]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có 10 số")]
		[Column(TypeName = "char(10)")]
        public string? SoDT { get; set; }
        [Column(TypeName = "ntext")]
        [RegularExpression(@"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
		public string? DiaChi { get; set; }
        [Column(TypeName = "ntext")]
        public string? GhiChu { get; set; }
        [ForeignKey("DonHang")]
        public int DonHangId { get; set; }
        public DonHang? DonHang { get; set; }

    }
}
