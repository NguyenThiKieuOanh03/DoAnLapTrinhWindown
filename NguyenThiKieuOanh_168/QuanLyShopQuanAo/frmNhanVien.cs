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
    public partial class frmNhanVien : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        NhanVienDAO nhanVienDAO = new NhanVienDAO();
        private string AddOrEdit = null;
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            ShowAndHidden(false);
            loadNhanVien();
        }
        private void loadNhanVien()
        {
            dgvNhanVien.DataSource = db.ThongTinNhanViens.Select(p => new { p.MaNhanVien, p.TenNhanVien, p.NgaySinh, p.DienThoai, p.GioiTinh, p.DiaChi, p.Email }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddOrEdit = "Edit";
            txtMaNV.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int  ma;
                if (txtMaNV.Text.Length == 0 || !int.TryParse(txtMaNV.Text, out ma))
                {
                    throw new Exception("Mã sản phẩm không được để trống");
                }

                if (txtTenNV.Text.Length.Equals(0))
                {
                    throw new Exception("Tên sản phẩm không được để trống");
                }
                if (mtxtDienThoai.Text.Length.Equals(0))
                {
                    throw new Exception("Số điện thoại không được để trống");
                }
                if (txtDiaChi.Text.Length.Equals(0))
                {
                    throw new Exception("Địa chỉ không được để trống");
                }
                if (txtEmail.Text.Length.Equals(0))
                {
                    throw new Exception("Email không được để trống");
                }

                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    NhanVienDAO nhanVienDAO = new NhanVienDAO();
                    ThongTinNhanVien nv = new ThongTinNhanVien();
                    nv.MaNhanVien = int.Parse(txtMaNV.Text.Trim());
                    nv.TenNhanVien = txtTenNV.Text.Trim();
                    nv.NgaySinh = DateTime.Parse(dtNgaySinh.Text);
                    nv.DienThoai = mtxtDienThoai.Text.Trim();
                    nv.GioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";
                    nv.DiaChi = txtDiaChi.Text.Trim();
                    nv.Email = txtEmail.Text.Trim();
                    nhanVienDAO.Insert(nv);
                    db.SaveChanges();
                    loadNhanVien();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maNhanVien = int.Parse(txtMaNV.Text.Trim());
                    ThongTinNhanVien nv = nhanVienDAO.getRow(maNhanVien);
                    nv.MaNhanVien = int.Parse(txtMaNV.Text.Trim());
                    nv.TenNhanVien = txtTenNV.Text.Trim();
                    nv.NgaySinh = DateTime.Parse(dtNgaySinh.Text);
                    nv.DienThoai = mtxtDienThoai.Text.Trim();
                    nv.GioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";
                    nv.DiaChi = txtDiaChi.Text.Trim();
                    nv.Email = txtEmail.Text.Trim();


                    nhanVienDAO.Update(nv);
                    dgvNhanVien.DataSource = nhanVienDAO.getList();
                }
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                dtNgaySinh.Text = "";
                mtxtDienThoai.Text = "";
                txtDiaChi.Text = "";
                txtEmail.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaNV.Text = dgvNhanVien.Rows[rowindex].Cells["MaNhanVien"].Value.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[rowindex].Cells["TenNhanVien"].Value.ToString();
                dtNgaySinh.Text = dgvNhanVien.Rows[rowindex].Cells["NgaySinh"].Value.ToString();
                mtxtDienThoai.Text = dgvNhanVien.Rows[rowindex].Cells["DienThoai"].Value.ToString();
                string gioiTinh = dgvNhanVien.Rows[rowindex].Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    rbtnNam.Checked = true;
                    rbtnNu.Checked = false;
                }
                else if (gioiTinh == "Nữ")
                {
                    rbtnNam.Checked = false;
                    rbtnNu.Checked = true;
                }
                txtDiaChi.Text = dgvNhanVien.Rows[rowindex].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvNhanVien.Rows[rowindex].Cells["Email"].Value.ToString();

            }
        }
    }
}
