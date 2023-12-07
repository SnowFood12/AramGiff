using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("SAN_PHAM")]
    public class SanPham
    {
        [Key]
        public int Id { get; set; }
        public string? Ten { get; set; }
        [Range(1000, int.MaxValue)]
        public int? Gia { get; set; }
        public byte[]? PicData { get; set; }
        public int CuaHangId { get; set; }
        public int LoaiSPId { get; set; }
        public CuaHang? CuaHang { get; set; }
        public LoaiSP? LoaiSP { get; set; }
    }
}
