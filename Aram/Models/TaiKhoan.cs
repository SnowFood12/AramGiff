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
        public bool? GioiTinh { get; set; }
        [StringLength(200)]
        public string? Email { get; set; }
        [Column(TypeName = "char(10)")]
        public string? SoDT { get; set; }
        public DateTime NgayTao { get; set; }
        [DefaultValue(true)]
        public bool LoaiTK { get; set; }
		[DefaultValue(true)]
		public bool TrangThai { get; set; }
    }
}
