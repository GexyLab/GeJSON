
using OpenLab.GeJSON.error;
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
        /// Return JPair that conain key and value of propertie, selected by key. If element not found fire the ElementNotFound exception
        /// </summary>
        /// <param name="key">String represent the key of property</param>
        /// <returns></returns>
        /// <exception cref="ElementNotFoundException">If object not contain property with the key which has been specified</exception>
        public JPair GetProperty(string key)
        {
            try
            {
                var p = Find(key);
                if(p != null)
                {
                    return p;
                }
                else
                {
                    throw new ElementNotFoundException(this,"Property with key " + key + " not found");
                }
            }
            catch 
            {
                throw;
            }
        }

        

        public JPair GetProperty(string key, JObject defaultValue) 
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key,defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, JArray defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, int defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, long defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, float defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, double defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }


        public JPair GetProperty(string key, decimal defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, bool defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public JPair GetProperty(string key, string defaultValue)
        {
            try
            {
                var p = Find(key);
                if (p != null)
                {
                    return p;
                }
                else
                {
                    return new JPair(key, defaultValue);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<JPair> GetProperties()
        {
            return content;
        }

        #endregion

        #region get values

        private JPair? Find(string key)
        {
            foreach (var item in content)
            {
                if(item.Key == key)
                {
                    return item;
                }
            }
            return null;
        }

        public dynamic? GetValue(string key)
        {
            try
            {
                var p = content.Find(r => r.Key == key);
                if (p != null)
                {
                    return p.Value;
                }
                else
                {
                    throw new ElementNotFoundException(this,"Element with key " + key + " not found");
                }
            }
            catch
            {
                throw;
            }
        }

        public dynamic? GetValue(string key, dynamic? defaultValue)
        {
            try
            {
                return GetValue(key);
            }
            catch (ElementNotFoundException)
            {
                return defaultValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region get alias

        public dynamic? Get(string key, dynamic? defaultvalue) => GetValue(key, defaultvalue);
      
        public dynamic? Get( string key ) => GetValue(key);

        #endregion

        #region set alias

        public JObject Set(string key, JObject? value) {
            try
            {
                var p = GetProperty(key);
                
                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }

        public JArray Set(string key, JArray? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public string Set(string key, string? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public byte Set(string key, byte? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public int Set(string key, int? value)
        {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public float Set(string key, float? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public double Set(string key, double? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public decimal Set(string key, decimal? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }

        public short Set(string key, short? value)
        {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public long Set(string key, long? value)
        {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }
        public bool Set(string key, bool? value) {
            try
            {
                var p = GetProperty(key);

                return p.Value = value;
            }
            catch
            {
                throw;
            }
        }

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
