using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    class Interpreter
    {
        readonly Lexer Lexer;
        Token _currentToken = null;
        //char _currentChar = Char.MinValue;
        public Interpreter(string sourceCode)
        {
            // turn the string into a stream
            // trim the ends first
            this.Lexer = new Lexer(sourceCode);
        }

        public void Eat(TokenType ExpectedToken)
        {
            if (this._currentToken.TokenType == TokenType.EOF)
            {
                throw new ArgumentNullException("END OF INPUT!");
            }

            if (this._currentToken.TokenType == ExpectedToken)
            {
                this._currentToken = this.Lexer.GetNextToken();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Syntax Error: Parsing failed");
            }
        }

        int Factor()
        {
            // return an integer term value
            // note eat pushes the token count forward
            if (_currentToken == null)
            {
                _currentToken = this.Lexer.GetNextToken();
            }
            var token = this._currentToken;
            if (token.TokenType == TokenType.Integer)
            {
                this.Eat(TokenType.Integer);
                return int.Parse(token.Value);
            }
            else {
                if (token.TokenType == TokenType.LeftParens)
                {
                    this.Eat(TokenType.LeftParens);
                    var result = this.Expr();
                    this.Eat(TokenType.RightParens);
                    return result;
                }
            }

            throw new ArgumentException($"No factor found, {_currentToken.TokenType}");
        }


        int Term()
        {
            var result = this.Factor();
            while (MathsOperators.IsMultDiv(_currentToken.TokenType))
            {
                Token operation = _currentToken;
                this.Eat(operation.TokenType);
                result = MathsOperators.Operate(operation.TokenType, result, this.Factor());
            }
            return result;
        }

        int Expr()
        {

            // this enforces the structure of mathiness on what we are doing. It expects the following format
            // Term We check for operators with higher precedence (*/ /) and act on them first
            // return the result
            int result = this.Term();

            // OP -- at this point we have moved forward (via Term method) and so should be at an the operator.
            // however if the expression is unary aka '3' there is no next token (EOF), so skip to the end and return the result.s
            while (MathsOperators.IsPlusMinus(_currentToken.TokenType))
            {
                Token operation = _currentToken;
                // moves it forward to the right
                // reassigns current token too.
                this.Eat(operation.TokenType);
                // DO THE MATHS!
                // NOTE we have a Term operation here, just in case there are more terms than expected
                result = MathsOperators.Operate(operation.TokenType, result, this.Term());
            }

            return result;
        }
        // Create's an arthirmeretic expression, and out puts the results
        public int Interpret()
        {
            // start popping tokens!
            // Parser.

            // if we have gotthis far, then we have a recipe for maths!
            return Expr();
        }
    }
}
