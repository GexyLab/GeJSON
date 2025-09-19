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

        public JPair GetItem(int index)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        public JPair GetItemOrNull(int index)
        {
            try
            {
                return content.ElementAt(index) ?? null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public JPair GetItem(int index, JObject defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, JArray defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, int defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, long defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, float defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JPair(null, defaultValue);
            }
        }

        public JPair GetItem(int index, bool defaultValue)
        {
            try
            {
                return content.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
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

        public int GetValue(int index)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        public int? GetValueOrNull(int index)
        {
            try
            {
                return content.ElementAt(index).Value ?? null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public JObject GetValue(int index, JObject defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public JArray GetValue(int index, JArray defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public string GetValue(int index, string defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public byte GetValue(int index, byte defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public short GetValue(int index, short defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public int GetValue(int index, int defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public long GetValue(int index, long defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public float GetValue(int index, float defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public double GetValue(int index, double defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public decimal GetValue(int index, decimal defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

        public bool GetValue(int index, bool defaultValue)
        {
            try
            {
                return content.ElementAt(index).Value;
            }
            catch (ArgumentOutOfRangeException)
            {
                return defaultValue;
            }
        }

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
