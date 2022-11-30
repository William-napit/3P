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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }



        SqlConnection Conn;
        string constr;
        SqlDataAdapter da;
        SqlCommand cmd;
        string query;
        DataSet ds;
        DataRow  dr;
        DataColumn[] dc =  new DataColumn[1];
        SqlCommandBuilder cb;

        private void Koneksi()
        {
            try
            {
                constr = "Data Source = localhost; Initial Catalog = Logistic; Integrated Security = true";
                Conn = new SqlConnection(constr);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        

        private void LoadData()
        {
            ds = new DataSet();
            query = "SELECT * FROM Product";
            cmd = new SqlCommand(query, Conn);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "Product");
            dc[0] = ds.Tables["Product"].Columns[0];
            ds.Tables["Product"].PrimaryKey = dc;
        }

        private void UpdateData()
        {
            cb = new SqlCommandBuilder(da);
            da = cb.DataAdapter;
            da.Update(ds.Tables["Product"]);
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            Koneksi();
            txtProductID.MaxLength = 6;
            cboUnit.Items.Add("pcs");
            cboUnit.Items.Add("box");
            cboUnit.Items.Add("kg");
            cboUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            nudPrice.Maximum = 50000000;
            nudPrice.Increment = 500;
            nudPrice.ThousandsSeparator = true;
            
        }

        private void Kosong()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            cboUnit.SelectedIndex = -1;
            nudPrice.Value = 0;
            rdoActive.Checked = false;
            rdoNonActive.Checked = false;
            txtProductID.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData();
            dr = ds.Tables["Product"].Rows.Find(txtProductID.Text);
            if (dr == null)
            {
                dr = ds.Tables["Product"].NewRow();
                dr[0] = txtProductID.Text;
                dr[1] = txtProductName.Text;
                dr[2] = cboUnit.SelectedItem;
                dr[3] = nudPrice.Value;
                if (rdoActive.Checked == true)
                {
                    dr[4] = true;
                }
                else
                {
                    dr[4] = false;
                }
                ds.Tables["Product"].Rows.Add(dr);
                UpdateData();
                MessageBox.Show($"Product ID {txtProductID.Text} telah berhasil ditambahkan", "Tambah Barang", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kosong();
            }
            else
            {
                MessageBox.Show($"Product ID {txtProductID.Text} sudah ada di database", "Tambah Produk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            frmBrowseProduct brwProduct = new frmBrowseProduct(this);
            brwProduct.Tag = this;
            brwProduct.Show();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData();
            dr = ds.Tables["Product"].Rows.Find(txtProductID.Text);
            if(dr != null)
            {
                dr[1] = txtProductName.Text;
                dr[2] = cboUnit.SelectedItem;
                dr[3] = nudPrice.Value;
                if (rdoActive.Checked == true)
                {
                    dr[4] = true;
                }
                else
                {
                    dr[4] = false;
                }
                UpdateData();
                MessageBox.Show($"Product ID {txtProductID.Text} berhasil di edti", "Ubah Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kosong();
            }
            else
            {
                MessageBox.Show($"Product ID {txtProductID.Text} tidak ada di database !", "Ubah data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LoadData();
            dr = ds.Tables["Product"].Rows.Find(txtProductID.Text);
            if(dr != null)
            {
                dr.Delete();
                UpdateData();
                MessageBox.Show($"Product ID {txtProductID.Text} telah dihapus dari database","Hapus Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Product ID {txtProductID.Text} tidak ada di database", "Hapus Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Kosong();
        }
    }
}
