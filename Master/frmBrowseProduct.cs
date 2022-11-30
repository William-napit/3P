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
    public partial class frmBrowseProduct : Form
    {
        public frmBrowseProduct()
        {
            InitializeComponent();
        }

        frmProduct mstProduct;
        public frmBrowseProduct(frmProduct mstProduct)
        {
            InitializeComponent();
            this.mstProduct = mstProduct;
        }

        SqlConnection Conn;
        string constr;
        SqlDataAdapter da;
        SqlCommand cmd;
        string query, subquery;
        DataSet ds;

        private void Koneksi()
        {
            try
            {
                constr = "Data Source = localhost; Initial Catalog = Logistic; Integrated security = true";
                Conn = new SqlConnection(constr);
                Conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TampilData()
        {
            dgvData.DataSource = ds.Tables["Product"];
            dgvData.Columns[0].HeaderText = "Product ID";
            dgvData.Columns[1].HeaderText = "Product Name";
            dgvData.Columns[2].HeaderText = "Unit";
            dgvData.Columns[3].HeaderText = "Purchasing Price";
            dgvData.Columns[4].HeaderText = "Status";
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            lblCount.Text = dgvData.RowCount.ToString();

        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = dgvData.CurrentCell.RowIndex;
            mstProduct.txtProductID.Text = dgvData[0, baris].Value.ToString();
            mstProduct.txtProductName.Text = dgvData[1, baris].Value.ToString();
            mstProduct.cboUnit.SelectedItem = dgvData[2, baris].Value;
            mstProduct.nudPrice.Value = decimal.Parse(dgvData[3, baris].Value.ToString());
            if ((bool)dgvData[4, baris].Value == true)
            {
                mstProduct.rdoActive.Checked = true;
            }
            else
            {
                mstProduct.rdoNonActive.Checked = true;
            }

            this.Close();
        }
        private void CariData(string query)
        {
            ds = new DataSet();
            cmd = new SqlCommand(query, Conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Product");
        }

        private void frmBrowseProduct_Load(object sender, EventArgs e)
        {
            subquery = "SELECT * FROM Product";
            Koneksi();
            CariData(subquery);
            TampilData();
        }
    }
}
