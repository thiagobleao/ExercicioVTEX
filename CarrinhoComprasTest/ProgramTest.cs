using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CarrinhoCompras;
using CarrinhoCompras.Models;
using CarrinhoCompras.UserInput;
using NUnit.Framework;

namespace CarrinhoComprasTest
{
    public class ProgramTest
    {
        private Program Program;

        [Test]
        public void RunMain()
        {
            Program.loja.ProdutosCarrinho = new List<Product>();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("45{0}n{0}s{0}www{0}s{0}abc",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format(
                        "-- Bem-vindo ao Carrinho de Compras!{0}{0}" +
                        "-> Digite o ID do produto que deseja adicionar:{0}" + 
                        "{0}O produto Boneca Mônica Patinadora foi adicionado!{0}{0}" + 
                        "-> Deseja adicionar outro produto [s/n]?{0}" + 
                        "-> Deseja adicionar um cupom de desconto [s/n]?{0}" + 
                        "-> Digite o código do cupom:{0}" + 
                        "Cupom não encontrado!{0}" + 
                        "-> Deseja tentar novamente [s/n]?{0}" + 
                        "-> Digite o código do cupom:{0}" + 
                        "{0}O desconto foi aplicado!{0}" +
                        "{0}45        Boneca Mônica Patinadora               149,9{0}" + 
                        "{0}Descontos: 29,98{0}" + 
                        "TOTAL: 119,92{0}" + 
                        "{0}Pressione qualquer tecla para sair.{0}",
                        Environment.NewLine);

                    string output = sw.ToString();
                    Assert.AreEqual(expected, output);
                }
            }
        }

        [Test]
        public void RunMain_no_cupom()
        {
            Program.loja.ProdutosCarrinho = new List<Product>();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("45{0}n{0}n",
                    Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Program.Main(new string[] { });

                    string expected = string.Format(
                        "-- Bem-vindo ao Carrinho de Compras!{0}{0}" + 
                        "-> Digite o ID do produto que deseja adicionar:{0}" +
                        "{0}O produto Boneca Mônica Patinadora foi adicionado!{0}{0}" + 
                        "-> Deseja adicionar outro produto [s/n]?{0}" + 
                        "-> Deseja adicionar um cupom de desconto [s/n]?{0}" +
                        "{0}45        Boneca Mônica Patinadora               149,9{0}" + 
                        "{0}Descontos: 0{0}" + 
                        "TOTAL: 149,9{0}" + 
                        "{0}Pressione qualquer tecla para sair.{0}",
                        Environment.NewLine);

                    string output = sw.ToString();
                    Assert.AreEqual(expected, output);
                }
            }
        }
        
        [Test]
        public void insereProdutos_with_valid_id()
        {
            Program.insereProdutos(new FakeInputs.FakeValidIDUserInput());

            Assert.Greater(Program.loja.ProdutosCarrinho.Count(),0);
        }

        [Test]
        public void insereProdutos_with_invalid_id()
        {
            Program.insereProdutos(new FakeInputs.FakeInvalidIDUserInput());

            Assert.AreEqual(Program.loja.ProdutosCarrinho.Count(), 0);
        }

        [Test]
        public void adicionarNovamente_with_user_yes_input()
        {
            Program.adicionarNovamente(new FakeInputs.FakeYesUserInput());

            Assert.AreEqual(Program.add, "s");
        }

        [Test]
        public void adicionarNovamente_with_user_no_input()
        {
            Program.adicionarNovamente(new FakeInputs.FakeNoUserInput());

            Assert.AreEqual(Program.add, "n");
        }

        [Test]
        public void adicionarNovamente_with_user_wrong_input()
        {
            Program.adicionarNovamente(new FakeInputs.FakeWrongUserInput());

            Assert.AreEqual(Program.add, "aaaaa");
        }

        [Test]
        public void insereDesconto_with_valid_cupom()
        {
            Discount desconto = Program.leCupom(new FakeInputs.FakeValidCupomUserInput());

            Assert.IsNotNull(desconto);
        }

        [Test]
        public void insereDesconto_with_invalid_cupom()
        {
            Discount desconto = Program.leCupom(new FakeInputs.FakeInvalidCupomUserInput());

            Assert.IsNull(desconto);
        }
    }
}
