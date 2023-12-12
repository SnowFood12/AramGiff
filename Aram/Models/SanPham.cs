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
		[RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
		[StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
		[Required(ErrorMessage = "Tên không được để trống")]
		public string? Ten { get; set; }
        [Range(10000, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 1K")]
        [Required]
		public int? Gia { get; set; }
        public byte[]? PicData { get; set; }
        public int CuaHangId { get; set; }
        public int LoaiSPId { get; set; }
        [DefaultValue(true)]
        public bool TrangThai { get; set; }
        public CuaHang CuaHang { get; set; }
        public LoaiSP LoaiSP { get; set; }
        public virtual ICollection<DonHang_ChiTiet> DonHang_ChiTiets { get; set; }
	} 
}
