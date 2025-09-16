using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.parser
{
    public enum TokenKind
    {
        OpenCurlyBrace,
        CloseCurlyBruce,

        Comma,
        Colon,

        OpenSquareBrace,
        CloseSquareBrace,
        
        BooleanLiteral,

        StringLiteral,
        NumericLiteral,

        EOF
    }
}
