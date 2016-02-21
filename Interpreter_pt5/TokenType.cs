using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    enum TokenType
    {
        UNKNOWN=0,
        Plus,
        Minus,
        Multiply,
        Divide,
        LeftParens,
        RightParens,
        UnaryPlus,
        UnaryMinus,
        Alpha,
        Character,
        Integer,
        EOF,
    }
}
