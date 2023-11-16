using QuanLyShopQuanAo.DAO;
using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopQuanAo
{
    public partial class frmHoaDon : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        HoaDonDAO hoaDonDAO = new HoaDonDAO();
        KhachHangDAO khachHangDAO = new KhachHangDAO();
        NhanVienDAO nhanVienDAO = new NhanVienDAO();
        private string AddOrEdit = null;
        public frmHoaDon()
        {
            InitializeComponent();
        }
        private void loadHoaDon()
        {
            dgvHoaDon.DataSource = db.ThongTinHoaDons.Select(p => new { p.MaHD, p.NgayLapHD, p.TenSP, p.MaNhanVien,p.TenNhanVien, p.DonGiaBan, p.SoLuongSP, p.MaKH, p.TenKH, p.DiaChi, p.SDT, p.TongTien }).ToList();
        }
        private void loadSanPham()
        {
            cbTenSP.DataSource = db.ThongTinSanPhams.ToList();
            cbTenSP.ValueMember = "MaSP";
            cbTenSP.DisplayMember = "TenSP"; 
            var sanPhams = db.ThongTinSanPhams.ToList();
            cbTenSP.DataSource = sanPhams;
        }
        private void loadKhachHang()
        {
            cbTenKH.DataSource = db.ThongTinKhachHangs.ToList();
            cbTenKH.ValueMember = "MaKH";
            cbTenKH.DisplayMember = "TenKH";
        }
        private void loadNhanVien()
        {
            cbTenNV.DataSource = db.ThongTinNhanViens.ToList();
            cbTenNV.ValueMember = "MaNhanVien";
            cbTenNV.DisplayMember = "TenNhanVien";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            cbMaKH.Enabled = true;
            cbMaNV.Enabled = true;
            txtMaHD.Enabled = true;
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
        }
        private void TinhTongTien()
        {
            int soLuong, donGia;

            if (int.TryParse(txtSoLuong.Text.Trim(), out soLuong) && int.TryParse(txtDonGia.Text.Trim(), out donGia))
            {
                int soLuong1 = Convert.ToInt32(txtSoLuong.Text);
                int dongia1 = Convert.ToInt32(txtDonGia.Text);
                int tongTien = soLuong1 * dongia1;
                
                txtTongTien.Text = tongTien.ToString();
            }
            else
            {
                txtTongTien.Text = "";
            }
        }
        private void cbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenSanPhamDuocChon = cbTenSP.Text;

            ThongTinSanPham sanPhamDuocChon = db.ThongTinSanPhams.FirstOrDefault(p => p.TenSP == tenSanPhamDuocChon);

            if (sanPhamDuocChon != null)
            {
                txtDonGia.Text = sanPhamDuocChon.DonGia.ToString();

            }
            else
            {
                txtDonGia.Text = "";
                txtSoLuong.Text = "";

            }
        }
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }
        private void cbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenNV.SelectedItem != null)
            {

                ThongTinNhanVien selectedNhanVien = (ThongTinNhanVien)cbTenNV.SelectedItem;

                cbMaNV.Text = selectedNhanVien.MaNhanVien.ToString();
                txtDiaChi.Text = selectedNhanVien.DiaChi;
            }
        }


        private void cbTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenKH.SelectedItem != null)
            {

                ThongTinKhachHang selectedKhachHang = (ThongTinKhachHang)cbTenKH.SelectedItem;

                cbMaKH.Text = selectedKhachHang.MaKH.ToString();
                mtxtDienThoai.Text = selectedKhachHang.DienThoai;
            }
        }
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            cbTenNV.SelectedIndexChanged += cbTenNV_SelectedIndexChanged;
            cbTenSP.SelectedIndexChanged += cbTenSP_SelectedIndexChanged;
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;
            txtDonGia.TextChanged += txtDonGia_TextChanged;
            cbTenKH.SelectedIndexChanged += cbTenKH_SelectedIndexChanged;
            dgvHoaDon.DataSource = hoaDonDAO.getList();
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;

            loadSanPham();
            loadHoaDon();
            loadNhanVien();
            loadKhachHang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaHD.Enabled = false;
            cbMaKH.Enabled = false;
            btnLuu.Enabled = true;
            AddOrEdit = "Edit";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int gia, soluong;

                if (txtMaHD.Text.Length.Equals(0))
                {
                    throw new Exception("Mã hóa đơn không được để trống");
                }
                if (dtNgayBan.Text.Length.Equals(0))
                {
                    throw new Exception("Ngày bán không được để trống");
                }

                if (txtDonGia.Text.Length.Equals(0))
                {
                    throw new Exception("Giá bán không được để trống");
                }
                if (!int.TryParse(txtDonGia.Text.Trim(), out gia))
                {
                    throw new Exception("Giá bán không phải số");
                }

                if (!int.TryParse(txtSoLuong.Text.Trim(), out soluong))
                {
                    throw new Exception("Số lượng không phải số");
                }
                if (cbTenSP.Text.Length.Equals(0))
                {
                    throw new Exception("Chon sản phẩm không được để trống");
                }
                if (cbMaKH.Text.Length.Equals(0))
                {
                    throw new Exception("Mã khách hàng  không được để trống");
                }
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    HoaDonDAO hoaDonDAO = new HoaDonDAO();
                    ThongTinHoaDon hd = new ThongTinHoaDon();
                    hd.MaHD = int.Parse(txtMaHD.Text.Trim());
                    hd.NgayLapHD = DateTime.Parse(dtNgayBan.Text);
                    hd.MaNhanVien = int.Parse(cbMaNV.Text.Trim());
                    hd.TenNhanVien = cbTenNV.Text.Trim();
                    hd.MaKH = int.Parse(cbMaKH.Text.Trim());
                    hd.TenKH = cbTenKH.Text.Trim();
                    hd.SDT = mtxtDienThoai.Text.Trim();
                    hd.DiaChi = txtDiaChi.Text.Trim();
                    hd.TenSP = cbTenSP.Text.Trim();
                    hd.DonGiaBan = Convert.ToInt32(txtDonGia.Text);
                    hd.SoLuongSP = Convert.ToInt32(txtSoLuong.Text);
                    hd.TongTien = Convert.ToInt32(txtTongTien.Text);


                    hoaDonDAO.Insert(hd);
                    db.SaveChanges();
                    loadHoaDon();
                    MessageBox.Show("Thêm thành công");
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maHD = int.Parse(txtMaHD.Text.Trim());
                    ThongTinHoaDon hd = hoaDonDAO.getRow(maHD);
                    hd.MaHD = int.Parse(txtMaHD.Text.Trim());
                    hd.NgayLapHD = DateTime.Parse(dtNgayBan.Text);
                    hd.MaNhanVien = int.Parse(cbMaNV.Text.Trim());
                    hd.TenNhanVien = cbTenNV.Text.Trim();
                    hd.MaKH = int.Parse(cbMaKH.Text.Trim());
                    hd.TenKH = cbTenKH.Text.Trim();
                    hd.SDT = mtxtDienThoai.Text.Trim();
                    hd.TenSP = cbTenSP.Text.Trim();
                    hd.DiaChi = txtDiaChi.Text.Trim();
                    hd.DonGiaBan = Convert.ToInt32(txtDonGia.Text);
                    hd.SoLuongSP = Convert.ToInt32(txtSoLuong.Text);
                    hd.TongTien = Convert.ToInt32(txtTongTien.Text);

                    hoaDonDAO.Update(hd);
                    dgvHoaDon.DataSource = hoaDonDAO.getList();
                }
                txtSoLuong.Text = "";
                txtDiaChi.Text = "";
                txtDonGia.Text = "";
                txtMaHD.Text = "";
                cbMaKH.Text = "";
                txtDiaChi.Text = "";
                mtxtDienThoai.Text = "";
                cbTenKH.Text = "";
                txtTongTien.Text = "";
                cbTenNV.Text = "";
                cbTenSP.Text = "";

                dtNgayBan.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaHD.Enabled = false;
                cbTenKH.Enabled = false;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                rowindex = e.RowIndex;

                if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDon.Rows.Count)
                {
                    int index = e.RowIndex;
                    txtMaHD.Text = dgvHoaDon.Rows[rowindex].Cells["MaHD"].Value.ToString();
                    dtNgayBan.Text = dgvHoaDon.Rows[rowindex].Cells["NgayLapHD"].Value.ToString();
                    cbTenNV.Text = dgvHoaDon.Rows[rowindex].Cells["TenNhanVien"].Value.ToString();
                    cbMaKH.Text = dgvHoaDon.Rows[rowindex].Cells["MaKH"].Value.ToString();
                    cbTenKH.Text = dgvHoaDon.Rows[rowindex].Cells["TenKH"].Value.ToString();
                    txtDiaChi.Text = dgvHoaDon.Rows[rowindex].Cells["DiaChi"].Value.ToString();
                    mtxtDienThoai.Text = dgvHoaDon.Rows[rowindex].Cells["SDT"].Value.ToString();
                    cbTenSP.Text = dgvHoaDon.Rows[rowindex].Cells["TenSP"].Value.ToString();
                    txtDonGia.Text = dgvHoaDon.Rows[rowindex].Cells["DonGiaBan"].Value.ToString();
                    txtSoLuong.Text = dgvHoaDon.Rows[rowindex].Cells["SoLuongSP"].Value.ToString();
                    txtTongTien.Text = dgvHoaDon.Rows[rowindex].Cells["TongTien"].Value.ToString();
                    cbMaNV.Text = dgvHoaDon.Rows[rowindex].Cells["MaNhanVien"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtTimKiem.Text;
            if (int.TryParse(input, out int number))
            {
                HoaDonDAO hoaDonDAO = new HoaDonDAO();
                List<ThongTinHoaDon> foundProducts = hoaDonDAO.TimKiemHoaDon(number);
                if (foundProducts.Count > 0)
                {
                    dgvHoaDon.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với ID hoặc tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                SanPhamDAO sanPhamDAO = new SanPhamDAO();
                List<ThongTinSanPham> foundProducts = sanPhamDAO.TimKiemSanPham(0, input);

                if (foundProducts.Count > 0)
                {
                    dgvHoaDon.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = db.ThongTinHoaDons.Select(p => new { p.MaHD, p.NgayLapHD, p.TenSP, p.MaNhanVien, p.TenNhanVien, p.DonGiaBan, p.SoLuongSP, p.MaKH, p.TenKH, p.DiaChi, p.SDT, p.TongTien }).ToList();
        }
    }
}
