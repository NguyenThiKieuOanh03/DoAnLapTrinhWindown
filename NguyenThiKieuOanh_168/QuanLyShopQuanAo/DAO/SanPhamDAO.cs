using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyShopQuanAo.DAO
{
    internal class SanPhamDAO
    {
        QLBanHangContext db = null;
        public SanPhamDAO()
        {
            db = new QLBanHangContext();
        }
        public List<ThongTinSanPham> getList()
        {
            return db.ThongTinSanPhams.ToList();
        }
        public ThongTinSanPham getRow(int MaSP)
        {
            return db.ThongTinSanPhams.Find(MaSP);
        }
        public int getCount()
        {
            return db.ThongTinSanPhams.Count();
        }
        public void Insert(ThongTinSanPham thongTinSanPham)
        {
            db.ThongTinSanPhams.Add(thongTinSanPham);
            db.SaveChanges();
        }
        public void Update(ThongTinSanPham thongTinSanPham)
        {
            db.Entry(thongTinSanPham).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(ThongTinSanPham thongTinSanPham)
        {
            db.ThongTinSanPhams.Remove(thongTinSanPham);
            db.SaveChanges();
        }
        public void Delete(int MaSP)
        {
            ThongTinSanPham thongTinSanPham = db.ThongTinSanPhams.Find(MaSP);
            db.ThongTinSanPhams.Remove(thongTinSanPham);
            db.SaveChanges();
        }
        public List<ThongTinSanPham> TimKiemSanPham(int id, string name)
        {
            using (var context = new QLBanHangContext())
            {
                // Thực hiện tìm kiếm sản phẩm dựa trên mã sản phẩm và tên sản phẩm
                var query = context.ThongTinSanPhams.AsQueryable();

                if (id != 0)
                {
                    query = query.Where(p => p.MaSP == id);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.TenSP.Contains(name));
                }



                return query.ToList();
            }
        }//Tìm kiếm
    }
}
