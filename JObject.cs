
using OpenLab.GeJSON.parser;

namespace OpenLab.GeJSON
{
    public class JObject
    {
        #region Variables

        List<JPair> content = new List<JPair>();

        dynamic parentObject;

        #endregion

        #region Contructor

        public JObject() {}
        public JObject(string key, JObject content) {
            Add(key, content);
        }
        public JObject(string key, JArray content)
        {
            Add(key, content);
        }
        public JObject(string key, string content)
        {
            Add(key, content);
        }
        public JObject(string key, byte content)
        {
            Add(key, content);
        }
        public JObject(string key, short content)
        {
            Add(key, content);
        }
        public JObject(string key, int content)
        {
            Add(key, content);
        }
        public JObject(string key, long content)
        {
            Add(key, content);
        }
        public JObject(string key, float content)
        {
            Add(key, content);
        }
        public JObject(string key, double content)
        {
            Add(key, content);
        }
        public JObject(string key, decimal content)
        {
            Add(key, content);
        }
        public JObject(string key, bool content)
        {
            Add(key, content);
        }
        public JObject(JPair pair) {
            content.Add(pair);
        }
        public JObject(string rawJson)
        {
            rawJson = rawJson ?? throw new ArgumentNullException(nameof(rawJson));

            var _lexer = new Lexer(rawJson);
            var _parser = new Parser(_lexer);

            JObject j  = _parser.Parse() as JObject ?? new JObject();
            content = j.GetItems();
            if (j.Empty()) throw new Exception("Could not parse json string");
        }

        #endregion

        #region Get value

        /// <summary>
        /// Return JPair that conain key and value of propertie, selected by key
        /// </summary>
        /// <param name="key">String represent the key of property</param>
        /// <returns>Return the JPair that contain data or null if not exist</returns>
        /// <exception cref="ArgumentNullException">If object not contain property with the key which has been specified</exception>
        public JPair Get(string key)
        {
            try
            {
                return content.Find(r => r.Key == key);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public JPair Get(string key, JObject defaultValue) 
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, JArray defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, int defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, long defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, float defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, double defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }


        public JPair Get(string key, decimal defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, bool defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public JPair Get(string key, string defaultValue)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? new JPair(key, defaultValue);
            }
            catch (Exception)
            {
                return new JPair(key, defaultValue);
            }
        }

        public List<JPair> GetItems()
        {
            return content;
        }

        #endregion

        #region Manage & check

        public bool Contains(string key)
        {
            try
            {
                content.Find(r => r.Key == key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Size()
        {
            return content.Count;
        }

        public bool Empty()
        {
            if(content.Count == 0) { return true; } else { return false; }
        }

        

        #endregion

        #region Add item

        public JPair Add(string key, JObject value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            value.parentObject = this;
            return p;
        }
        public JPair Add(string key, JArray value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            value.parentObject = this;
            return p;
        }
        public JPair Add(string key, int value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }
        public JPair Add(string key, long value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }
        public JPair Add(string key, float value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }
        public JPair Add(string key, bool value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }
        public JPair Add(string key, string value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }

        public JPair Add(string key, double value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }

        public JPair Add(string key, decimal value)
        {
            JPair p = new JPair(key, value);
            content.Add(p);
            return p;
        }

        #endregion

        #region Remove item

        public void Remove(string key)
        {
            try
            {
                content.Remove(content.Find(r => r.Key == key));
            }
            catch (ArgumentNullException)
            {
                throw; 
            }
        }

        #endregion

        #region navigation

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
        #endregion

        #region conversions

        public string Minify()
        {
            string o = "{";
            foreach (JPair p in content)
            {
                o += p.Minify();
            }
            o += "}";

            return o;
        }

        public string ToString(bool deep=true, int leftReturn=0, string spaceChar = " ")
        {

            string s = makeLeftReturn(leftReturn, spaceChar);
            

            string n = Environment.NewLine;
            string o = s+"{"+n;
            int i = 0;
            foreach (JPair p in content)
            {
                if (deep)
                {
                    o += p.ToString(deep, leftReturn + 1, spaceChar);
                }
                else
                {
                    if(p.Value is JObject)
                    {
                        o += makeLeftReturn(leftReturn+1,spaceChar) + "\"" + p.Key+ "\" : " +p.GetJsonType();
                    }else if(p.Value is JArray)
                    {
                        o += makeLeftReturn(leftReturn + 1, spaceChar) + "\"" + p.Key + "\" : " + p.GetJsonType();
                    }
                    else
                    {
                        o += p.ToString(deep, leftReturn + 1, spaceChar);
                    }
                    
                }
                if (i < content.Count - 1)
                {
                    o += ",";
                }

                o += n;

                i++;
            }
            o += s+"}";

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

        #region Validation

        JSchema schema { get; set; }

        #endregion
    }
}
