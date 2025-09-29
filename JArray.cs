using OpenLab.GeJSON.error;

namespace OpenLab.GeJSON
{
    public class JArray
    {
        #region Variables
        
        List<JPair> content = new List<JPair>();

        public dynamic parentObject;

        #endregion

        #region Base contructors

        public JArray() { }

        public JArray(JObject content)
        {
            Add(content);
        }
        public JArray(JArray content)
        {
            Add(content);
        }
        public JArray(string content)
        {
            Add(content);
        }
        public JArray(byte content)
        {
            Add(content);
        }
        public JArray(short content)
        {
            Add(content);
        }
        public JArray(int content) {
            Add(content);
        }
        public JArray(long content)
        {
            Add(content);
        }
        public JArray(float content)
        {
            Add(content);
        }
        public JArray(double content)
        {
            Add(content);
        }
        public JArray(decimal content)
        {
            Add(content);
        }
        public JArray(bool content)
        {
            Add(content);
        }

        #endregion

        #region Constructor for add array

        public JArray(JObject[] content)
        {
            foreach(JObject item in content) Add(item);
        }
        public JArray(JArray[] content)
        {
            foreach (JArray item in content) Add(item);
        }
        public JArray(string[] content)
        {
            foreach (string item in content) Add(item);
        }
        public JArray(int[] content)
        {
            foreach (int item in content) Add(item);
        }
        public JArray(long[] content)
        {
            foreach (long item in content) Add(item);
        }
        public JArray(float[] content)
        {
            foreach (float item in content) Add(item);
        }
        public JArray(bool[] content)
        {
            foreach (bool item in content) Add(item);
        }

        #endregion

        #region Get propertes

        private JPair Find(int index)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ElementNotFoundException(this, index, "Index " + index + " not found in array(" + Size() + " elements)");
            }
        }
       
        public JPair GetItem(int index)
        {
            try
            {
                return Find(index);
            }
            catch
            {
                throw;
            }
        }

       

        public JPair GetItem(int index, JObject defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, JArray defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, int defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, long defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, float defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, bool defaultValue)
        {
            try
            {
                return Find(index);
            }
            catch 
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair LastItem()
        {
            return content.Last();
        }

        public JPair FirstItem()
        {
            return content.First();
        }

        public List<JPair> GetItems()
        {
            return content;
        }

        #endregion

        #region Get Values

        public dynamic? GetValue(int index)
        {
            try
            {
               return Find(index).Value;
            }
            catch (ElementNotFoundException)
            {
                throw new ElementNotFoundException(this, index, "Index "+index+" not found in array("+Size()+" elements)");
            }
        }

        public dynamic? GetValue(int index, dynamic? defaultValue)
        {
            try
            {
                return Find(index).Value;
            }
            catch (ElementNotFoundException)
            {
                return defaultValue;
            }
        }

        #endregion

        #region get alias

        public dynamic? Get(int index) => GetValue(index);
        public dynamic? Get(int index, dynamic? defaultvalue) => GetValue(index, defaultvalue);
        

        #endregion

        #region set alias

        public JObject Set(int index, JObject value) { return (GetItem(index).Value = value); }
        public JArray Set(int index, JArray value) { return (GetItem(index).Value = value); }
        public string Set(int index, string value) { return (GetItem(index).Value = value); }
        public byte Set(int index, byte value) { return (GetItem(index).Value = value); }
        public float Set(int index, float value) { return (GetItem(index).Value = value); }
        public double Set(int index, double value) { return (GetItem(index).Value = value); }
        public decimal Set(int index, decimal value) { return (GetItem(index).Value = value); }
        public bool Set(int index, bool value) { return (GetItem(index).Value = value); }

        #endregion

        #region Mange methods

        public int Size()
        {
            return content.Count;
        }

        public List<JPair> ToList()
        {
            return content;
        }

        public JPair[] ToArray()
        {
            return content.ToArray();
        }

        public dynamic Parent()
        {
            if (parentObject != null)
            {
                return parentObject;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
      
        public void SetParent(JObject parent)
        {
            if (parentObject != null)
            {
               
                throw new InvalidOperationException("Parent for this object is just set");
            }
            else
            {
                parentObject = parent;
            }
        }
        public void SetParent(JArray parent)
        {
            if (parentObject != null)
            {
                throw new InvalidOperationException("Parent for this object is just set");
                
            }
            else
            {
                parentObject = parent;
            }
        }

        public int Index(JPair pair)
        {
            int r = content.FindIndex(o => o.Equals(pair));
            if (r == -1)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return r;
            }
        }


        #endregion

        #region Add item

        public JPair Add(JObject value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            parentObject = this;
            return p;
        }
        public JPair Add(JArray value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            parentObject = this;
            return p;
        }
        public JPair Add(int value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }
        public JPair Add(long value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }
        public JPair Add(float value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }
        public JPair Add(bool value)
        {
            JPair p = new JPair(null,value);
            content.Add(p);
            return p;
        }
        public JPair Add(string value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }
        public JPair Add(double value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }
        public JPair Add(decimal value)
        {
            JPair p = new JPair(null, value);
            content.Add(p);
            return p;
        }

        #endregion

        #region Remove item

        public void Remove(int index)
        {
            try
            {
                content.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        #endregion

        #region Convertions

        public string Minify()
        {
            string o = "[";
            int i = 0;
            foreach (JPair p in content)
            {
                o += p.Minify();
                if (i < content.Count - 1)
                {
                    o += ",";
                }
                    i++;
            }
            o += "]";

            return o;
        }

        public string ToString(bool deep = true, int leftReturn = 0, string spaceChar = " ")
        {
            string n = Environment.NewLine;
            string s = makeLeftReturn(leftReturn, spaceChar);

            string o = s+"["+n;
            int i = 0;
            foreach (JPair p in content)
            {
                if (p.Value is JObject)
                {
                    o += makeLeftReturn(leftReturn + 1, spaceChar) + "\"" + p.Key + "\" : " + p.GetJsonType();
                }
                else if (p.Value is JArray)
                {
                    o += makeLeftReturn(leftReturn + 1, spaceChar) + "\"" + p.Key + "\" : " + p.GetJsonType();
                }
                else
                {
                    o += p.ToString(deep, leftReturn + 1, spaceChar);
                }

                if (i < content.Count - 1)
                {
                    o += "," + n;
                }
                else
                {
                    o += n;
                }
                i++;
            }
            o += s+"]";

            return o;
        }

        private string makeLeftReturn(int leftReturn = 0, string spaceChar = " ")
        {
            string s = "";
            for (int a = 0; a < leftReturn; a++)
            {
                s += spaceChar;
            }
            return s;
        }

        #endregion
    }
}
