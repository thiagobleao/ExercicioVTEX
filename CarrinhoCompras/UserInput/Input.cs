﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrinhoCompras.UserInput
{
    public class Input : IUserInput
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}
