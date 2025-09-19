using OpenLab.GeJSON.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.validator
{
    public class ValidatorException(string Message) : Exception(Message)
    {
      

        public Token parserToken { get; set; }
        public dynamic jsonEntity { get; set; }

        public override string ToString()
        {
            string o = "";

            if (jsonEntity != null)
            {
                o += jsonEntity.ToString(false);
                o += Environment.NewLine;
                o += Environment.NewLine;
            }

            if (parserToken != null)
            {
                o += parserToken.ToString();
                o += Environment.NewLine;
                o += Environment.NewLine;
            }
            
            o += base.ToString();

            return o;
        }

    }
}
