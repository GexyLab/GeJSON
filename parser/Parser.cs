using OpenLab.GeJSON.validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.parser
{
    public class Parser<T> 
    {
        #region Variable 

        public Lexer _lexer {  get; set; }
        private Token _currentToken {  get; set; }
        private Token _nextToken { get; set; }

        #endregion 

        public Parser(string jsonString)
        {
            _lexer = new Lexer(jsonString);

            _currentToken = new Token(TokenKind.EOF, '\0');
            _nextToken = new Token(TokenKind.EOF, '\0');

            Next();
        }

        

        public void Next()
        {
            _currentToken = _nextToken;
            _nextToken = _lexer.Lex();
        }

        public dynamic Parse()
        {
            Next();

            switch (_currentToken.kind)
            {
                case TokenKind.StringLiteral:
                case TokenKind.NumericLiteral:
                case TokenKind.BooleanLiteral:
                    return _currentToken.literal;
                case TokenKind.OpenCurlyBrace: // {
                    if (typeof(T) == typeof(JObject))
                    {
                        return MakeJObject();
                    }
                    else if (typeof(T) == typeof(JSchema))
                    {
                        return MakeJSchema();
                    }
                    else
                    {
                        throw new Exception("Type passed to Parser is wrong. Can be JObject or JSchema");
                    }
                case TokenKind.OpenSquareBrace: // [
                    return MakeJArray();
                default:
                    return string.Empty;
            }
        }

        private JArray MakeJArray()
        {
            JArray a = new JArray();
            while (_currentToken.kind != TokenKind.CloseSquareBrace)
            {
                JPair p = a.Add(Parse());  
                p.ParserToken = _currentToken;
                Next();
            }
            return a;
        }

        private JObject MakeJObject()
        {
            JObject o = new JObject();
            while (_currentToken.kind != TokenKind.CloseCurlyBruce)
            {
                var key = Parse();
                if (_nextToken.kind != TokenKind.Colon) throw new Exception($"Expected a colon char, found {_nextToken.literal}. Line {_nextToken.lineNumber}, char: {_nextToken._charInLinePos}");

                Next(); 
                var value = Parse();
                JPair p = o.Add(key, value);
                p.ParserToken = _currentToken;
                Next();
            }

            return o;
        }

        private JSchema MakeJSchema()
        {
            JSchema o = new JSchema();
            while (_currentToken.kind != TokenKind.CloseCurlyBruce)
            {
                
                var key = Parse();
                if (_nextToken.kind != TokenKind.Colon) throw new Exception($"Expected a colon char, found {_nextToken.literal}. Line {_nextToken.lineNumber}, char: {_nextToken._charInLinePos}");

                Next();
                var value = Parse();
                JPair p = o.Add(key, value);
                p.ParserToken = _currentToken;
                Next();
            }

            return o;
        }
        
        #region Option

        /// <summary>
        /// Enable/disable the convertion of number in scientific notation
        /// </summary>
        /// <param name="val">True to enable or false</param>
        public void enableScientificNotationConvertion(bool val = true)
        {
            _lexer.scentificNotationConvertionEnable(val);
        }

        #endregion
    } 
}
