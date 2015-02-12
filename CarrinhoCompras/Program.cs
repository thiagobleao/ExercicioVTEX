using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarrinhoCompras
{
    class Program
    {

        private static readonly Loja loja = new Loja();

        private 
        static void Main(string[] args)
        {
            Console.WriteLine("-- Bem-vindo ao Carrinho de Compras!");
            
            insereProdutos();

            insereDesconto();

            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadLine();
        }

        private static void insereProdutos()
        {
            string add = "s";
            while (add.ToUpper() == "S")
            {
                Console.WriteLine("-> Digite o ID do produto que deseja adicionar:");
                int id;
                Int32.TryParse(Console.ReadLine(), out id);
                string message = loja.adicionarProduto(id);
                do
                {
                    Console.WriteLine(message);
                    add = Console.ReadLine();
                } while (add.ToUpper() != "S" && add.ToUpper() != "N");
            }
        }

        private static void insereDesconto()
        {
            string add;
            if (loja.ProdutosCarrinho.Count > 0)
            {
                do
                {
                    Console.WriteLine("-> Deseja adicionar um cupom de desconto [s/n]?");
                    add = Console.ReadLine();
                } while (add.ToUpper() != "S" && add.ToUpper() != "N");

                double totalPedido = loja.ProdutosCarrinho.Sum(x => x.price);

                while (add.ToUpper() == "S")
                {
                    Console.WriteLine("-> Digite o código do cupom:");
                    Discount desconto = loja.buscarDesconto(Console.ReadLine());
                    if (desconto == null)
                    {
                        Console.WriteLine("Cupom não encontrado!");
                        do
                        {
                            Console.WriteLine("Deseja tentar novamente [s/n]?");
                            add = Console.ReadLine();
                        } while (add.ToUpper() != "S" && add.ToUpper() != "N");
                    }
                    else
                    {
                        double valorDesconto = loja.calcularDesconto(desconto,totalPedido);
                        Console.WriteLine(loja.retornaPedido(valorDesconto,totalPedido));
                        break;
                    }
                }
                if (add.ToUpper() == "N")
                   Console.WriteLine( loja.retornaPedido(0,totalPedido));
            }
        }
    }
}
