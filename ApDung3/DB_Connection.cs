using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApDung3
{
    public class DB_Connection
    {
        string strConn = @"Data Source=M45;Initial Catalog=QLSV;Integrated Security=True";

        public SqlConnection conn = null;

        public SqlConnection GetConn()
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }

            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
                //MessageBox.Show("Mo ket noi thanh cong!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return conn;
        }

        public SqlConnection CloseConn()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                //MessageBox.Show("Dong ket noi thanh cong!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return conn;
        }
    }
}
