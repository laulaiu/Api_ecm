using api_e_commerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace api_e_commerce.Controllers
{
    public class CadastrosController : ApiController
    {
        int msg;
        bool vdd = false;

        [HttpDelete]
        //deleta os produto
        public string delet_prod(int id)
        {
            Cadastro mw = new Cadastro();
            mw.Id = id;
            if((mw.excluir_imagem().Equals("sucesso")) & (mw.excluir_produto().Equals("sucesso")))
            {
                return "sucesso";
            }
            else
            {
                return "falha";
            }
            
        }


        [HttpGet]
        //obtem os produtos
        public List<Cadastro> get_prod()
        {             
            return Cadastro.getProdutos();
        }


        [HttpPost]
/*        public void cadastrar_prod(string nome, string preco, int quantidade, string descricao,
            int categoria, byte[] imagem)*/
        public void cadastrar_prod(object produto)
        {
            
            var prods = JsonConvert.DeserializeObject<Cadastro>(produto.ToString());
            Cadastro mw = new Cadastro();
            mw.Nome = prods.Nome;
            mw.Preco = prods.Preco;
            mw.Quantidade = prods.Quantidade;
            mw.Descricao = prods.Descricao;
            mw.Categoria = prods.Categoria;
            mw.Imagem = prods.Imagem;              

            string msg1 = mw.add_produto_Adm();
            string msg2 = mw.get_id_bd();
            string msg3 = mw.add_imagem_adm();
         
        }



    }
}
