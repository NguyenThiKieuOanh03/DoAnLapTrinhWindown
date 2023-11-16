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
    public partial class frmDangNhap : Form
    {
        QLBanHangContext db = new QLBanHangContext();
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtDangNhap.Text.Trim();
            string password = txtMatKhau.Text;
            ThanhVienDAO thanhVienDAO = new ThanhVienDAO();
            ThongTinThanhVien tv = thanhVienDAO.GetRowBySomeProperty(username);

            if (tv == null)
            {
                lblThongBao.Text = "Tài khoản không chính xác !";
            }
            else
            {
                if (tv.MatKhau == password)
                {
                    frmMain.thanhvien = tv; 
                    Form frm = new frmMain();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    lblThongBao.Text = "Tài khoản không chính xác !";
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblQuenMK_Click(object sender, EventArgs e)
        {
            Form frm = new frmQuenMatKhau();
            frm.ShowDialog();
        }
    }
}
