using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("THONGTIN_NHANHANG")] 
    public class ThongTin_NhanHang
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? HoTen { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? SoDT { get; set; }
        [Column(TypeName = "ntext")]
        public string? DiaChi { get; set; }
        [Column(TypeName = "ntext")]
        public string? GhiChu { get; set; }
        public int DonHangId { get; set; }
        public DonHang DonHang { get; set; } = null!;
    }
}
