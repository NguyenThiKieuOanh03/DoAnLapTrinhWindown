using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyShopQuanAo.DATA
{
    public partial class QLBanHangContext : DbContext
    {
        public QLBanHangContext()
            : base("name=QLBanHangContext")
        {
        }

        public virtual DbSet<ThongTinHoaDon> ThongTinHoaDons { get; set; }
        public virtual DbSet<ThongTinKhachHang> ThongTinKhachHangs { get; set; }
        public virtual DbSet<ThongTinLoai> ThongTinLoais { get; set; }
        public virtual DbSet<ThongTinNhanVien> ThongTinNhanViens { get; set; }
        public virtual DbSet<ThongTinSanPham> ThongTinSanPhams { get; set; }
        public virtual DbSet<ThongTinThanhVien> ThongTinThanhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
