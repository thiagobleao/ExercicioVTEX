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

        private readonly Product[] Products;

        private readonly Discount[] Discounts;

        public List<Product> ProdutosCarrinho;

        public Loja()
        {
            ProdutosCarrinho = new List<Product>();

            Products = lerArquivoProdutos();

            Discounts = lerArquivoDescontos();
        }

        public Product[] lerArquivoProdutos()
        {
            var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CarrinhoCompras.Resources.products.json"));
            return JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd()).ToArray();
        }

        public Discount[] lerArquivoDescontos()
        {
            var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CarrinhoCompras.Resources.discounts.json"));
            return JsonConvert.DeserializeObject<List<Discount>>(sr.ReadToEnd()).ToArray();
        }

        public string adicionarProduto(int id)
        {
            Product produto = Products.FirstOrDefault(x => x.id == id);
            if (produto != null)
            {
                ProdutosCarrinho.Add(produto);
                Console.WriteLine("O produto " + produto.name + " foi adicionado!");
                return "-> Deseja adicionar outro produto [s/n]?";
            }
            Console.WriteLine("Produto não encontrado!");
            return "Deseja tentar novamente [s/n]?";
        }

        public Discount buscarDesconto(string cupom)
        {
            Discount desconto = Discounts.FirstOrDefault(x => x.code == cupom.ToUpper());
            if (desconto != null)
            {
                return desconto;
            }
            return null;
        }

        public double calcularDesconto(Discount desconto, double totalPedido)
        {
            double valorDesconto = 0;
            if (desconto.type == "percentage")
                valorDesconto = desconto.amount*0.01*totalPedido;
            else if (desconto.type == "fixed")
                valorDesconto = desconto.amount;
            Console.WriteLine("O desconto foi aplicado!");
            //retornaPedido(valorDesconto,totalPedido);
            return valorDesconto;
        }

        public string retornaPedido(double valorDesconto, double totalPedido)
        {
            if (ProdutosCarrinho.Count == 0)
                return "Erro ao finalizar o pedido, o carrinho está vazio.";

            foreach (var produto in ProdutosCarrinho)
            {
                Console.WriteLine("{0,0}{1,20}{2,30}",produto.id,produto.name,produto.price);
            }
            Console.WriteLine("Descontos: " + valorDesconto);
            return "Total: " + (totalPedido - valorDesconto);
        }
    }
}
