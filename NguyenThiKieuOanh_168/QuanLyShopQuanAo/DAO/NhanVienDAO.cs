using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class NhanVienDAO
    {
        QLBanHangContext db = null;
        public NhanVienDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinNhanVien> getList()
        {
            return db.ThongTinNhanViens.ToList();
        }
        public ThongTinNhanVien getRow(int MaNhanVien)
        {
            return db.ThongTinNhanViens.Find(MaNhanVien);
        }
        public int getCount()
        {
            return db.ThongTinNhanViens.Count();
        }
        public void Insert(ThongTinNhanVien thongTinNhanVien)
        {
            db.ThongTinNhanViens.Add(thongTinNhanVien);
            db.SaveChanges();
        }
        public void Update(ThongTinNhanVien thongTinNhanVien)
        {
            db.Entry(thongTinNhanVien).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinNhanVien thongTinNhanVien)
        {
            db.ThongTinNhanViens.Remove(thongTinNhanVien);
            db.SaveChanges();
        }
        public void Delete(int MaSP)
        {
            ThongTinNhanVien thongTinNhanVien = db.ThongTinNhanViens.Find(MaSP);
            db.ThongTinNhanViens.Remove(thongTinNhanVien);
            db.SaveChanges();
        }
    }
}
