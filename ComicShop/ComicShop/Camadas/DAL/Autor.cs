using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicShop.Camadas.DAL
{
    public class Autor
    {
        private string strCon = Conexao.getConexao();

        public List<MODEL.Autor> Select()
        {
            List<MODEL.Autor> lstAutor = new List<MODEL.Autor>();
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Select * from Autor";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            conexao.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MODEL.Autor aut = new MODEL.Autor();
                    aut.Id = Convert.ToInt32(reader[0].ToString());
                    aut.Nome = reader["Nome"].ToString();
                    lstAutor.Add(aut);
                }
            }
            catch
            {
                Console.WriteLine("Erro na seleção de autores!");
            }
            finally
            {
                conexao.Close();
            }
            return lstAutor;
        }

        public void Insert(MODEL.Autor aut)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Insert into Autor values (@nome)";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@nome", aut.Nome);
            conexao.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Erro ao inserir autor!");
            }
            finally
            {
                conexao.Close();
            }
        }


        public void Update(MODEL.Autor aut)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Update Autor set nome=@nome, ";

            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@nome", aut.Nome);
            conexao.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Erro ao atualizar o autor!");
            }
            finally
            {
                conexao.Close();
            }

        }

        public void Delete(MODEL.Autor aut)
        {
            SqlConnection conexao = new SqlConnection(strCon);
            string sql = "Delete from Autor where id=@id;";
            SqlCommand cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", aut.Id);
            conexao.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Erro na Remoção de Autores!");
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
