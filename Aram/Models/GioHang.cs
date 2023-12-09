using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("GIO_HANG")]
    public class GioHang
    {
        [Key]
        public int Id { get; set; }
        [StringLength(15)]
        [ForeignKey("TaiKhoan")]    
		public string? TenTK { get; set; }
        public ICollection<GioHang_ChiTiet>? GioHang_ChiTiets { get; set; }
    }
}
