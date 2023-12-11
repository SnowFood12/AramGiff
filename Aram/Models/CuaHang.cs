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
        [StringLength(50)]
        public string? Ten { get; set; }
        [Column(TypeName = "char(10)")]
        public string? SoDT { get; set; }
        [Column(TypeName = "Date")]
        public DateTime NgayTaoCuaHang { get; set; } = DateTime.Now;
		[Column(TypeName = "ntext")]
        public string? DiaChi { get; set; }
        [Column(TypeName = "text")]
        public string? LinkMap { get; set; }
        [DefaultValue(1)]
        public bool TrangThai { get; set; } = true;
        [StringLength(15)]
        [ForeignKey("TaiKhoan")]
        public string? TenTK { get; set; }
		public TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
