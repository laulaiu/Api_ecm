using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_e_commerce.Models
{
    public class Cadastro
    {
        private int id;
        private byte[] imagem;
        private string img_convert;
        private string nome;
        private float preco;
        private int quantidade;
        private string descricao;
        private int categoria;
        private int fk_id_produto;

        string msg;

        private const string conexao = "server=localhost; database =e_comerce; user id=root; password=laulaiu098";

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Fk_id_produto
        {
            get { return fk_id_produto; }
            set { fk_id_produto = value; }
        }

        public byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        public String Img_convert
        {
            get { return img_convert; }
            set { img_convert = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }


        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }


        public String add_produto_Adm()
        {
            MySqlConnection conecta = new MySqlConnection(conexao);

            try
            {
                conecta.Open();
                MySqlCommand query = new MySqlCommand(" insert into produto(nome, preco, quantidade, descricao, categoria) values (@nome, @preco, @quantidade, @descricao, @categoria)", conecta);
                query.Parameters.AddWithValue("@nome", this.nome);
                query.Parameters.AddWithValue("@quantidade", this.quantidade);
                query.Parameters.AddWithValue("@preco", this.preco);
                query.Parameters.AddWithValue("@descricao", this.descricao);
                query.Parameters.AddWithValue("@categoria", this.categoria);
                query.ExecuteNonQuery();

                this.msg = "Produtos salvo com sucesso!\n";

            }
            catch (Exception ae)
            {
                this.msg = "erro" + ae;
            }
            finally
            {
                conecta.Close();
            }

            return msg;

        }

        // estou tentando pegar o id do produto para inserir na chave estrangeira da imagem
        public String get_id_bd()
        {
            MySqlConnection conecta = new MySqlConnection(conexao);

            try
            {
                conecta.Open();

                MySqlCommand retorna_id = new MySqlCommand("select id from produto where nome = @nome and quantidade = @quantidade and preco = @preco and descricao = @descricao", conecta);
                retorna_id.Parameters.AddWithValue("@nome", this.nome);
                retorna_id.Parameters.AddWithValue("@quantidade", this.quantidade);
                retorna_id.Parameters.AddWithValue("@preco", this.preco);
                retorna_id.Parameters.AddWithValue("@descricao", this.descricao);
                MySqlDataReader leitor = retorna_id.ExecuteReader();
                leitor.Read();
                this.id = leitor.GetInt32(0);


                this.msg = "id pego com sucesso!\n " + this.id;

            }
            catch (Exception ae)
            {
                this.msg = "erro" + ae;
            }
            finally
            {
                conecta.Close();
            }

            return this.msg;
        }

        public String add_imagem_adm()
        {
            MySqlConnection conecta = new MySqlConnection(conexao);
            try
            {
                conecta.Open();

                MySqlCommand query = new MySqlCommand("insert into imagem (image, fk_id_produto) values (@foto, @id)", conecta);
                query.Parameters.AddWithValue("@foto", this.imagem);
                query.Parameters.AddWithValue("@id", this.id);
                query.ExecuteNonQuery();

                this.msg = "A imagem foi gravado com sucesso!\n";
            }
            catch (Exception ae)
            {
                this.msg = "erro: " + ae;
            }
            finally
            {
                conecta.Close();
            }

            return this.msg;
        }

        public static List<Cadastro> getProdutos()
        {

            string msg;
            MySqlConnection conecta = new MySqlConnection(conexao);
            List<Cadastro> lista = new List<Cadastro>();


            try
            {
                conecta.Open();

                MySqlCommand produto = new MySqlCommand("select * from produto inner join imagem on produto.id = imagem.fk_id_produto", conecta);
                MySqlDataReader leitor = produto.ExecuteReader();

                while (leitor.Read())
                {
                    Cadastro mw = new Cadastro();

                    mw.Id = int.Parse(leitor["id"].ToString());
                    mw.Nome = leitor["nome"].ToString();
                    mw.Preco = float.Parse(leitor["preco"].ToString());
                    mw.quantidade = int.Parse(leitor["quantidade"].ToString());
                    mw.Descricao = leitor["descricao"].ToString();
                    mw.Categoria = int.Parse(leitor["categoria"].ToString());
                    mw.Id = int.Parse(leitor["id"].ToString());
                    mw.Img_convert = Convert.ToBase64String((byte[])leitor["image"]);
                    mw.Fk_id_produto = int.Parse(leitor["fk_id_produto"].ToString());
                    lista.Add(mw);
                }

            }
            catch (Exception ae)
            {
                msg = "erro" + ae;
            }
            finally
            {
                conecta.Close();
            }

            return lista;

        }

        public String excluir_imagem()
        {

            MySqlConnection conecta = new MySqlConnection(conexao);

            try
            {
                conecta.Open();

                MySqlCommand query = new MySqlCommand(" delete from imagem where fk_id_produto = @id;", conecta);
                query.Parameters.AddWithValue("@id", this.id);
                query.ExecuteNonQuery();


                this.msg = "sucesso";

            }
            catch (Exception ae)
            {
                this.msg = "Erro ao apagar imagem: " + ae;
            }
            finally
            {
                conecta.Close();
            }

            return this.msg;

        }
        public String excluir_produto()
        {

            MySqlConnection conecta = new MySqlConnection(conexao);

            try
            {
                conecta.Open();
                
                MySqlCommand query = new MySqlCommand(" delete from produto where id = @id", conecta);
                query.Parameters.AddWithValue("@id", this.Id);
                query.ExecuteNonQuery();
                this.msg = "sucesso";
                
            }
            catch (Exception ae)
            {
                this.msg = "Erro ao apagar produto: " + ae;
            }
            finally
            {
                conecta.Close();
            }

            return this.msg;

        }




    }
}