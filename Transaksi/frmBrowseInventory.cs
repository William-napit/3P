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
    public partial class frmBrowseInventory : Form
    {
        public frmBrowseInventory()
        {
            InitializeComponent();
        }

        SqlConnection Conn;
        string constr;
        SqlDataAdapter da;
        SqlCommand cmd;
        string querry;
        DataSet ds;

        private void Koneksi()
        {
            try
            {
                constr = "Data Source = localhost; Initial Catalog = Logistic; Integrated Security = true";
                Conn = new SqlConnection(constr);
                Conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Tampil()
        {
            dgvData.DataSource = ds.Tables["Data_Barang"];
            dgvData.Columns[0].HeaderText = "Kode Barang";
            dgvData.Columns[1].HeaderText = "Nama Barang";
            dgvData.Columns[2].HeaderText = "Qty";

            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
        }

        private void frmBrowseInventory_Load(object sender, EventArgs e)
        {
            Koneksi();

        }
    }
}
