using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("DON_HANG")]
    public class DonHang
    {
        [Key]
        public int Id { get; set; }
        public DateTime ThoiGianTaoDon { get; set; } = DateTime.Now;
        [Column(TypeName = "nvarchar(20)")]
		public string TrangThaiDH { get; set; } = "Chờ duyệt";
        [DefaultValue(true)]
        public bool TrangThai { get; set; }
		[ForeignKey("TaiKhoan")]
        public string? TenTK { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public virtual ThongTin_NhanHang? ThongTin_NhanHangs { get; set; }
        public virtual ICollection<DonHang_ChiTiet>? DonHang_ChiTiets { get; set; }

    }
}
