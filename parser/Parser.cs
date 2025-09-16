using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.parser
{
    public class Parser
    {
        public Lexer _lexer {  get; set; }
        private Token _currentToken {  get; set; }
        private Token _nextToken { get; set; }

        public Parser(Lexer lexer)
        {
            _lexer = lexer;

            _currentToken=new Token(TokenKind.EOF,'\0');
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
                    return MakeJObject();
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
                a.Add(Parse());
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
                o.Add(key, value);
                Next();
            }

            return o;
        }
    } 
}
