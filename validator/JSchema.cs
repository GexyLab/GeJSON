using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.validator
{
    public class JSchema : JObject
    {
        public JSchema() { }
        public JSchema(string rawJson) : base(rawJson)
        {
        }

       
    }
}
