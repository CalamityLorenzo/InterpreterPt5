using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    class Token
    {
        readonly TokenType _tokenType;
        readonly string _value;

        public Token(TokenType type, string value)
        {
            this._tokenType = type;
            this._value = value;
        }

        public TokenType TokenType
        {
            get { return _tokenType; }
        }

        public string Value
        {
            get { return _value; }
        }


        public override string ToString()
        {
            return $"{this.TokenType} {this.Value}";
        }
        // simple factory to stop clutteirng up me logic
        internal static Token CreateOperatorToken(char value)
        {
            switch (value)
            {
                case '+':
                    return new Token(TokenType.Plus, "+");
                case '-':
                    return new Token(TokenType.Minus, "-");
                case '*':
                    return new Token(TokenType.Multiply, "*");
                case '/':
                    return new Token(TokenType.Divide, "/");
                case '(':
                    return new Token(TokenType.LeftParens, "(");
                case ')':
                    return new Token(TokenType.RightParens, ")");
                default:
                    throw new ArgumentOutOfRangeException("Operator Not recoginised");
            }
        }
        // token bucket!!
        internal static Token CreateCharacterToken(char value)
        {
            return new Token(TokenType.Character, value.ToString());
        }

        internal static Token CreateNumberToken(char value)
        {
            return new Token(TokenType.Integer, value.ToString());
        }

        internal static Token CreateNumberToken(StreamReader stream, char currentChar)
        {
            return Token.CreateTokenOfType(stream, TokenType.Integer, currentChar, (chr) => char.IsNumber(chr));
        }

        // only use of we KNOW it's single character
        internal static Token CreateAlphaToken(char value)
        {
            return new Token(TokenType.Alpha, value.ToString());
        }

        internal static Token CreateAlphaToken(StreamReader stream, char currentChar)
        {
            return Token.CreateTokenOfType(stream, TokenType.Alpha, currentChar, (chr) => char.IsLetter(chr));
        }

        internal static Token CreateCharacterToken(StreamReader stream, char currentChar)
        {
            return Token.CreateTokenOfType(stream, TokenType.Alpha, currentChar, (chr) => ",.{}@&_#¬`|\\:;'\"~!£$%^&=".Contains(chr));
        }

        private static Token CreateTokenOfType(StreamReader sreader, TokenType type, char currentChar, Func<char, bool> predicate)
        {
            // iterate through the stream until we don't have a wot we are looking for.
            List<char> chars = new List<char> { currentChar };
          
            while (!sreader.EndOfStream)
            {
                var nextChar = (char)sreader.Read();
                if (predicate(nextChar))
                {
                    chars.Add(nextChar);
                }
                else
                {
                    break;
                }
            }

            return new Token(type, new string(chars.ToArray()));
        }
    }
}
