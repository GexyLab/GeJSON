using OpenLab.GeJSON.parser;
using OpenLab.GeJSON.validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON
{
    public class JPair
    {
        #region Variables

        public string? Key { get; }
        public dynamic? Value { get; set; }

        #endregion

        #region Contructor

        public JPair(string key)
        {
            this.Key = key;
            this.Value = null;
        }

        public JPair()
        {
            this.Key = null;
            this.Value = null;
        }

        public JPair(string? key, JObject value)
        {
            this.Key = key;
            this.Value = value;
        }

        public JPair(string? key, JArray value)
        {
            this.Key = key;
            this.Value = value;
        }

        public JPair(string? key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, int value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, long value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, float value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, bool value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, double value)
        {
            this.Key = key;
            this.Value = value;
        }
        public JPair(string? key, decimal value)
        {
            this.Key = key;
            this.Value = value;
        }

        #endregion

        #region Type

        public JType GetJsonType()
        {
            if (Value is JObject)
            {
                return JType.Object;
            }
            else if (Value is JArray)
            {
                return JType.Array;
            }
            else if (Value is string)
            {
                return JType.String;
            }
            else if (Value is byte)
            {
                return JType.Byte;
            }
            else if (Value is short)
            {
                return JType.Short;
            }
            else if (Value is int)
            {
                return JType.Integer;
            }
            else if (Value is long)
            {
                return JType.Long;
            }
            else if (Value is float)
            {
                return JType.Float;
            }
            else if (Value is double)
            {
                return JType.Double;
            }
            else if (Value is decimal)
            {
                return JType.Decimal;
            }
            else if (Value is bool)
            {
                return JType.Boolean;
            }
            else if (Value is object)
            {
                return JType.Class;
            }
            else
            {
                return JType.Unknown;

            }
        }

        #endregion

        #region manage

        public bool Empty()
        {
            if(Value != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Parser token

        public Token ParserToken { get; set; }

        #endregion

        #region Get
        public JPair getProperty(string key)
        {
            if(GetJsonType() == JType.Object)
            {
                if(Value == null)
                {
                    throw new Exception("The value for key " + Key + " is null, is not possible find inner object that have a property with key " + key);
                }

                try
                {
                    JPair p = ((JObject)Value).GetProperty(key);
                    if (p != null)
                    {
                        throw new Exception("The property with "+Key+" not have inner object that have a property with key "+key);
                    }
                    else
                    {
                        return p;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Trying to get inner object property, with key " + key + ", on a value(key: " + Key + ") that not is an json object");
            }
        }

        public JPair getItem(int index) 
        {
            if (GetJsonType() == JType.Array)
            {
                if (Value == null)
                {
                    throw new Exception("The value for key " + Key + " is null, is not possible find inner array item with index " + index);
                }

                try
                {
                    JPair p = ((JArray)Value).GetItem(index);
                    if (p != null)
                    {
                        throw new Exception("The property with " + Key + " not have inner array with item index " + index);
                    }
                    else
                    {
                        return p;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Trying to get inner array item, with index " + index + ", on a value(key: " + Key + ") that not is an json array");
            }
        }
        #endregion

        #region Conversion

        public string Minify()
        {
            string o = "";

            if (Key != null) { o += "\"" + Key + "\":"; }

            switch (GetJsonType())
            {
                case JType.Object:
                    o += ((JObject)Value).Minify();
                    break;
                case JType.Array:
                    o += ((JArray)Value).Minify();
                    break;
                case JType.String:
                    o += "\"" + (string)Value + "\"";
                    break;
                case JType.Byte:
                    o += (byte)Value;
                    break;
                case JType.Short:
                    o += (short)Value;
                    break;
                case JType.Integer:
                    o += (int)Value;
                    break;
                case JType.Long:
                    o += (long)Value;
                    break;
                case JType.Float:
                    o += (float)Value;
                    break;
                case JType.Double:
                    o += (double)Value;
                    break;
                case JType.Decimal:
                    o += (decimal)Value;
                    break;
                case JType.Boolean:
                    o += (bool)Value;
                    break;
                case JType.Class:
                    o += ((object)Value).GetType().ToString();
                    break;
                case JType.Unknown:
                    o += ((object)Value).ToString();
                    break;
            }
            return o;    
        }

        public string ToString(bool deep = true, int leftReturn = 0, string spaceChar = " ")
        {
            string s = makeLeftReturn(leftReturn, spaceChar);

            string n = Environment.NewLine;

            string o = "";
            if (Key != null) { o += s+"\"" + Key + "\": "; } else { o += s; }

            switch (GetJsonType())
            {
                case JType.Object:
                    o += n;
                    o += ((JObject)Value).ToString(deep,leftReturn + 1, spaceChar);
                    break;
                case JType.Array:
                    o += n;
                    o += ((JArray)Value).ToString(deep, leftReturn + 1, spaceChar);
                    break;
                case JType.String:
                    o += "\"" + (string)Value + "\"";
                    break;
                case JType.Byte:
                    o += (byte)Value;
                    break;
                case JType.Short:
                    o += (short)Value;
                    break;
                case JType.Integer:
                    o += (int)Value;
                    break;
                case JType.Long:
                    o += (long)Value;
                    break;
                case JType.Float:
                    o += (float)Value;
                    break;
                case JType.Double:
                    o += (double)Value;
                    break;
                case JType.Decimal:
                    o += (decimal)Value;
                    break;
                case JType.Boolean:
                    o += (bool)Value;
                    break;
                case JType.Class:
                    o += ((object)Value).GetType().ToString();
                    break;
                case JType.Unknown:
                    o += ((object)Value).ToString();
                    break;
            }

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