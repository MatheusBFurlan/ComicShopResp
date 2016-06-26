using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComicShop.Camadas;

namespace ComicShop
{
    public partial class frmCliente : Form
    {
        private char OP = 'X';

        public frmCliente()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Camadas.BLL.Cliente bllCliente = new Camadas.BLL.Cliente();
            dtgCliente.DataSource = bllCliente.Select();

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
            txtNome.Enabled = status;
            txtEndereco.Enabled = status;
            txtCidade.Enabled = status;
            txtEstado.Enabled = status;
            txtAniversario.Enabled = status;

            if(OP != 'E')
            {
                txtId.Text = "-1";
                txtNome.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtEstado.Text = "";
                txtAniversario.Text = "";
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {            
            OP = 'I';
            habilitaCampos(true);
            txtNome.Focus();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Camadas.MODEL.Cliente cliente = new Camadas.MODEL.Cliente();
            Camadas.BLL.Cliente bllCliente = new Camadas.BLL.Cliente();

            cliente.id = Convert.ToInt32(txtId.Text);
            cliente.nome = txtNome.Text;
            cliente.endereco = txtEndereco.Text;
            cliente.cidade = txtCidade.Text;
            cliente.estado = txtEstado.Text;
            cliente.aniversario = Convert.ToDateTime(txtAniversario.Text);

            string msg;
            if (OP == 'I')
                msg = "Deseja confirmar Inserção dos Dados?";
            else
                msg = "Deseja confirmar Alteração dos Dados?";

            DialogResult resp;
            resp = MessageBox.Show(msg, "Gravar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (resp == DialogResult.OK)
            {
                if (OP == 'I')
                    bllCliente.Insert(cliente);
                else
                    bllCliente.Update(cliente);
            }

            dtgCliente.DataSource = bllCliente.Select();


            OP = 'X';
            habilitaCampos(false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            OP = 'X';
            habilitaCampos(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) > 0)
            {
                OP = 'E';
                habilitaCampos(true);
                txtNome.Focus();
            }
            else MessageBox.Show("Não há registro Selecionado");
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) > 0)
            {
                Camadas.MODEL.Cliente cliente = new Camadas.MODEL.Cliente();
                Camadas.BLL.Cliente bllCliente = new Camadas.BLL.Cliente();

                cliente.id = Convert.ToInt32(txtId.Text);
                DialogResult result;
                result = MessageBox.Show("Deseja Remover o cliente Selecionado?",
                                          "Remover Cliente",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    bllCliente.Delete(cliente);
                    MessageBox.Show("Cliente Removido com Sucesso...");
                }
                else MessageBox.Show("Não confirmada Remoção de Cliente...", "Remover");


                dtgCliente.DataSource = bllCliente.Select();
                habilitaCampos(false);
            }
            else MessageBox.Show("Não há registros Selecionados", "Remover");
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgCliente_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtgCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgCliente_DoubleClick(object sender, EventArgs e)
        {
            if (dtgCliente.SelectedRows.Count > 0)
            {
                txtId.Text = dtgCliente.SelectedRows[0].Cells["id"].Value.ToString();
                txtNome.Text = dtgCliente.SelectedRows[0].Cells["nome"].Value.ToString();
                txtEndereco.Text = dtgCliente.SelectedRows[0].Cells["endereco"].Value.ToString();
                txtCidade.Text = dtgCliente.SelectedRows[0].Cells["cidade"].Value.ToString();
                txtEstado.Text = dtgCliente.SelectedRows[0].Cells["estado"].Value.ToString();
                txtAniversario.Text = dtgCliente.SelectedRows[0].Cells["aniversario"].Value.ToString();
            }
        }

        private void btnFiltar_Click(object sender, EventArgs e)
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
            lblBusca.Text = "Informe o Nome: ";
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

            dtgCliente.DataSource = lstCli;
        }
    }
}