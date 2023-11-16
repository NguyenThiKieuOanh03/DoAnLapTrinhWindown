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
    public partial class frmMain : Form
    {
        public static ThongTinThanhVien thanhvien = null;

        public frmMain()
        {
            InitializeComponent();
        }
        private Form currentFromChild;

        private void openChildForm(Form childForm)
        {
            if (currentFromChild != null)
            {
                currentFromChild.Close();
            }
            currentFromChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSanPham());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhanVien());
        }
        private void btnThanhVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmThanhVien());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            openChildForm(new frmHoaDon());
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLoaiSanPham());

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblTen.Text = thanhvien.HoTen;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Form frm = new frmDangNhap();
            frm.ShowDialog();
        }

    }
}
