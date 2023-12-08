using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("TAI_KHOAN")]
    public class TaiKhoan
    {
        [Key]
        [StringLength(15)]
        public string? TenTK  { get; set; }
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
        [DefaultValue(0)]
        public bool LoaiTK { get; set; }
        [DefaultValue(1)]
        public bool TrangThai { get; set; }
        public virtual ICollection<CuaHang>? CuaHangs { get; set; }
        public virtual ICollection<DonHang>? DonHangs { get; set; }
    }
}
