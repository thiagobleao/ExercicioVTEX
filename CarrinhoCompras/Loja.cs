using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CarrinhoCompras.Properties;
using Newtonsoft.Json;

namespace CarrinhoCompras
{
    public class Loja
    {

        public Product[] Produtos;

        public Discount[] Descontos;

        public List<Product> ProdutosCarrinho;

        public Loja()
        {
            ProdutosCarrinho = new List<Product>();
            var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CarrinhoCompras.Resources.products.json"));
            Produtos = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd()).ToArray();

            sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CarrinhoCompras.Resources.discounts.json"));
            Descontos = JsonConvert.DeserializeObject<List<Discount>>(sr.ReadToEnd()).ToArray();
        }

        public string adicionarProduto(int id)
        {
            Product produto = Produtos.FirstOrDefault(x => x.id == id);
            if (produto != null)
            {
                ProdutosCarrinho.Add(produto);
                Console.WriteLine("O produto " + produto.name + " foi adicionado!");
                return "-> Deseja adicionar outro produto [s/n]?";
            }
            Console.WriteLine("Produto não encontrado!");
            return "Deseja tentar novamente [s/n]?";
        }

        public bool buscarDesconto(string cupom)
        {
            Discount desconto = Descontos.FirstOrDefault(x => x.code == cupom.ToUpper());
            if (desconto != null)
            {
                return calcularDesconto(desconto);
            }
            return false;
        }

        public bool calcularDesconto(Discount desconto)
        {
            try
            {
                var totalPedido = ProdutosCarrinho.Sum(x => x.price);
                double valorDesconto;
                if (desconto.type == "percentage")
                    valorDesconto = desconto.amount*0.01*totalPedido;
                else
                    valorDesconto = desconto.amount;
                Console.WriteLine("O desconto foi aplicado!");
                retornaPedido(valorDesconto,totalPedido);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void retornaPedido(double valorDesconto, double totalPedido)
        {
            foreach (var produto in ProdutosCarrinho)
            {
                Console.WriteLine(produto.id + " " + produto.name + " " + produto.price);
            }
            Console.WriteLine("Descontos: " + valorDesconto);
            Console.WriteLine("Total: " + (totalPedido - valorDesconto));
        }
    }
}
