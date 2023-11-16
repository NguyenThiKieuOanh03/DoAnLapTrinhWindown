using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class LoaiDAO
    {
        QLBanHangContext db = null;
        public LoaiDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinLoai> getList()
        {
            return db.ThongTinLoais.ToList();
        }
        public ThongTinLoai getRow(int MaLoai)
        {
            return db.ThongTinLoais.Find(MaLoai);
        }
        public int getCount()
        {
            return db.ThongTinLoais.Count();
        }
        public void Insert(ThongTinLoai thongTinLoai)
        {
            db.ThongTinLoais.Add(thongTinLoai);
            db.SaveChanges();
        }
        public void Update(ThongTinLoai thongTinLoai)
        {
            db.Entry(thongTinLoai).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinLoai thongTinLoai)
        {
            db.ThongTinLoais.Remove(thongTinLoai);
            db.SaveChanges();
        }
        public void Delete(int MaLoai)
        {
            ThongTinLoai thongTinLoai = db.ThongTinLoais.Find(MaLoai);
            db.ThongTinLoais.Remove(thongTinLoai);
            db.SaveChanges();
        }
    }
}
