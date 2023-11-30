using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApDung3
{
    public partial class Form1 : Form
    {
        DB_Connection DbConn = new DB_Connection();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnXemTT_Click(object sender, EventArgs e)
        {
            string MaSV = txtMaSV.Text.Trim();
            string HoTen = "";
            string NgaySinh = "";
            string GioiTinh = "";
            string QueQuan = "";
            string MaLop = "";

            string query = $"SELECT * FROM SinhVien WHERE MaSV = '{MaSV}'";

            DbConn.GetConn();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = DbConn.GetConn();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    HoTen = reader.GetString(1);
                    NgaySinh = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    QueQuan = reader.GetString(4);
                    GioiTinh = reader.GetString(2);
                    MaLop = reader.GetString(5);

                    txtHoTen.Text = HoTen;
                    txtGioiTinh.Text = GioiTinh;
                    txtNgaySinh.Text = NgaySinh;
                    txtQueQuan.Text = QueQuan;
                    txtMaLop.Text = MaLop;
                }
                else
                {
                    MessageBox.Show("Doc du lieu bi loi!");
                }

                reader.Close();
            }
            else
            {
                txtHoTen.Text = HoTen;
                txtGioiTinh.Text = GioiTinh;
                txtNgaySinh.Text = NgaySinh;
                txtQueQuan.Text = QueQuan;
                txtMaLop.Text = MaLop;
                MessageBox.Show("Khong tim thay sinh vien. Kiem tra lai ma sinh vien!");
            }

            DbConn.CloseConn();
        }
    }
}
