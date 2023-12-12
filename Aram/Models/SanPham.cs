using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("SAN_PHAM")]
    public class SanPham
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s\u0080-\u00FF\u0102\u0103\u0110\u0111\u0128\u0129\u0168\u0169\u01A0\u01A1\u01AF\u01B0\u1EA0-\u1EF9]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
        [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
		[Required(ErrorMessage = "Tên không được để trống")]
		public string? Ten { get; set; }
        [Range(10000, int.MaxValue, ErrorMessage = "Giá phải lớn hơn 10K")]
        [Required(ErrorMessage = "Giá không được để trống")]
		public int? Gia { get; set; }
        public byte[]? PicData { get; set; }
        public int CuaHangId { get; set; }
        public int LoaiSPId { get; set; }
        public bool TrangThai { get; set; } = true;
        public CuaHang? CuaHang { get; set; }
        public LoaiSP? LoaiSP { get; set; }
        public virtual ICollection<DonHang_ChiTiet>? DonHang_ChiTiets { get; set; }
	} 
}
