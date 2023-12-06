using Microsoft.EntityFrameworkCore;
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
        public string? Name { get; set; }
        [Column(TypeName ="char(10)")]
        public string? SoDT { get; set; }
        [Column(TypeName = "Date")]
        public DateTime NgayTaoCuaHang { get; set; }
        [Column(TypeName = "ntext")]
        public string? DiaChi { get; set; }
        [Column(TypeName = "text")]
        public string? LinkMap { get; set; }
        public virtual ICollection<SanPham>? SanPham { get; set; }
    }
}
