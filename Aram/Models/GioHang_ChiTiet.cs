using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("GIOHANG_CHITIET")]
    public class GioHang_ChiTiet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GioHangId { get; set; }
        public GioHang? GioHang { get; set; }
        [Required]
        public int SanPhamId { get; set; }
        public SanPham? SanPham { get; set; }
        public int SoLuong { get; set; }
    }
}
