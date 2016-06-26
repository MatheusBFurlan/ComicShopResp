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
    public partial class frmAutor : Form
    {
        private char OP = 'X';

        public frmAutor()
        {
            InitializeComponent();
        }

        private void Autor_Load(object sender, EventArgs e)
        {
            Camadas.BLL.Autor bllAutor = new Camadas.BLL.Autor();
            dtgAutor.DataSource = bllAutor.Select();

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
            

            if (OP != 'E')
            {
                txtId.Text = "-1";
                txtNome.Text = "";
                
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
            Camadas.MODEL.Autor autor = new Camadas.MODEL.Autor();
            Camadas.BLL.Autor bllAutor = new Camadas.BLL.Autor();

            autor.Id = Convert.ToInt32(txtId.Text);
            autor.Nome = txtNome.Text;
            
            string msg;
            if (OP == 'I')
                msg = "Deseja confirmar Inserção dos Dados?";
            else
                msg = "Deseja confirmar Alteração dos Dados?";

            DialogResult resp;
            resp = MessageBox.Show(msg, "Gravar",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1);

            if (resp == DialogResult.OK)
            {
                if (OP == 'I')
                    bllAutor.Insert(autor);
                else
                    bllAutor.Update(autor);
            }

            dtgAutor.DataSource = bllAutor.Select();


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
                Camadas.MODEL.Autor autor = new Camadas.MODEL.Autor();
                Camadas.BLL.Autor bllAutor = new Camadas.BLL.Autor();

                autor.Id = Convert.ToInt32(txtId.Text);
                DialogResult result;
                result = MessageBox.Show("Deseja Remover o autor Selecionado?",
                                          "Remover autor",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    bllAutor.Delete(autor);
                    MessageBox.Show("Autor Removido com Sucesso...");
                }
                else MessageBox.Show("Não confirmada Remoção de Autor...", "Remover");


                dtgAutor.DataSource = bllAutor.Select();
                habilitaCampos(false);
            }
            else MessageBox.Show("Não há registros Selecionados", "Remover");
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgAutor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgAutor.SelectedRows.Count > 0)
            {
                txtId.Text = dtgAutor.SelectedRows[0].Cells["id"].Value.ToString();
                txtNome.Text = dtgAutor.SelectedRows[0].Cells["nome"].Value.ToString();
            }
        }
    }
}
