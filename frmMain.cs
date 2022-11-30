using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3P
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void invemtoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuMaster_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            stbWaktu.Text = DateTime.Now.ToString();
            timWaktu.Interval = 1000;
            timWaktu.Enabled = true;
        }

        private void mnuTransaksiPurchasing_Click(object sender, EventArgs e)
        {
            Transaksi.FrmPurchasing tscPurchasing = new Transaksi.FrmPurchasing();
            
            tscPurchasing.Show();


        }

        private void mnuTransactionInventory_Click(object sender, EventArgs e)
        {
            
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master.frmProduct mstProduct = new Master.frmProduct();
            mstProduct.Show();
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master.frmVendor mstVendor = new Master.frmVendor();
            mstVendor.Show();
        }
    }
}
