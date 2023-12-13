using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aram.Models
{
    [Table("LOAI_SP")]   
    public class LoaiSP
    {
        [Key]
        public int Id { get; set; }
		[RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
		[StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
		[Required(ErrorMessage = "Tên không được để trống")]
		public string? Ten { get; set; }
        [DefaultValue(true)]
        public bool TrangThai { get; set; }
        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
