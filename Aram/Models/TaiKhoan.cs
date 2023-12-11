using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("TAI_KHOAN")]
    public class TaiKhoan
    {
        	
		[StringLength(15)]
		[Key]
		public string TenTK  { get; set; }
        [StringLength(15)]
        public string? MatKhau { get; set; }
        [StringLength(50)]
        public string? HoTen { get; set; }
        public bool GioiTinh { get; set; }
        [StringLength(200)]
        public string? Email { get; set; }
        [Column(TypeName = "char(10)")]
        public string? SoDT { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }
        public bool LoaiTK { get; set; } = true;
        public bool TrangThai { get; set; } = true;
        public ICollection<CuaHang>? CuaHangs { get; set; }
        public ICollection<DonHang>? DonHangs { get; set; }
    }
}
