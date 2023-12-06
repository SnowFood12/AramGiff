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
        [Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }
        [DefaultValue(true)]
        public bool TrangThai { get; set; }
        public ICollection<SanPham>? SanPhams { get; set; }
    }
}
