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
    public partial class frmKhachHang : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        KhachHangDAO khachHangDAO = new KhachHangDAO();
        private string AddOrEdit = null;
        public frmKhachHang()
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

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            ShowAndHidden(false);
            loadNhanVien();
        }
        private void loadNhanVien()
        {
            dgvKhachHang.DataSource = db.ThongTinKhachHangs.Select(p => new { p.MaKH, p.TenKH, p.DienThoai }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddOrEdit = "Edit";
            txtMaKH.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int ma;
                if (txtMaKH.Text.Length == 0 || !int.TryParse(txtMaKH.Text, out ma))
                {
                    throw new Exception("Mã khách hàng không được để trống");
                }

                if (txtTenKH.Text.Length.Equals(0))
                {
                    throw new Exception("Tên sản phẩm không được để trống");
                }
                if (mtxtDienThoai.Text.Length.Equals(0))
                {
                    throw new Exception("Điện thoại không được để trống");
                }

                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    KhachHangDAO khachHangDAO = new KhachHangDAO();
                    ThongTinKhachHang kh = new ThongTinKhachHang();
                    kh.MaKH = int.Parse(txtMaKH.Text.Trim());
                    kh.TenKH = txtTenKH.Text.Trim();
                    kh.DienThoai = mtxtDienThoai.Text.Trim();
                    khachHangDAO.Insert(kh);
                    db.SaveChanges();
                    loadNhanVien();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maKhachHang = int.Parse(txtMaKH.Text.Trim());
                    ThongTinKhachHang kh = khachHangDAO.getRow(maKhachHang);
                    kh.MaKH = int.Parse(txtMaKH.Text.Trim());
                    kh.TenKH = txtTenKH.Text.Trim();
                    kh.DienThoai = mtxtDienThoai.Text.Trim();


                    khachHangDAO.Update(kh);
                    dgvKhachHang.DataSource = khachHangDAO.getList();
                }
                txtMaKH.Text = "";
                txtTenKH.Text = "";
                mtxtDienThoai.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaKH.Text = dgvKhachHang.Rows[rowindex].Cells["MaKH"].Value.ToString();
                txtTenKH.Text = dgvKhachHang.Rows[rowindex].Cells["TenKH"].Value.ToString();
                mtxtDienThoai.Text = dgvKhachHang.Rows[rowindex].Cells["DienThoai"].Value.ToString();
            }
        }
    }
}
