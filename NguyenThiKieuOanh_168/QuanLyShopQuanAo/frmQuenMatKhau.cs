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
    public partial class frmQuenMatKhau : Form
    {
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string passwordnew = txtMatKhau.Text.Trim();
                string enteranewpassword = txtXacNhan.Text.Trim();
                ThanhVienDAO thanhvienDAO = new ThanhVienDAO();
                ThongTinThanhVien tv = thanhvienDAO.GetRowBySomeProperty1(email);
                if (tv == null)
                {
                    throw new Exception("Email không chính xác");
                }
                if (!passwordnew.Equals(enteranewpassword))
                {
                    throw new Exception("Mật khẩu mới không khớp");
                }
                string matkhau = txtMatKhau.Text.Trim();
                tv.MatKhau = matkhau;//Cập nhật mật khẩu
                ThanhVienDAO tvDAO = new ThanhVienDAO();
                tvDAO.Update(tv);
                MessageBox.Show("Cập nhật thành công");
                txtEmail.Clear();
                txtMatKhau.Clear();
                txtXacNhan.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo !");
            }
        }
    }
}
