using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrinhoCompras;
using CarrinhoCompras.UserInput;
using NUnit.Framework;

namespace CarrinhoComprasTest
{
    public class InputTest
    {
        private Input input;

        [SetUp]
        public void SetUp()
        {
            input = new Input();
        }

        [Test]
        public void insereProdutos_with_valid_id()
        {
            using (StringReader sr = new StringReader("stringReturn"))
            {
                Console.SetIn(sr);

                string inputReturn = input.GetInput();

                Assert.AreEqual("stringReturn", inputReturn);
            }
        }
    }
}
