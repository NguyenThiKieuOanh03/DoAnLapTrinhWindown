using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class ThanhVienDAO
    {
        QLBanHangContext db = null;
        public ThanhVienDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinThanhVien> getList()
        {
            return db.ThongTinThanhViens.ToList();
        }
        public ThongTinThanhVien getRow(int MaTV)
        {
            return db.ThongTinThanhViens.Find(MaTV);
        }
        public int getCount()
        {
            return db.ThongTinThanhViens.Count();
        }
        public void Insert(ThongTinThanhVien thongTinThanhVien)
        {
            db.ThongTinThanhViens.Add(thongTinThanhVien);
            db.SaveChanges();
        }
        public void Update(ThongTinThanhVien thongTinThanhVien)
        {
            db.Entry(thongTinThanhVien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinThanhVien thongTinThanhVien)
        {
            db.ThongTinThanhViens.Remove(thongTinThanhVien);
            db.SaveChanges();
        }
        public void Delete(int MaTV)
        {
            ThongTinThanhVien thongTinThanhVien = db.ThongTinThanhViens.Find(MaTV);
            db.ThongTinThanhViens.Remove(thongTinThanhVien);
            db.SaveChanges();
        }
        public ThongTinThanhVien GetRowBySomeProperty(string someValue)
        {
            return db.ThongTinThanhViens.FirstOrDefault(tv => tv.TenDangNhap == someValue);
        }

        public ThongTinThanhVien GetRowBySomeProperty1(string someValue)
        {
            return db.ThongTinThanhViens.FirstOrDefault(tv => tv.Email == someValue);
        }

    }
}
