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

namespace ApDung2
{
    public partial class Form1 : Form
    {
        DB_Connection DbConn = new DB_Connection();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DbConn.GetConn();

                string query = @"SELECT COUNT (*) FROM SinhVien";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = DbConn.GetConn();

                int soLuongSV = Convert.ToInt32(cmd.ExecuteScalar());

                MessageBox.Show("So Luong Sinh Vien Trong bang 'SinhVien' la: " + soLuongSV, "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DbConn.CloseConn();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Thong bao",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
