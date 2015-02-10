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

        private static Loja loja = new Loja();

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
            string add = "s";
            if (loja.ProdutosCarrinho.Count > 0)
            {
                Console.WriteLine("-> Deseja adicionar um cupom de desconto [s/n]?");
                add = Console.ReadLine();
                while (add.ToUpper() == "S")
                {
                    Console.WriteLine("-> Digite o código do cupom:");
                    if (!loja.buscarDesconto(Console.ReadLine()))
                    {
                        Console.WriteLine("Erro ao calcular o desconto!");
                        do
                        {
                            Console.WriteLine("Deseja tentar novamente [s/n]?");
                            add = Console.ReadLine();
                        } while (add.ToUpper() != "S" && add.ToUpper() != "N");
                    }
                    else
                        break;
                }
                if (add.ToUpper() == "N")
                    loja.retornaPedido(0, loja.ProdutosCarrinho.Sum(x => x.price));
            }
        }
    }
}
