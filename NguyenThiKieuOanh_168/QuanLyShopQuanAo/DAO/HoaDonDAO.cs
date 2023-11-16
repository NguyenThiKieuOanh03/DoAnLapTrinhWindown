using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class HoaDonDAO
    {
        QLBanHangContext db = null;
        public HoaDonDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinHoaDon> getList()
        {
            return db.ThongTinHoaDons.ToList();
        }
        public ThongTinHoaDon getRow(int MaHD)
        {
            return db.ThongTinHoaDons.Find(MaHD);
        }
        public int getCount()
        {
            return db.ThongTinHoaDons.Count();
        }
        public void Insert(ThongTinHoaDon thongTinHoaDon)
        {
            db.ThongTinHoaDons.Add(thongTinHoaDon);
            db.SaveChanges();
        }
        public void Update(ThongTinHoaDon thongTinHoaDon)
        {
            db.Entry(thongTinHoaDon).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinHoaDon thongTinHoaDon)
        {
            db.ThongTinHoaDons.Remove(thongTinHoaDon);
            db.SaveChanges();
        }
        public void Delete(int MaHD)
        {
            ThongTinHoaDon thongTinHoaDon = db.ThongTinHoaDons.Find(MaHD);
            db.ThongTinHoaDons.Remove(thongTinHoaDon);
            db.SaveChanges();
        }
        public List<ThongTinHoaDon> TimKiemHoaDon(int id)
        {
            using (var context = new QLBanHangContext())
            {
                // Thực hiện tìm kiếm sản phẩm dựa trên mã sản phẩm và tên sản phẩm
                var query = context.ThongTinHoaDons.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaHD == id);
                }
                return query.ToList();
            }
        }//Tìm kiếm
    }
}
