using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopQuanAo.DAO
{
    internal class ketnoi
    {
        public static string str = "data source=DESKTOP-QS17CUM\\SQLEXPRESS;initial catalog=QLBanHang;persist security info=True;user id=sa;password=oanh03";
        public static SqlConnection getConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(str);
                return connection;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối");
                return null;
            }
        }
    }
}
