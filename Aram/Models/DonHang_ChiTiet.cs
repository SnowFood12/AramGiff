using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("DONHANG_CHITIET")]
    public class DonHang_ChiTiet
    {
        [Key]
        public int Id { get; set; }
		public int SoLuong { get; set; }
        [DefaultValue(1)]
        public bool TrangThai { get; set; }
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public virtual SanPham? SanPham { get; set; }
		public virtual DonHang? DonHang { get; set; }

	}
}
