using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.parser
{
    public record Token(TokenKind kind, dynamic literal, int lineNumber=0, int _charInLinePos=0);
}
