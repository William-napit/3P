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
    public partial class frmBorwsePurchasing : Form
    {
        public frmBorwsePurchasing()
        {
            InitializeComponent();
        }
        FrmPurchasing tscPurchasing;
        public frmBorwsePurchasing(FrmPurchasing tscPurchasing)
        {
            InitializeComponent();
            this.tscPurchasing = tscPurchasing;
        }

        SqlConnection Conn;
        string constr;
        SqlDataAdapter da;
        SqlCommand cmd;
        string query;
        DataSet ds;
        DataRow dr;
        DataColumn[] dc = new DataColumn[1];


        private void Koneksi()
        {
            try
            {
                constr = "Data Source = localhost; Initial catalog = Logistic; Integrated Security = true";
                Conn = new SqlConnection(constr);
                Conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void frmBorwsePurchasing_Load(object sender, EventArgs e)
        {
            Koneksi();
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            LoadData();
            TampilData();
            
        }

        private void LoadData()
        {
            ds = new DataSet();
            query = $"SELECT PH. No_Pembelian, PH.Tanggal_Pembelian, PH.Kode_Supplier, V.Nama_Supplier + CHAR(13) + CHAR(10) + V.Alamat + CHAR(13) + CHAR(10) + V.NO_HP, PH.Total FROM Header_Purchasing PH INNER JOIN Supplier V ON PH.Kode_Supplier = V.Kode_Supplier WHERE PH.Tanggal_Pembelian BETWEEN '{dtpFrom.Value.ToString("yyyyMMdd")}'AND'{dtpTo.Value.ToString("yyyyMMdd")}'";
            cmd = new SqlCommand(query, Conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "vwPurchasing");
        }

        private void TampilData()
        {
            dgvData.DataSource = ds.Tables["vwPurchasing"];
            dgvData.Columns[0].HeaderText = "Purchase ID";
            dgvData.Columns[1].HeaderText = "Purchase Date";
            dgvData.Columns[2].HeaderText = "Vendor ID";
            dgvData.Columns[3].HeaderText = "Vendor Description";
            dgvData.Columns[4].HeaderText = "Total";
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvData.CurrentCell.RowIndex;

            tscPurchasing.txtPurchaseID.Text = dgvData[0, baris].Value.ToString();
            tscPurchasing.dtpPurchaseDate.Value = DateTime.Parse(dgvData[1, baris].Value.ToString());
            tscPurchasing.txtVendorID.Text = dgvData[2, baris].Value.ToString();
            tscPurchasing.txtVendorDesc.Text = dgvData[3, baris].Value.ToString();
            tscPurchasing.txtTotal.Text = dgvData[4, baris].Value.ToString();
            this.Close();
        }
    }
}
