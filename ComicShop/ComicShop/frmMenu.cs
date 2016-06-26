using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComicShop
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCliente frmCli = new frmCliente();
            frmCli.MdiParent = this;
            frmCli.Show();
        }

        private void hQsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHQ frmhq = new frmHQ();
            frmhq.MdiParent = this;
            frmhq.Show();
        }

        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAutor frmAut = new frmAutor();
            frmAut.MdiParent = this;
            frmAut.Show();
        }
    }
}
