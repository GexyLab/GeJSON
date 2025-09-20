using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.parser
{
    public class Lexer
    {
        string _rawJson { get; }

        int _position;
        char _currentChar;
        char _nextChar;
        int _lineNumber=1;
        int _charInLinePos=1;
        bool scentificNotationConvertionEnabled = true;
        public Lexer(string rawJson)
        {
            _rawJson = rawJson ?? throw new ArgumentException(nameof(rawJson));

            _position = -2;
            _currentChar = '\0';
            _nextChar = '\0';

            Next();
        }

        private void Next()
        {
            _charInLinePos++;
            _position++;
            _currentChar = _nextChar;

            if (_position <= (_rawJson.Length - 2))
            {
                _nextChar = _rawJson[_position + 1];
            }
            else
            {
                _nextChar = '\0';
            }
        }

        private void ConsumeWhiteSpace()
        {
            while (_currentChar != '\0' && char.IsWhiteSpace(_currentChar))
            {
                _charInLinePos++;
                Next();
            }
        }

        private void ConsumeNewline()
        {
            while (_currentChar != '\0' && (_currentChar == '\n' || _currentChar == '\r'))
            {
                _lineNumber++;
                Next();
            }
        }

        public Token Lex()
        {
            Next();
            ConsumeWhiteSpace();
            ConsumeNewline();
            var currentChar = _currentChar;
            var charString = currentChar.ToString();

            if (currentChar == '\0')
            {
                return new Token(TokenKind.EOF, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == ':')
            {
                return new Token(TokenKind.Colon, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == '{')
            {
                return new Token(TokenKind.OpenCurlyBrace, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == '}')
            {
                return new Token(TokenKind.CloseCurlyBruce, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == '[')
            {
                return new Token(TokenKind.OpenSquareBrace, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == ']')
            {
                return new Token(TokenKind.CloseSquareBrace, charString, _lineNumber, _charInLinePos);
            }
            else if (currentChar == ',')
            {
                return new Token(TokenKind.Comma, charString, _lineNumber, _charInLinePos);
            }
            else if ((currentChar == 't') || (currentChar == 'T') || (currentChar == 'f') || (currentChar == 'F'))
            {
                return MakeBooleanLiteral();
            }
            else if (currentChar == '"' || currentChar == '\'')
            {
                return MakeStringLiteral();
            }
            else if (char.IsDigit(currentChar))
            {
                return MakeNumericLiteral();
            }
            else if ((currentChar == '\n'))
            {
                _lineNumber++;
                _charInLinePos = 1;
            }

            return new Token(TokenKind.EOF, '\0', _lineNumber, _charInLinePos);
        }

        private Token MakeBooleanLiteral()
        {
            var currentPos = _position;
            while(_currentChar != '\0' && _currentChar != 'e')
            {
                Next();
            }

            var boolValue = _rawJson.Substring(currentPos, _position - currentPos + 1);
            return new Token(TokenKind.BooleanLiteral, bool.Parse(boolValue.ToString()), _lineNumber, _charInLinePos);
        }

        private Token MakeNumericLiteral()
        {
            var currentPos = _position;
            var dots = 0;
            while (_currentChar != '\0' && (
                char.IsDigit(_nextChar) || 
                _nextChar == '.' || 
                _nextChar == '-' || 
                _nextChar == 'e' || 
                _nextChar == 'E' ||
                _nextChar == 'd' ||
                _nextChar == 'D' ||
                _nextChar == 'm' ||
                _nextChar == 'M' ||
                _nextChar == 'f' ||
                _nextChar == 'F' 
                ))
            {
                if (_currentChar == '.') dots++;

                Next();
            }

            if (dots > 1) throw new FormatException($"Ivalid decimal number format. Line {_lineNumber}, char {_charInLinePos}");
            if (dots == 0)
            {
                // interno
                try
                {
                    return new Token(TokenKind.NumericLiteral, byte.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint), _lineNumber, _charInLinePos);
                }
                catch (OverflowException ex) when (ex.Message.Contains("byte"))
                {
                    try
                    {
                        return new Token(TokenKind.NumericLiteral, short.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint), _lineNumber, _charInLinePos);
                    }
                    catch (OverflowException ex1) when (ex1.Message.Contains("Int16"))
                    {
                        try
                        {
                            return new Token(TokenKind.NumericLiteral, int.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint), _lineNumber, _charInLinePos);
                        }
                        catch (OverflowException ex2) when (ex2.Message.Contains("Int32"))
                        {
                            return new Token(TokenKind.NumericLiteral, long.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint), _lineNumber, _charInLinePos);
                        }
                    }
                }
            }
            else
            {
                string literal = _rawJson.Substring(currentPos, _position - currentPos + 1);
                // se è esponenziale, contiene il crattere (-)
                if (literal.Contains('e') || literal.Contains('E'))
                {
                    //Console.WriteLine("esponenziale: E");

                    // verifica tipo esplicito
                    if (literal.Contains('d') || literal.Contains('D'))
                    { //double
                        //Console.WriteLine("esplicito: D");
                        return new Token(TokenKind.NumericLiteral, double.Parse(_rawJson.Substring(currentPos, _position - currentPos), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else if (literal.Contains('f') || literal.Contains('F'))
                    { // float
                        //Console.WriteLine("esplicito: F");
                        return new Token(TokenKind.NumericLiteral, float.Parse(_rawJson.Substring(currentPos, _position - currentPos ), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else if (literal.Contains('m') || literal.Contains('M'))
                    { //decimal
                        //Console.WriteLine("esplicito: M");
                        return new Token(TokenKind.NumericLiteral, decimal.Parse(_rawJson.Substring(currentPos, _position - currentPos ), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else
                    {
                        return new Token(TokenKind.NumericLiteral, decimal.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                }
                else
                {
                    // verifica tipo esplicito

                    if (literal.Contains('d') || literal.Contains('D'))
                    { //double
                        //Console.WriteLine("esplicito: D");
                        return new Token(TokenKind.NumericLiteral, double.Parse(_rawJson.Substring(currentPos, _position - currentPos), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else if (literal.Contains('f') || literal.Contains('F'))
                    { // float
                        //Console.WriteLine("esplicito: F");
                        return new Token(TokenKind.NumericLiteral, float.Parse(_rawJson.Substring(currentPos, _position - currentPos), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else if (literal.Contains('m') || literal.Contains('M'))
                    { //decimal
                        //Console.WriteLine("esplicito: M");
                        return new Token(TokenKind.NumericLiteral, decimal.Parse(_rawJson.Substring(currentPos, _position - currentPos), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                    else
                    {
                        return new Token(TokenKind.NumericLiteral, decimal.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
                    }
                }

                // non esponenziale
                Console.WriteLine("double");
                return new Token(TokenKind.NumericLiteral, double.Parse(_rawJson.Substring(currentPos, _position - currentPos + 1), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), _lineNumber, _charInLinePos);
            }            
        }

        private Token MakeStringLiteral()
        {
            Next();
            var currentPos = _position;

            while (_currentChar != '\0' && _currentChar != '"' && _currentChar != '\'')
            {
                Next();
            }

            var literal = _rawJson.Substring(currentPos, _position - currentPos);
            return new Token(TokenKind.StringLiteral, literal, _lineNumber, _charInLinePos);
        }

        #region option

        public void scentificNotationConvertionEnable(bool val = true)
        {
            scentificNotationConvertionEnabled = val;
        }

        #endregion
    }
}
