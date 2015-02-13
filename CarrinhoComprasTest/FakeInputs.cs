using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrinhoCompras.UserInput;

namespace CarrinhoComprasTest
{
    public class FakeInputs
    {

        public class FakeYesUserInput : IUserInput
        {
            public string GetInput()
            {
                return "s";
            }
        }

        public class FakeNoUserInput : IUserInput
        {
            public string GetInput()
            {
                return "n";
            }
        }
        public class FakeWrongUserInput : IUserInput
        {
            public string GetInput()
            {
                return "aaaaa";
            }
        }

        public class FakeValidIDUserInput : IUserInput
        {
            public string GetInput()
            {
                return "45";
            }
        }
        public class FakeInvalidIDUserInput : IUserInput
        {
            public string GetInput()
            {
                return "12345";
            }
        }

        public class FakeValidCupomUserInput : IUserInput
        {
            public string GetInput()
            {
                return "abc";
            }
        }
        public class FakeInvalidCupomUserInput : IUserInput
        {
            public string GetInput()
            {
                return "asdf";
            }
        }
    }
}
