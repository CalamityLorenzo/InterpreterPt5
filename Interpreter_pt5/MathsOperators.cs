using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    static class MathsOperators
    {
        static Dictionary<TokenType, Func<int, int, int>> Operations = new Dictionary<TokenType, Func<int, int, int>>
         {
          {TokenType.Plus, (x,y)=>x+y },
          {TokenType.Minus, (x,y)=>x-y },
          {TokenType.Multiply, (x,y)=>x*y },
          {TokenType.Divide, (x,y)=>x/y }
        };

        public static int Operate(TokenType type, string left, string right)
        {
            return Operations[type](int.Parse(left), int.Parse(right));
        }

        public static int Operate(TokenType type, int left, int right)
        {
            return Operations[type](left, right);
        }
        // is it a recognised operator
        public static bool IsMathsOperator(TokenType currentType)
        {
            return currentType == TokenType.Plus || currentType == TokenType.Minus
                || currentType == TokenType.Multiply || currentType == TokenType.Divide;
        }

        public static bool IsPlusMinus(TokenType currentType)
        {
            return currentType == TokenType.Plus || currentType == TokenType.Minus;
        }

        public static bool IsMultDiv(TokenType currentType)
        {
            return currentType == TokenType.Divide || currentType == TokenType.Multiply;
        }

        public static bool IsParens(TokenType currentType)
        {
            return currentType == TokenType.LeftParens || currentType == TokenType.RightParens;
        }
    }
}
