using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrinhoCompras.Models;
using CarrinhoCompras.UserInput;
using Newtonsoft.Json;

namespace CarrinhoCompras
{
    public class Program
    {

        public static readonly Loja loja = new Loja();

        public static string add = "s";

        private static string message;

        public 
        static void Main(string[] args)
        {
            var input = new Input();
            Console.WriteLine("-- Bem-vindo ao Carrinho de Compras!");

            while (add.ToUpper() == "S")
            {
                Console.WriteLine("-> Digite o ID do produto que deseja adicionar:");
                insereProdutos(input);
                do
                {
                    adicionarNovamente(input);
                } while (add.ToUpper() != "S" && add.ToUpper() != "N");
            }

            if (loja.ProdutosCarrinho.Count > 0)
            {
                message = "-> Deseja adicionar um cupom de desconto [s/n]?";
                do
                {
                    adicionarNovamente(input);
                } while (add.ToUpper() != "S" && add.ToUpper() != "N");

                double totalPedido = loja.ProdutosCarrinho.Sum(x => x.price);
                while (add.ToUpper() == "S")
                {
                    Discount desconto = leCupom(input);
                    if (desconto == null)
                    {
                        Console.WriteLine("Cupom não encontrado!");
                        message = "-> Deseja tentar novamente [s/n]?";
                        adicionarNovamente(input);
                    }
                    else
                    {
                        double valorDesconto = loja.calcularDesconto(desconto, totalPedido);
                        Console.WriteLine(loja.retornaPedido(valorDesconto, totalPedido));
                        break;
                    }
                }
                if (add.ToUpper() == "N")
                    Console.WriteLine(loja.retornaPedido(0, totalPedido));
            }

            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadLine();
        }

        public static void insereProdutos(IUserInput input)
        {
            int id;
            Int32.TryParse(input.GetInput(), out id);
            message = loja.adicionarProduto(id);
        }

        public static void adicionarNovamente(IUserInput input)
        {
            Console.WriteLine(message);
            add = input.GetInput();
        }

        public static Discount leCupom(IUserInput input)
        {
            Console.WriteLine("-> Digite o código do cupom:");
            return loja.buscarDesconto(input.GetInput());
        }
    }
}
