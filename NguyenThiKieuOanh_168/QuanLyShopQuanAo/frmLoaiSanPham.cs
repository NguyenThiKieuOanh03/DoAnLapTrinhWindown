using QuanLyShopQuanAo.DAO;
using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopQuanAo
{
    public partial class frmLoaiSanPham : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        LoaiDAO loaiDAO = new LoaiDAO();
        private string AddOrEdit = null;
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            ShowAndHidden(false);
            loadLoaiSP();
        }
        private void loadLoaiSP()
        {
            dgvLoai.DataSource = db.ThongTinLoais.Select(p => new { p.MaLoai, p.TenLoai }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaLoai.Enabled = true;
            txtTenLoai.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddOrEdit = "Edit";
            if (txtMaLoai.Text.Length > 0)
            {
                ShowAndHidden(true);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int ma;
                if (txtMaLoai.Text.Length == 0 || !int.TryParse(txtMaLoai.Text, out ma))
                {
                    throw new Exception("Mã loại không được để trống");
                }

                if (txtMaLoai.Text.Length == 0)
                {
                    throw new Exception("Tên loại không được để trống");
                }
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    LoaiDAO loaiDAO = new LoaiDAO();
                    ThongTinLoai l = new ThongTinLoai();
                    l.MaLoai = int.Parse(txtMaLoai.Text.Trim());
                    l.TenLoai = txtTenLoai.Text.Trim();
                    loaiDAO.Insert(l);
                    db.SaveChanges();
                    loadLoaiSP();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maLoai = int.Parse(txtMaLoai.Text.Trim());
                    ThongTinLoai ls = loaiDAO.getRow(maLoai);
                    ls.MaLoai = int.Parse(txtMaLoai.Text.Trim());
                    ls.TenLoai = txtTenLoai.Text.Trim();
                    loaiDAO.Update(ls);
                    dgvLoai.DataSource = loaiDAO.getList();
                }
                txtMaLoai.Text = "";
                txtTenLoai.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvLoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvLoai.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaLoai.Text = dgvLoai.Rows[rowindex].Cells["MaLoai"].Value.ToString();
                txtTenLoai.Text = dgvLoai.Rows[rowindex].Cells["TenLoai"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int MaLoai = Convert.ToInt16(txtMaLoai.Text);
            ThongTinLoai ls = loaiDAO.getRow(MaLoai);
            loaiDAO.Delete(ls);
            //dgvDanhSach.DataSource = loaiSachDAO.getList();
            loadLoaiSP();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không", "Thông báo",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
