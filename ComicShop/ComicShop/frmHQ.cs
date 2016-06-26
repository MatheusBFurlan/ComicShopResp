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
    public partial class frmHQ : Form
    {
        private char OP = 'X';

        public frmHQ()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmHQ_Load(object sender, EventArgs e)
        {
            Camadas.BLL.HQ bllHQ = new Camadas.BLL.HQ();
            dtgHQ.DataSource = bllHQ.Select();

            habilitaCampos(false);
        }

        private void habilitaCampos(bool status)
        {
            btnInserir.Enabled = !status;
            btnEditar.Enabled = !status;
            btnRemover.Enabled = !status;
            btnGravar.Enabled = status;
            btnCancelar.Enabled = status;

            txtId.Enabled = false;
            txtTitulo.Enabled = status;
            txtAutor.Enabled = status;
            txtValor.Enabled = status;
            
            if(OP != 'E')
            {
                txtId.Text = "-1";
                txtTitulo.Text = "";
                txtAutor.Text = "";
                txtValor.Text = ""; 
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            OP = 'I';
            habilitaCampos(true);
            txtTitulo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) > 0)
            {
                OP = 'E';
                habilitaCampos(true);
                txtTitulo.Focus();
            }
            else MessageBox.Show("Não há registro selecionado!");

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) > 0)
            {
                Camadas.MODEL.HQ hq = new Camadas.MODEL.HQ();
                Camadas.BLL.HQ bllHQ = new Camadas.BLL.HQ();

                hq.Id__Hq = Convert.ToInt32(txtId.Text);
                DialogResult result;
                result = MessageBox.Show("Deseja remover o produto selecionado",
                                            "Remover produto",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    bllHQ.Delete(hq);
                    MessageBox.Show("Produto removido com sucesso!");
                }
                else MessageBox.Show("Remoção não confirmada");

                dtgHQ.DataSource = bllHQ.Select();
                habilitaCampos(false);
            }
            else MessageBox.Show("Não há registros para remover!", "Remover");
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Camadas.MODEL.HQ hq = new Camadas.MODEL.HQ();
            Camadas.BLL.HQ bllHQ = new Camadas.BLL.HQ();

            hq.Id__Hq = Convert.ToInt32(txtId.Text);
            hq.Titulo = txtTitulo.Text;
            hq.Autor = txtAutor.Text;
            hq.Valor = Convert.ToDouble(txtValor.Text);

            string msg;
            if (OP == 'I')
            {
                msg = "Deseja confirmar a Inserção dos dados";
            }
            else msg = "Deseja confirmar a alteração dos dados";

            DialogResult resp;
            resp = MessageBox.Show(msg, "Gravar",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1);

            if(resp ==  DialogResult.OK)
            {
                if (OP == 'I')
                {
                    bllHQ.Insert(hq);
                }
                else bllHQ.Update(hq);
            }

            dtgHQ.DataSource = bllHQ.Select();

            OP = 'X';
            habilitaCampos(false);

        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            OP = 'X';
            habilitaCampos(false);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgHQ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtgHQ_DoubleClick(object sender, EventArgs e)
        {
            if (dtgHQ.SelectedRows.Count > 0)
            {
                txtId.Text = dtgHQ.SelectedRows[0].Cells["id"].Value.ToString();
                txtTitulo.Text = dtgHQ.SelectedRows[0].Cells["titulo"].Value.ToString();
                txtAutor.Text = dtgHQ.SelectedRows[0].Cells["autor"].Value.ToString();
                txtValor.Text = dtgHQ.SelectedRows[0].Cells["valor"].Value.ToString();
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            pnlBusca.Visible = !pnlBusca.Visible;
        }

        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {
            lblBusca.Visible = false;
            txtBusca.Visible = false;
        }

        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            lblBusca.Text = "Informe o Cogido :";
            txtBusca.Text = "";
            lblBusca.Visible = true;
            txtBusca.Visible = true;
            txtBusca.Focus();
        }

        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {
            lblBusca.Text = "Informe o Titulo: ";
            txtBusca.Text = "";
            lblBusca.Visible = true;
            txtBusca.Visible = true;
            txtBusca.Focus();
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            Camadas.BLL.Cliente bllCliente = new Camadas.BLL.Cliente();
            List<Camadas.MODEL.Cliente> lstCli = new List<Camadas.MODEL.Cliente>();

            if (rdbTodos.Checked)
                lstCli = bllCliente.Select();
            else if (txtBusca.Text != string.Empty)
            {
                if (rdbCodigo.Checked)
                    lstCli = bllCliente.SelectById(Convert.ToInt32(txtBusca.Text));
                else if (rdbNome.Checked)
                    lstCli = bllCliente.SelectByNome(txtBusca.Text);
            }
            else MessageBox.Show("Não foi Informado o Valor de Pesquisa");

            dtgHQ.DataSource = lstCli;
        }
    }
}
