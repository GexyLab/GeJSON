using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.error
{
    public class ElementNotFoundException : Exception
    {
        public JObject element;
        public JArray array;
        public JObject Object;
        public int arrayIndex;
        public ElementNotFoundException() : base(){ }
        public ElementNotFoundException(string Message) : base(Message) { }
        public ElementNotFoundException(JObject element) : base() {
            this.element = element;
        }
        public ElementNotFoundException(JObject element, string Message) : base(Message)
        {
            this.element = element;
        }

        public ElementNotFoundException(JArray array, int index) : base()
        {
            this.array = array;
            this.arrayIndex = index;
        }
        public ElementNotFoundException(JArray array, int index, string Message) : base(Message)
        {
            this.array = array;
            this.arrayIndex = index;
        }
    }
}
