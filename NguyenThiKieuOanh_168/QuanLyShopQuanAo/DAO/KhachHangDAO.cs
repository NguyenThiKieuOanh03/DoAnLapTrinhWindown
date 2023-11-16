using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class KhachHangDAO
    {
        QLBanHangContext db = null;
        public KhachHangDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinKhachHang> getList()
        {
            return db.ThongTinKhachHangs.ToList();
        }
        public ThongTinKhachHang getRow(int MaKH)
        {
            return db.ThongTinKhachHangs.Find(MaKH);
        }
        public int getCount()
        {
            return db.ThongTinKhachHangs.Count();
        }
        public void Insert(ThongTinKhachHang thongTinKhachHang)
        {
            db.ThongTinKhachHangs.Add(thongTinKhachHang);
            db.SaveChanges();
        }
        public void Update(ThongTinKhachHang thongTinKhachHang)
        {
            db.Entry(thongTinKhachHang).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinKhachHang thongTinKhachHang)
        {
            db.ThongTinKhachHangs.Remove(thongTinKhachHang);
            db.SaveChanges();
        }
        public void Delete(int MaKH)
        {
            ThongTinKhachHang thongTinKhachHang = db.ThongTinKhachHangs.Find(MaKH);
            db.ThongTinKhachHangs.Remove(thongTinKhachHang);
            db.SaveChanges();
        }
    }
}
