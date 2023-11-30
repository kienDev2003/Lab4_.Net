using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApDung4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string StrConn = @"Data Source=M45;Initial Catalog=QLSV;Integrated Security=True";
        private SqlConnection conn = null;
        int chucNang = 0;

        private SqlConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(StrConn);
            }
            if(conn != null && conn.State==ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }

        private SqlConnection CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return conn;
        }

        private SqlDataReader Reader(string query)
        {
            GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection= conn;

            SqlDataReader reader= cmd.ExecuteReader();
            return reader;
        }

        private int Command(string query)
        {
            GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = conn;

            int check = cmd.ExecuteNonQuery();
            return check;
        }

        private void LoadList()
        {
            lsvDanhSach.Items.Clear();
            GetConnection();

            string query = @"SELECT * FROM SinhVien";
            SqlDataReader reader = Reader(query);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string maSV = reader.GetString(0);
                    string tenSV = reader.GetString(1);
                    string gioiTinh = reader.GetString(2);
                    string ngaySinh = reader.GetDateTime(3).ToString("MM/dd/yyyy");
                    string queQuan = reader.GetString(4);
                    string maLop = reader.GetString(5);

                    ListViewItem lvi = new ListViewItem(maSV);
                    lvi.SubItems.Add(tenSV);
                    lvi.SubItems.Add(ngaySinh);
                    lvi.SubItems.Add(gioiTinh);
                    lvi.SubItems.Add(queQuan);
                    lvi.SubItems.Add(maLop);

                    lsvDanhSach.Items.Add(lvi);
                }
                reader.Close();
            }
            else
            {
                reader.Close();
                MessageBox.Show("Danh sách sinh viên không có dữ liệu!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CloseConnection();
        }

        private void TimKiemTheoMaSV(string MaSVTk)
        {
            lsvDanhSach.Items.Clear();
            GetConnection();

            string query = $"SELECT * FROM SinhVien WHERE MaSV = N'{MaSVTk}'";
            SqlDataReader reader = Reader(query);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string maSV = reader.GetString(0);
                    string tenSV = reader.GetString(1);
                    string gioiTinh = reader.GetString(2);
                    string ngaySinh = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    string queQuan = reader.GetString(4);
                    string maLop = reader.GetString(5);

                    ListViewItem lvi = new ListViewItem(maSV);
                    lvi.SubItems.Add(tenSV);
                    lvi.SubItems.Add(ngaySinh);
                    lvi.SubItems.Add(gioiTinh);
                    lvi.SubItems.Add(queQuan);
                    lvi.SubItems.Add(maLop);

                    lsvDanhSach.Items.Add(lvi);
                }
                reader.Close();
            }
            else
            {
                reader.Close();
                MessageBox.Show($"Không tìm thấy sinh viên có Mã: {MaSVTk}", "Thông báo");
                txtTkMaSV.Text = "";
                LoadList();
            }

            CloseConnection();
        }

        private void TimKiemTheoTenSV(string TenSVTk)
        {
            lsvDanhSach.Items.Clear();
            GetConnection();

            string query = $"SELECT * FROM SinhVien WHERE TenSV LIKE N'%{TenSVTk}%'";
            SqlDataReader reader = Reader(query);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string maSV = reader.GetString(0);
                    string tenSV = reader.GetString(1);
                    string gioiTinh = reader.GetString(2);
                    string ngaySinh = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    string queQuan = reader.GetString(4);
                    string maLop = reader.GetString(5);

                    ListViewItem lvi = new ListViewItem(maSV);
                    lvi.SubItems.Add(tenSV);
                    lvi.SubItems.Add(ngaySinh);
                    lvi.SubItems.Add(gioiTinh);
                    lvi.SubItems.Add(queQuan);
                    lvi.SubItems.Add(maLop);

                    lsvDanhSach.Items.Add(lvi);
                }
                reader.Close();
            }
            else
            {
                reader.Close();
                MessageBox.Show($"Không tìm thấy sinh viên có Tên: {TenSVTk}", "Thông báo");
                txtTkTenSV.Text = "";
                LoadList();
            }

            CloseConnection();
        }

        private void ResetTextBox()
        {
            txtMaSV.Text = "";
            txtTenSV.Text = "";
            dtpNgaySinh.Text = DateTime.Now.ToString("MM/dd/yyyy");
            cboGioiTinh.SelectedIndex = -1;
            txtQueQuan.Text = "";
            txtMaLop.Text = "";
        }

        private void InsertSinhVien()
        {
            string maSV = txtMaSV.Text;
            string tenSV = txtTenSV.Text;
            string ngaySinh = dtpNgaySinh.Text;
            string gioiTinh = cboGioiTinh.Text;
            string queQuan = txtQueQuan.Text;
            string maLop = txtMaLop.Text;


        }

        private void UpdateSinhVien()
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lsvDanhSach.Items.Clear();
            gbThongTinChiTiet.Enabled = false;
            //btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadList();
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bajn thật sụ muốn thoát ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lsvDanhSach.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lsvDanhSach.SelectedItems[0];

                string maSV = lvi.SubItems[0].Text;
                string tenSV = lvi.SubItems[1].Text;
                string ngaySinh = lvi.SubItems[2].Text;
                string gioiTinh = lvi.SubItems[3].Text;
                string queQuan = lvi.SubItems[4].Text;
                string maLop = lvi.SubItems[5].Text;

                txtMaSV.Text = maSV;
                txtTenSV.Text = tenSV;
                dtpNgaySinh.Text = ngaySinh;
                txtGioiTinh.Text= gioiTinh;
                txtQueQuan.Text = queQuan;
                txtMaLop.Text = maLop;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maSVTk = txtTkMaSV.Text.Trim();
            string tenSVTk = txtTkTenSV.Text.Trim();

            if(maSVTk != "" && tenSVTk=="")
            {
                TimKiemTheoMaSV(maSVTk);
            }
            else if(maSVTk == "" && tenSVTk != "")
            {
                TimKiemTheoTenSV(tenSVTk);
            }
            else if(maSVTk!="" && tenSVTk != "")
            {
                TimKiemTheoMaSV(maSVTk);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập nội dung tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadList();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetTextBox();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            chucNang = 1;
            gbThongTinChiTiet.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(chucNang == 1)
            {
                InsertSinhVien();
            }else if(chucNang == 2)
            {
                UpdateSinhVien();
            }
        }
    }
}
