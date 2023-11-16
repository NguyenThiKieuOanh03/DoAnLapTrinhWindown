using QuanLyShopQuanAo.DAO;
using QuanLyShopQuanAo.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopQuanAo
{
    public partial class frmSanPham : Form
    {
        int rowindex = -1;
        QLBanHangContext db = new QLBanHangContext();
        SanPhamDAO sanPhamDAO = new SanPhamDAO();
        private string AddOrEdit = null;
        private byte[] selectedImageBytes;

        public frmSanPham()
        {
            InitializeComponent();
        }
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            bntLuu.Enabled = false;
            btnXoa.Enabled = false;
            ShowAndHidden(false);
            loadSanPham();
            loadLoai();
        }
        private void loadLoai()
        {
            cbLoai.DataSource = db.ThongTinLoais.ToList();
            cbLoai.ValueMember = "MaLoai";
            cbLoai.DisplayMember = "TenLoai";
        }
        private void loadSanPham()
        {
            dgvSanPham.DataSource = db.ThongTinSanPhams.Select(p => new { p.MaSP, p.TenSP, p.Loai,p.DonGia,p.SoLuong,p.ChiTiet,p.Anh }).ToList();
        }
        private void ShowAndHidden(bool show)
        {
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ShowAndHidden(true);
            AddOrEdit = "Add";
            bntLuu.Enabled = true;
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cbLoai.Text = "";
            txtGia.Text = "";
            txtGia.Text = "";
            txtSoLuong.Text = "";
            txtGhiChu.Text = "";
            pbHinh.Image = null;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            AddOrEdit = "Edit";
            txtMaSP.Enabled = false;
            bntLuu.Enabled = true;
        }


        private void bntLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int gia, soluong,ma;
                if (txtMaSP.Text.Length == 0 || !int.TryParse(txtMaSP.Text, out ma))
                {
                    throw new Exception("Mã sản phẩm không được để trống");
                }

                if (txtTenSP.Text.Length.Equals(0))
                {
                    throw new Exception("Tên sản phẩm không được để trống");
                }
                if (txtGia.Text.Length.Equals(0))
                {
                    throw new Exception("Giá không được để trống");
                }
                if (!int.TryParse(txtGia.Text.Trim(), out gia))
                {
                    throw new Exception("Giá nhập không phải số");
                }
                if (!int.TryParse(txtSoLuong.Text.Trim(), out soluong))
                {
                    throw new Exception("Số lượng không phải số");
                }
                if (AddOrEdit == "Add")
                {
                    //Luu vào CSDL
                    SanPhamDAO sanPhamDAO = new SanPhamDAO();
                    ThongTinSanPham sp = new ThongTinSanPham();
                    if (this.checkMaSP(txtMaSP.Text.Trim()) == false)
                    {
                        throw new Exception("Mã sản phẩm đã tồn tại");
                    }

                    sp.MaSP = int.Parse(txtMaSP.Text.Trim());
                    sp.TenSP = txtTenSP.Text.Trim();
                    sp.Loai = cbLoai.Text.Trim();
                    sp.DonGia = Convert.ToInt32(txtGia.Text);
                    sp.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                    sp.ChiTiet = txtGhiChu.Text.Trim();
                    sp.Anh = selectedImageBytes;

                    sanPhamDAO.Insert(sp);
                    db.SaveChanges();
                    loadSanPham();
                }
                if (AddOrEdit == "Edit")
                {
                    //Update
                    int maSP = int.Parse(txtMaSP.Text.Trim());
                    ThongTinSanPham sp = sanPhamDAO.getRow(maSP);
                    sp.MaSP = int.Parse(txtMaSP.Text.Trim());
                    sp.TenSP = txtTenSP.Text.Trim();
                    sp.Loai = cbLoai.Text.Trim();
                    sp.DonGia = Convert.ToInt32(txtGia.Text);
                    sp.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                    sp.ChiTiet = txtGhiChu.Text.Trim();
                    byte[] currentImage = sp.Anh;
                    if (selectedImageBytes != null)
                    {
                        sp.Anh = selectedImageBytes; // Cập nhật thông tin ảnh mới
                    }
                    else
                    {
                        sp.Anh = currentImage; // Giữ nguyên thông tin ảnh cũ
                    }
                    sanPhamDAO.Update(sp);
                    dgvSanPham.DataSource = sanPhamDAO.getList();
                }
                txtMaSP.Text = "";
                txtTenSP.Text = "";
                cbLoai.Text = "";
                txtGia.Text = "";
                txtGia.Text = "";
                txtSoLuong.Text = "";
                txtGhiChu.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public bool checkMaSP(string masp)
        {
            if (dgvSanPham.Rows.Count == 0)
            {
                return true;
            }
            for (int row = 0; row < dgvSanPham.Rows.Count - 1; row++)
            {
                if (dgvSanPham.Rows[row].Cells["MaSP"].Value.ToString() == masp)
                {
                    return false;
                }
            }
            return true;

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            rowindex = e.RowIndex;
            if (e.RowIndex >= 0 && e.RowIndex < dgvSanPham.Rows.Count)
            {
                int index = e.RowIndex;
                txtMaSP.Text = dgvSanPham.Rows[rowindex].Cells["MaSP"].Value.ToString();
                txtTenSP.Text = dgvSanPham.Rows[rowindex].Cells["TenSP"].Value.ToString();
                cbLoai.Text = dgvSanPham.Rows[rowindex].Cells["Loai"].Value.ToString();
                txtGia.Text = dgvSanPham.Rows[rowindex].Cells["DonGia"].Value.ToString();
                txtSoLuong.Text = dgvSanPham.Rows[rowindex].Cells["SoLuong"].Value.ToString();
                txtGhiChu.Text = dgvSanPham.Rows[rowindex].Cells["ChiTiet"].Value.ToString();
                if (dgvSanPham.Rows[rowindex].Cells["Anh"].Value !=null)
                {
                    byte[] imageData = (byte[])dgvSanPham.Rows[rowindex].Cells["Anh"].Value;
                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pbHinh.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbHinh.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pbHinh.Image = null; // Nếu không có hình ảnh, PictureBox sẽ hiển thị rỗng.
                    }
                }
                else
                {
                    pbHinh.Image = null; // Nếu không có hình ảnh, PictureBox sẽ hiển thị rỗng.
                }

            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maSP = int.Parse(txtMaSP.Text.Trim());
            ThongTinSanPham sp = sanPhamDAO.getRow(maSP);
            sanPhamDAO.Delete(sp);
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cbLoai.Text = "";
            txtGia.Text = "";
            txtGia.Text = "";
            txtSoLuong.Text = "";
            txtGhiChu.Text = "";
            pbHinh.Image = null;
            loadSanPham();
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;
                    pbHinh.Image = Image.FromFile(imagePath);
                    selectedImageBytes = File.ReadAllBytes(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string input = txtSearch.Text;
            string TenSP = txtTenSP.Text;
            if (int.TryParse(input, out int number))
            {
                SanPhamDAO sanPhamDAO = new SanPhamDAO();
                List<ThongTinSanPham> foundProducts = sanPhamDAO.TimKiemSanPham(number, string.Empty);
                if (foundProducts.Count > 0)
                {
                    dgvSanPham.DataSource = foundProducts;
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
                    dgvSanPham.DataSource = foundProducts;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = db.ThongTinSanPhams.Select(p => new { p.MaSP, p.TenSP, p.Loai, p.DonGia, p.SoLuong, p.ChiTiet, p.Anh }).ToList();
        }
    }
}
