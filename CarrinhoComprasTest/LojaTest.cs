using System;
using System.Linq;
using System.Security.Cryptography;
using CarrinhoCompras;
using NUnit.Framework;

namespace CarrinhoComprasTest
{
    public class LojaTest
    {
        private Loja Loja;

        [SetUp]
        public void SetUp()
        {
            Loja = new Loja();
        }

        [Test]
        public void lerArquivoProdutos_should_return_array()
        {
            Product[] produtos = Loja.lerArquivoProdutos();

            Assert.NotNull(produtos);
            Assert.IsInstanceOf<Product[]>(produtos);
        }

        [Test]
        public void lerArquivoDescontos_should_return_array()
        {
            Discount[] descontos = Loja.lerArquivoDescontos();

            Assert.NotNull(descontos);
            Assert.IsInstanceOf<Discount[]>(descontos);
        }

        [Test]
        public void adicionarProduto_should_insert_valid_id()
        {
            string mensagem = Loja.adicionarProduto(45);

            Assert.IsNotNull(Loja.ProdutosCarrinho.FirstOrDefault());
            Assert.AreEqual("-> Deseja adicionar outro produto [s/n]?",mensagem);
        }

        [Test]
        public void adicionarProduto_should_not_insert_invalid_id()
        {
            string mensagem = Loja.adicionarProduto(1);

            Assert.IsNull(Loja.ProdutosCarrinho.FirstOrDefault());
            Assert.AreEqual("Deseja tentar novamente [s/n]?", mensagem);
        }

        [Test]
        public void buscarDesconto_should_find_valid_coupon()
        {
            Discount desconto = Loja.buscarDesconto("abc");

            Assert.IsNotNull(desconto);
        }

        [Test]
        public void buscarDesconto_should_not_find_invalid_coupon()
        {
            Discount desconto = Loja.buscarDesconto("aaab");

            Assert.IsNull(desconto);
        }

        [Test]
        public void calcularDesconto_when_type_percentage()
        {
            Discount desconto = new Discount();
            desconto.amount = 15;
            desconto.type = "percentage";
            desconto.code = "abc";

            double valorDesconto = Loja.calcularDesconto(desconto,1000);

            double valorDescontoEsperado = desconto.amount*0.01*1000;

            Assert.AreEqual(valorDescontoEsperado,valorDesconto);
        }

        [Test]
        public void calcularDesconto_when_type_fixed()
        {
            Discount desconto = new Discount();
            desconto.amount = 15;
            desconto.type = "fixed";
            desconto.code = "abc";

            double valorDesconto = Loja.calcularDesconto(desconto, 1000);

            double valorDescontoEsperado = desconto.amount;

            Assert.AreEqual(valorDescontoEsperado, valorDesconto);
        }

        [Test]
        public void retornaPedido_should_return_order()
        {
            Loja.adicionarProduto(45);
            double valorDesconto = 15;
            double totalPedido = Loja.ProdutosCarrinho.Sum(x => x.price);

            string pedido = Loja.retornaPedido(15, totalPedido);

            Assert.AreEqual("Total: " + (totalPedido - valorDesconto), pedido);
        }

        [Test]
        public void retornaPedido_should_not_return_order_if_ProdutosCarrinho_empty()
        {
            string pedido = Loja.retornaPedido(15, 0);

            Assert.AreEqual("Erro ao finalizar o pedido, o carrinho está vazio.", pedido);
        }
    }
}
