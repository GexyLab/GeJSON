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

        public JArray(JObject content) {
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

        #region Get value

        public JPair Get(int index)
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

        public JPair Get(int index, JObject defaultValue)
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

        public JPair Get(int index, JArray defaultValue)
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

        public JPair Get(int index, int defaultValue)
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

        public JPair Get(int index, long defaultValue)
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

        public JPair Get(int index, float defaultValue)
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

        public JPair Get(int index, bool defaultValue)
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

        public JPair last()
        {
            return content.Last();
        }

        public JPair first()
        {
            return content.First();
        }

        #endregion

        #region Mange methods

        public int size()
        {
            return content.Count;
        }

        public List<JPair> toList()
        {
            return content;
        }

        public JPair[] toArray()
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

        public JPair Add(object value)
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
                o += p.ToSerializedString();
                if (i < content.Count - 1)
                {
                    o += ",";
                }
                    i++;
            }
            o += "]";

            return o;
        }

        public string ToString(int leftReturn = 0, string spaceChar = " ")
        {
            string n = Environment.NewLine;
            string s = "";
            for (int a = 0; a < leftReturn; a++)
            {
                s += spaceChar;
            }

            string o = s+"["+n;
            int i = 0;
            foreach (JPair p in content)
            {
                o += p.ToString(leftReturn + 1, spaceChar);
                if (i < content.Count - 1)
                {
                    o += ","+n;
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

        #endregion
    }
}
