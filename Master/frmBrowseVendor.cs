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

namespace _3P.Master
{
    public partial class frmBrowseVendor : Form
    {
        public frmBrowseVendor()
        {
            InitializeComponent();
        }
        frmVendor mstVendor;
        public frmBrowseVendor(frmVendor mstVendor)
        {
            InitializeComponent();
            this.mstVendor = mstVendor;
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
            constr = $"Data Source = localhost; Initial Catalog = Logistic; Integrated Security = true";
            Conn = new SqlConnection(constr);
            Conn.Open();
        }

        private void LoadData()
        {
            ds = new DataSet();
            query = "SELECT * FROM Vendor";
            cmd = new SqlCommand(query, Conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Vendor");
            dc[0] = ds.Tables["Vendor"].Columns[0];
            ds.Tables["Vendor"].PrimaryKey = dc;
        }

        private void Tampil()
        {
            dgvData.DataSource = ds.Tables["Vendor"];
            dgvData.Columns[0].HeaderText = "Vendor ID";
            dgvData.Columns[1].HeaderText = "Vendor Name";
            dgvData.Columns[2].HeaderText = "Address";
            dgvData.Columns[3].HeaderText = "Phone";
            dgvData.Columns[4].HeaderText = "Email";
            dgvData.Columns[5].HeaderText = "Status";
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            lblCount.Text = dgvData.RowCount.ToString();
        }
        private void frmBrowseVendor_Load(object sender, EventArgs e)
        {
            Koneksi();
            LoadData();
            Tampil();
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvData.CurrentCell.RowIndex;
            mstVendor.txtVendorID.Text = dgvData.Rows[baris].Cells[0].Value.ToString();
            mstVendor.txtVendorName.Text = dgvData.Rows[baris].Cells[1].Value.ToString();
            mstVendor.txtAddress.Text = dgvData.Rows[baris].Cells[2].Value.ToString();
            mstVendor.txtPhone.Text = dgvData.Rows[baris].Cells[3].Value.ToString();
            mstVendor.txtEmail.Text = dgvData.Rows[baris].Cells[4].Value.ToString();
            if(bool.Parse(dgvData.Rows[baris].Cells[5].Value.ToString())== true)
            {
                mstVendor.rdoActive.Checked = true;
            }
            else
            {
                mstVendor.rdoNonActive.Checked = true;
            }
            this.Close();
            
        }
    }
}
