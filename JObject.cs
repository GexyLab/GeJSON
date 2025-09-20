
using OpenLab.GeJSON.parser;
using OpenLab.GeJSON.validator;

namespace OpenLab.GeJSON
{
    public class JObject
    {
        #region Variables

        protected List<JPair> content = new List<JPair>();

        dynamic parentObject;

        #endregion

        #region Contructor

        public JObject() {}

        public JObject(JPair pair)
        {
            Add(pair.Key, pair.Value);
        }
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
     
        public JObject(string rawJson)
        {
            rawJson = rawJson ?? throw new ArgumentNullException(nameof(rawJson));

            var _parser = new Parser<JObject>(rawJson);

            JObject j  = _parser.Parse() as JObject ?? new JObject();
            content = j.GetProperties();
            if (j.Empty()) throw new Exception("Could not parse json string");
        }

        #endregion

        #region Get property

        /// <summary>
        /// Return JPair that conain key and value of propertie, selected by key
        /// </summary>
        /// <param name="key">String represent the key of property</param>
        /// <returns>Return the JPair that contain data or null if not exist</returns>
        /// <exception cref="ArgumentNullException">If object not contain property with the key which has been specified</exception>
        public JPair GetProperty(string key)
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

        /// <summary>
        /// Return the JPair that contain key and value of property requeste by it's key. If the key not exist return null
        /// </summary>
        /// <param name="key">The key string of the property</param>
        /// <returns>Return JPair that contain data or null</returns>
        public JPair? GetPropertyOrNull(string key)
        {
            try
            {
                return content.Find(r => r.Key == key) ?? null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public JPair GetProperty(string key, JObject defaultValue) 
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

        public JPair GetProperty(string key, JArray defaultValue)
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

        public JPair GetProperty(string key, int defaultValue)
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

        public JPair GetProperty(string key, long defaultValue)
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

        public JPair GetProperty(string key, float defaultValue)
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

        public JPair GetProperty(string key, double defaultValue)
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


        public JPair GetProperty(string key, decimal defaultValue)
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

        public JPair GetProperty(string key, bool defaultValue)
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

        public JPair GetProperty(string key, string defaultValue)
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

        public List<JPair> GetProperties()
        {
            return content;
        }

        #endregion

        #region get values

        public dynamic? GetValueOrNull(string key)
        {
            try
            {
                return content.Find(r => r.Key == key).Value ?? null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public dynamic? GetValue(string key)
        {
            try
            {
                return content.Find(r => r.Key == key).Value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public JObject GetValue(string key, JObject defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public JArray GetValue(string key, JArray defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public string GetValue(string key, string defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null) {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public byte GetValue(string key, byte defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public float GetValue(string key, float defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public double GetValue(string key, double defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public decimal GetValue(string key, decimal defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public bool GetValue(string key, bool defaultValue)
        {
            try
            {
                JPair p = content.FirstOrDefault(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        #region get alias

        public dynamic? GetOrNull(string key) => GetValueOrNull(key);
        public JObject Get(string key, JObject defaultvalue) => GetValue(key, defaultvalue);
        public JArray Get(string key, JArray defaultvalue) => GetValue(key, defaultvalue);
        public string Get(string key, string defaultvalue) => GetValue(key, defaultvalue);
        public byte Get(string key, byte defaultvalue) => GetValue(key, defaultvalue);
        public float Get(string key, float defaultvalue) => GetValue(key, defaultvalue);
        public double Get(string key, double defaultvalue) => GetValue(key, defaultvalue);
        public decimal Get(string key, decimal defaultvalue) => GetValue(key, defaultvalue);
        public bool Get(string key, bool defaultvalue) => GetValue(key, defaultvalue);

        public dynamic Get(string key ) => GetValue(key);

        #endregion

        #region set alias

        public JObject Set(string key, JObject value) {  return (GetProperty(key).Value = value); }
        public JArray Set(string key, JArray value) { return (GetProperty(key).Value = value); }
        public string Set(string key, string value) { return (GetProperty(key).Value = value); }
        public byte Set(string key, byte value) { return (GetProperty(key).Value = value); }
        public float Set(string key, float value) { return (GetProperty(key).Value = value); }
        public double Set(string key, double value) { return (GetProperty(key).Value = value); }
        public decimal Set(string key, decimal value) { return (GetProperty(key).Value = value); }
        public bool Set(string key, bool value) { return (GetProperty(key).Value = value); }

        #endregion

        #region Manage & check

        public bool Contains(string key)
        {
            if (content.Find(r => r.Key == key) == null)
            {
                return false;
            }
            else
            {
                return true;
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

        public string schemaTitle;
        public string schemaDescription;
        public bool Validate(JSchema schema)
        {
            Validator validator = new Validator();
            try
            {
                return validator.Validate(this, schema);
            }
            catch 
            {
                throw;
            }
        }

        #endregion
    }
}
