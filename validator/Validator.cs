using OpenLab.GeJSON;
using OpenLab.GeJSON.error;
using OpenLab.GeJSON.validator;

namespace OpenLab.GeJSON.validator
{
    public class Validator
    {
        #region Variables

        bool additionalPropertiesEnabled = true;
        JType? additionalPropertiesType = null;
        List<JPair> additionalProperties = new List<JPair> ();

        #endregion

        #region Object

        public bool Validate(JObject src, JSchema schema) {
            // ****************************************************************************************************
            // Elaborazione dello schema
            // ****************************************************************************************************
            string title = schema.Get("title", "");
            string description = schema.Get("descripion", "");

            //type
            // ----------------------------------------------------------------
            try
            {
                JType type = convertTypeString(schema.Get("type"));
                if (type != JType.Object)
                {
                    ValidatorException ex = new ValidatorException("The type is wrong, expected object but it's " + type);
                    ex.jsonEntity = src;
                    throw ex;
                }
            }
            catch (ElementNotFoundException)
            {
                throw new Exception("Schema error: Parameter \"Type\" is required for node " + src.GetProperties().ElementAt(0).ToString(false));
            }

            //additionalProperties
            // ----------------------------------------------------------------
            if (schema.GetProperty("additionalProperties", "").GetJsonType() == JType.Object)
            {
                additionalPropertiesEnabled = true;

                JObject? additionalPropertiesDef = schema.Get("additionalProperties",null);

                if (additionalPropertiesDef != null)
                {
                    if (additionalPropertiesDef.Contains("type"))
                    {
                        // è specificato il tipo di dato che devono avere le proprietà non esplicitamente dichiarate nello schema
                        additionalPropertiesType = convertTypeString(additionalPropertiesDef.Get("type"));
                    }
                }


            }
            else
            {
                additionalPropertiesEnabled = schema.Get("additionalProperties", true);
            }

            // additional properties list
            // ----------------------------------------------------------------
            additionalProperties = src.GetProperties().Except(schema.GetProperties()).ToList();

            // minProperties e maxPropeties
            // ----------------------------------------------------------------
            int minPropeties = schema.Get("minProperties", 0);
            int maxPropeties = schema.Get("maxProperties", 0);

            // required
            // ----------------------------------------------------------------
            JArray requiredProperties = schema.Get("required", new JArray());
            


            // nullable
            // ----------------------------------------------------------------
            bool nullable = schema.Get("nullable", true);

            // default
            // ----------------------------------------------------------------
            JObject? defaultValue;
            if (schema.Contains("default"))
            {
                defaultValue = schema.Get("default");
            }

            // properties
            // ----------------------------------------------------------------
            JSchema schemaProperties = new JSchema(schema.Get("properties", new JObject()));

            // ****************************************************************************************************
            // verifica 
            // ****************************************************************************************************

            /* --------------------------------------------------------------------------
            * verifica additionalProperties
            * ------------------------------------------------------------------------ */
            if (!additionalPropertiesEnabled && additionalProperties.Count > 0)
            {
                // se additionalProperties = false && proprietà sorgente > proprietà schema
                ValidatorException ex = new ValidatorException("Node not permit additional properties");
                ex.jsonEntity = src;
                throw ex;
            }
            

            /* --------------------------------------------------------------------------
             * verifica minProperties e maxProperties
             * ----------------------------------------------------------------------- */
            if (minPropeties > 0 && src.Size() < minPropeties)
            {
                ValidatorException ex = new ValidatorException("The properties of this item are too few, expected " + minPropeties);
                ex.jsonEntity = src;
                throw ex;
            }

            if (maxPropeties > 0 && src.Size() > maxPropeties)
            {
                ValidatorException ex = new ValidatorException("The properties of this item are too many, expected " + maxPropeties);
                ex.jsonEntity = src;
                throw ex;
            }

            /* --------------------------------------------------------------------------
             * verifica required
             * ----------------------------------------------------------------------- */
            foreach (JPair requiredKey in requiredProperties.GetItems())
            {
                if (!src.Contains(requiredKey.Value))
                {
                    ValidatorException ex = new ValidatorException("The property with key " + requiredKey.Value + " is required, but is not present");
                    ex.jsonEntity = src;
                    throw ex;
                }
            }

            

            // ===========================================================================================================================================

            foreach (JPair p in src.GetProperties())
            {
                if(p.GetJsonType() == JType.Object)
                {
                    Validate(p.Value, schemaProperties);
                }
                else if (p.GetJsonType() == JType.Array)
                {
                    Validate(p.Value, schemaProperties);
                }
                else
                {
                    Validate(p, schemaProperties);
                }
            }

            /* --------------------------------------------------------------------------
             * verifica default
             * ----------------------------------------------------------------------- */
            /*List<JPair>defaultValues = schema.GetProperties().Except(src.GetProperties()).ToList();
            foreach(JPair p in defaultValues)
            {
                JPair dfl = p.Value.GetPropertyOrNull("default");
                if (dfl != null) {
                    src.Add(p.Key, dfl.Value);
                }
            }*/


            return true;
        }

