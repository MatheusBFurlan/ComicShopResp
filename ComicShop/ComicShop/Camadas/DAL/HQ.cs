using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicShop.Camadas.DAL
{
    public class HQ
    {
        private string strCon = Conexao.getConexao();

        public List<MODEL.HQ> Select()
        {
            List<MODEL.HQ> lstHQ = new List<MODEL.HQ>();
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Select * from HQ";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            conexao.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MODEL.HQ hq = new MODEL.HQ();
                    hq.Id__Hq = Convert.ToInt32(reader[0].ToString());
                    hq.Titulo = reader["Titulo"].ToString();
                    hq.Autor = reader["Autor"].ToString();
                    hq.Qtd = Convert.ToInt32(reader["quantidade"].ToString());
                    hq.Valor = Convert.ToDouble(reader["Valor"].ToString());
                    lstHQ.Add(hq);
                }
            }
            catch
            {
                Console.WriteLine("Erro na seleção de produtos!");
            }
            finally
            {
                conexao.Close();
            }
            return lstHQ;
        }

        public void Insert(MODEL.HQ hq)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Insert into HQ values (@titulo, @autor,@quantidade, @valor)";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@titulo", hq.Titulo);
            cmd.Parameters.AddWithValue("@autor", hq.Autor);
            cmd.Parameters.AddWithValue("@quantidade", hq.Qtd);
            cmd.Parameters.AddWithValue("@valor", hq.Valor);
            conexao.Open();

            try
            {
                cmd.ExecuteNonQuery();
                //hq.Qtd++;
            }
            catch 
            {
                Console.WriteLine("Erro ao inserir produto!");
                
            }
            finally
            {
                conexao.Close();
            }
        }

        public void update(MODEL.HQ hq)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Update HQ set titulo=@titulo, ";
            sql += " autor=@autor, quantidade=@quantidade, valor=@valor ";
            sql += " where id=@id";

            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@titulo", hq.Titulo);
            cmd.Parameters.AddWithValue("@autor", hq.Autor);
            cmd.Parameters.AddWithValue("@quantidade", hq.Qtd);
            cmd.Parameters.AddWithValue("@valor", hq.Valor);
            conexao.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Erro na atualização do produto");
            }
            finally
            {
                conexao.Close();
            }
        }

        public void Delete(MODEL.HQ hq)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Delete from HQ where id=@id;";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", hq.Id__Hq);
            conexao.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Erro na Remoção de Produtos!");
            }
            finally
            {
                conexao.Close();
            }
        }

        public List<MODEL.HQ> SelectById(int id)
        {
            List<MODEL.HQ> lstHQ = new List<MODEL.HQ>();
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Select * from HQ where id=@id";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            conexao.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MODEL.HQ HQ = new MODEL.HQ();
                    HQ.Id__Hq = Convert.ToInt32(reader[0].ToString());
                    HQ.Titulo = reader["titulo"].ToString();
                    HQ.Autor = reader["autor"].ToString();
                    HQ.Qtd = Convert.ToInt32(reader["quantidade"].ToString());
                    HQ.Valor = Convert.ToInt32(reader["valor"].ToString());
                    lstHQ.Add(HQ);
                }
            }
            catch
            {
                Console.WriteLine("Deu erro na Seleção de HQ por ID...");
            }
            finally
            {
                conexao.Close();
            }

            return lstHQ;
        }

        public List<MODEL.HQ> SelectByNome(string nome)
        {
            List<MODEL.HQ> lstHQ = new List<MODEL.HQ>();
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Select * from HQ where (titulo like @titulo)";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@titulo", nome.Trim() + "%");
            conexao.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MODEL.HQ HQ = new MODEL.HQ();
                    HQ.Id__Hq = Convert.ToInt32(reader[0].ToString());
                    HQ.Titulo = reader["titulo"].ToString();
                    HQ.Autor = reader["autor"].ToString();
                    HQ.Qtd = Convert.ToInt32(reader["quantidade"].ToString());
                    HQ.Valor = Convert.ToInt32(reader["valor"].ToString());
                    lstHQ.Add(HQ);
                }
            }
            catch
            {
                Console.WriteLine("Deu erro na Seleção de HQ por Nome...");
            }
            finally
            {
                conexao.Close();
            }

            return lstHQ;
        }
    }
}
