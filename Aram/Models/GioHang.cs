using Humanizer.Localisation.TimeToClockNotation;

namespace Aram.Models
{
	public class GioHang
	{
		public List<Giohang_Line>? Lines { get; set; } = new List<Giohang_Line>();
		public void AddItem(SanPham sanpham, int soluong)
		{
			Giohang_Line? line = Lines
				.Where(x => x.SanPham.Id == sanpham.Id)
				.FirstOrDefault();
			if (line == null)
			{
				Lines.Add(new Giohang_Line { SanPham = sanpham, SoLuong = soluong });
			}
			else
			{
				line.SoLuong += soluong;
			}
		}
		public void RemoveSanPham(int Id) => Lines.Remove(Lines.Where(p => p.SanPham.Id == Id).FirstOrDefault());
		public int TamTinh() => (int)Lines.Sum(p => p.SanPham.Gia * p.SoLuong);
		public int TongTien() => (int)(TamTinh() + 20);
		public void Clear() => Lines.Clear();

	}
	public class Giohang_Line
	{
		public int Giohang_Line_Id { get; set; }
		public SanPham SanPham { get; set; } = new();
		public int SoLuong { get; set; }

		public int TongTienSp() => (int)(SoLuong * SanPham.Gia);
	}


}
