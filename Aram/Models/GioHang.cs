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
		public void RemoveSanPham(SanPham sanPham) => Lines.Remove(Lines.Where(p => p.SanPham.Id == sanPham.Id).FirstOrDefault());
		public int TongTien() => (int)Lines.Sum(p => p.SanPham.Gia * p.SoLuong);
		public void Clear() => Lines.Clear();

	}
	public class Giohang_Line
	{
		public int Giohang_Line_Id { get; set; }
		public SanPham SanPham { get; set; } = new();
		public int SoLuong { get; set; }
	}
}