        #endregion

        #region Array

        public bool Validate(JArray src, JSchema schema)
        {
            // ****************************************************************************************************
            // Elaborazione dello schema
            // ****************************************************************************************************
            string title = schema.Get("title", "");
            string description = schema.Get("descripion", "");

            //type
            // ----------------------------------------------------------------
            JType type = convertTypeString(schema.Get("type"));
            if (type != JType.Array)
            {
                ValidatorException ex = new ValidatorException("The type is wrong, expected array but it's " + type);
                ex.jsonEntity = src;
                throw ex;
            }
            
            // minProperties e maxPropeties
            // ----------------------------------------------------------------
            int minItems = schema.Get("minItems", 0);
            int maxItems = schema.Get("maxItems", 0);

            // properties
            // ----------------------------------------------------------------
            JObject schemaItems = schema.Get("items", new JObject());

            // ****************************************************************************************************
            // verifica 
            // ****************************************************************************************************

            /* --------------------------------------------------------------------------
             * verifica minProperties e maxProperties
             * ----------------------------------------------------------------------- */
            if (minItems > 0 && src.Size() < minItems)
            {
                ValidatorException ex = new ValidatorException("The items of this array are too few, expected " + minItems);
                ex.jsonEntity = src;
                throw ex;
            }

            if (maxItems > 0 && src.Size() > maxItems)
            {
                ValidatorException ex = new ValidatorException("The items of this array are too many, expected " + maxItems);
                ex.jsonEntity = src;
                throw ex;
            }

            // ===========================================================================================================================================

            foreach (JPair p in src.GetItems())
            {
                if (p.GetJsonType() == JType.Object)
                {
                    Validate(p.Value, schema.Get("properties"));
                }
                else if (p.GetJsonType() == JType.Array)
                {
                    Validate(p.Value, schema);
                }
                else
                {
                    Validate(p, schema);
                }
            }

            return true;
        }

        #endregion

        #region property

        public bool Validate(JPair src, JSchema schema)
        {
            // ****************************************************************************************************
            // Elaborazione dello schema
            // ****************************************************************************************************
            string title = schema.Get("title", "");
            string description = schema.Get("descripion", "");

            //type
            // ----------------------------------------------------------------
            // check if it's an additional property
            if (additionalProperties.Contains(src)) {
                if (additionalPropertiesType != null)
                {
                    if (src.GetJsonType() != additionalPropertiesType)
                    {
                        ValidatorException ex = new ValidatorException("Type of additional property "+ src.Key + " is not correct. Found " + src.GetJsonType() + " but expected " + additionalPropertiesType);
                        ex.jsonEntity = src;
                        throw ex;
                    }
                }

            }
            else
            {
                JType type = convertTypeString(schema.Get("type"));
                if (type != src.GetJsonType())
                {
                    ValidatorException ex = new ValidatorException("The type is wrong, expected " + type + " but it's " + src.GetJsonType());
                    ex.jsonEntity = src;
                    throw ex;
                }
            }
        
            return true;
        }

        #endregion

        #region utils

      

        JType convertTypeString(string typeStr)
        {
            switch (typeStr)
            {
                case "array":
                    return JType.Array;
                case "object":
                    return JType.Object;
                case "string":
                    return JType.String;
                case "byte":
                    return JType.Byte;
                case "number":
                    return JType.Float;
                case "integer":
                    return JType.Integer;
                case "long":
                    return JType.Long;
                case "float":
                    return JType.Float;
                case "double":
                    return JType.Double;
                case "decimal":
                    return JType.Decimal;
                case "boolean":
                    return JType.Boolean;
                case "class":
                    return JType.Class;
                default:
                    return JType.Unknown;
            }
        }

        #endregion
    }
}
