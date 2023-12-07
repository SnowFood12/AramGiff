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
        public DateTime NgayTaoCuaHang { get; set; }
        [Column(TypeName = "ntext")]
        public string? DiaChi { get; set; }
        [Column(TypeName = "text")]
        public string? LinkMap { get; set; }
        [DefaultValue(true)]
        public bool TrangThai { get; set; }
        public int? TaiKhoanId { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
