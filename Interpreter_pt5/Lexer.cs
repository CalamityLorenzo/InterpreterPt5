using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    class Lexer
    {
        Stream _sourceStream;
        // assume we start at the beginning of the stream
        long _currentPosition = 0;
        readonly string operatorsString = "+-/*()";//&|";
        readonly string otherString = ",.{}@&_#¬`|\\:;'\"~!£$%^=";

        public Lexer(string source)
        {
            // turn the string into a stream
            // trim the ends first
            var streamBytes = Encoding.UTF8.GetBytes(source.Trim());
            _sourceStream = new MemoryStream(streamBytes);
        }

        public Lexer(Stream sourceCode)
        {
            _sourceStream = sourceCode;
            // this may or may not be useful
            _currentPosition = sourceCode.Position;
        }

        public Token GetNextToken()
        {
            // set the position
            _sourceStream.Seek(_currentPosition, SeekOrigin.Begin);
            char currentChar = Char.MinValue;

            using (StreamReader sourceReader = new StreamReader(_sourceStream, Encoding.UTF8, false, 32, true))
            {
                currentChar = (char)sourceReader.Read();

                // cut through all the whitespace
                if (currentChar == ' ')
                {
                    _currentPosition += 1;
                    while (currentChar == ' ')
                    {
                        currentChar = (char)sourceReader.Read();
                        _currentPosition += 1;
                    }

                }

                if (Char.IsNumber(currentChar))
                {
                    var tkn = Token.CreateNumberToken(sourceReader, currentChar);  //new Token(TokenType.Integer, currentChar.ToString());
                    _currentPosition += tkn.Value.Length;
                    return tkn;
                }
                if (Char.IsLetter(currentChar))
                {
                    var tkn = Token.CreateAlphaToken(sourceReader, currentChar);  //new Token(TokenType.Integer, currentChar.ToString());
                    _currentPosition += tkn.Value.Length;
                    return tkn;
                }
                if (this.operatorsString.Contains(currentChar))
                {
                    var tkn = Token.CreateOperatorToken(currentChar); //new Token(TokenType.Integer, currentChar.ToString());
                    _currentPosition += tkn.Value.Length;
                    return tkn;
                }
                if (this.otherString.Contains(currentChar))
                {
                    var tkn = Token.CreateCharacterToken(sourceReader, currentChar);
                    _currentPosition += tkn.Value.Length;
                    return tkn;
                }

                if (sourceReader.EndOfStream)
                {
                    return new Token(TokenType.EOF, "");
                }
            }



            throw new NotImplementedException();
        }
    }
}
