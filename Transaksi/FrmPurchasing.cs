using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _3P.Transaksi
{
    public partial class FrmPurchasing : Form
    {
        public FrmPurchasing()
        {
            InitializeComponent();
        }

        SqlConnection Conn;
        string constr;
        SqlDataAdapter da;
        SqlCommand cmd;
        string querry;
        DataSet ds;
        DataRow dr;
        DataColumn[] dc1 = new DataColumn[1];
        DataColumn[] dc2 = new DataColumn[2];
        SqlCommandBuilder cb;

        private void Koneksi()
        {
            try
            {
                constr = "Data source = localhost; initial Catalog = Latihan2_3 Integrated Security = true";
                Conn = new SqlConnection(constr);
                Conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void FrmPurchasing_Load(object sender, EventArgs e)
        {
            Koneksi();
            txtPurchaseID.MaxLength = 6;
            dgvData.ColumnCount = 6;
            dgvData.Columns[0].HeaderText = "Product ID";
            dgvData.Columns[1].HeaderText = "Product Name";
            dgvData.Columns[2].HeaderText = "Qty";
            dgvData.Columns[5].HeaderText = "Subtotal";
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
        }

        private void btnBrowsePurchasing_Click(object sender, EventArgs e)
        {
            frmBorwsePurchasing brwPurchasing = new frmBorwsePurchasing(this);
            brwPurchasing.ShowDialog();
        }
    }
}
