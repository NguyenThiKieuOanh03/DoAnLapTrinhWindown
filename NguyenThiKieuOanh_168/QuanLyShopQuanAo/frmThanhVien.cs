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
    public partial class frmThanhVien : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        ThanhVienDAO thanhVienDAO = new ThanhVienDAO();
        private string AddOrEdit = null;
        public frmThanhVien()
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

        private void frmThanhVien_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            ShowAndHidden(false);
            loadThanhVien();
        }
        private void loadThanhVien()
        {
            dgvThanhVien.DataSource = db.ThongTinThanhViens.Select(p => new { p.MaTV, p.HoTen, p.Email, p.SDT, p.MatKhau, p.TenDangNhap, p.Role }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaTV.Enabled = true;
            txtHoTen.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddOrEdit = "Edit";
            txtMaTV.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int ma;
                if (txtMaTV.Text.Length == 0 || !int.TryParse(txtMaTV.Text, out ma))
                {
                    throw new Exception("Mã thành viên không được để trống");
                }

                if (txtHoTen.Text.Length.Equals(0))
                {
                    throw new Exception("Tên sản phẩm không được để trống");
                }
                if (mtxtDienThoai.Text.Length.Equals(0))
                {
                    throw new Exception("Số điện thoại không được để trống");
                }
                if (txtEmail.Text.Length.Equals(0))
                {
                    throw new Exception("Email không được để trống");
                }
                if (txtMatKhau.Text.Length.Equals(0))
                {
                    throw new Exception("Mật Khẩu không được để trống");
                }
                if (txtTenDN.Text.Length.Equals(0))
                {
                    throw new Exception("Tên Đăng Nhập không được để trống");
                }


                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    ThanhVienDAO thanhVienDAO = new ThanhVienDAO();
                    ThongTinThanhVien tv = new ThongTinThanhVien();
                    tv.MaTV = int.Parse(txtMaTV.Text.Trim());
                    tv.HoTen = txtHoTen.Text.Trim();
                    tv.Email = txtEmail.Text.Trim();
                    tv.SDT = mtxtDienThoai.Text.Trim();
                    tv.MatKhau = txtMatKhau.Text.Trim();
                    tv.TenDangNhap = txtTenDN.Text.Trim();
                    tv.Role = txtQuyen.Text.Trim();
                    thanhVienDAO.Insert(tv);
                    db.SaveChanges();
                    loadThanhVien();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maThanhVien = int.Parse(txtMaTV.Text.Trim());
                    ThongTinThanhVien tv = thanhVienDAO.getRow(maThanhVien);
                    tv.MaTV = int.Parse(txtMaTV.Text.Trim());
                    tv.HoTen = txtHoTen.Text.Trim();
                    tv.Email = txtEmail.Text.Trim();
                    tv.SDT = mtxtDienThoai.Text.Trim();
                    tv.MatKhau = txtMatKhau.Text.Trim();
                    tv.TenDangNhap = txtTenDN.Text.Trim();
                    tv.Role = txtQuyen.Text.Trim();


                    thanhVienDAO.Update(tv);
                    dgvThanhVien.DataSource = thanhVienDAO.getList();
                }
                txtMaTV.Text = "";
                txtHoTen.Text = "";
                txtEmail.Text = "";
                mtxtDienThoai.Text = "";
                txtMatKhau.Text = "";
                txtTenDN.Text = "";
                txtQuyen.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dgvThanhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvThanhVien.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaTV.Text = dgvThanhVien.Rows[rowindex].Cells["MaTV"].Value.ToString();
                txtHoTen.Text = dgvThanhVien.Rows[rowindex].Cells["HoTen"].Value.ToString();
                txtEmail.Text = dgvThanhVien.Rows[rowindex].Cells["Email"].Value.ToString();
                mtxtDienThoai.Text = dgvThanhVien.Rows[rowindex].Cells["SDT"].Value.ToString();
                txtMatKhau.Text = dgvThanhVien.Rows[rowindex].Cells["MatKhau"].Value.ToString();
                txtTenDN.Text = dgvThanhVien.Rows[rowindex].Cells["TenDangNhap"].Value.ToString();
                txtQuyen.Text = dgvThanhVien.Rows[rowindex].Cells["Role"].Value.ToString();

            }
        }
    }
}
