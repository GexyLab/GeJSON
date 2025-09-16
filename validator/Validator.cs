using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLab.GeJSON.validator
{
    public class Validator(JSchema schema)
    {
        string currentPos;

        public bool valid(JObject src) {

            int index;
            for (index = 0; index < src.GetItems().Count; index++)
            {
                // ****************************************************************************************************
                // Elaborazione dello schema
                // ****************************************************************************************************
                string title = schema.Get("title", "").Value;
                string description = schema.Get("descripion", "").Value;
                JType type;

                //type
                // ----------------------------------------------------------------
                try
                {
                    type = convertTypeString(schema.Get("type").Value);
                }
                catch (ArgumentNullException)
                {
                    throw new Exception("Schema error: Parameter \"Type\" is required for node " + src.GetItems().ElementAt(0).ToString());
                }

                //additionalProperties
                // ----------------------------------------------------------------
                bool additionalProperties=true;
                JType? additionalPropertiesType = null;

                if (schema.Get("additionalProperties", "").GetJsonType() == JType.Object)
                {
                    // è specificato il tipo di dato che devono avere le proprietà non esplicitamente dichiarate nello schema
                    additionalPropertiesType = convertTypeString(((JObject)schema.Get("additionalProperties").Value).Get("type").Value);
                }
                else
                {
                    additionalProperties = schema.Get("additionalProperties", true).Value;
                    Console.WriteLine(additionalProperties.ToString());
                }



                //properties
                // ----------------------------------------------------------------
                JObject sProperties = schema.Get("properties", new JObject()).Value;

                // ****************************************************************************************************
                // verifica oggetto di partenza
                // ****************************************************************************************************

                /* --------------------------------------------------------------------------
                 * verifica type
                 * ----------------------------------------------------------------------- */
                if (type == JType.Object && src is not JObject)
                {
                    ValidatorException ex = new ValidatorException("The type is wrong, expected object but it's " + type);
                    ex.jsonEntity = src;
                    throw ex;
                }

                /* --------------------------------------------------------------------------
                 * verifica additionalProperties
                 * ----------------------------------------------------------------------- */
                if (!additionalProperties)
                {
                    // se additionalProperties = false
                    if (src.Size() > sProperties.Size() )
                    {
                        // se additionalProperties = false && proprietà sorgente > proprietà schema
                        ValidatorException ex = new ValidatorException("Node not permit additional properties");
                        ex.jsonEntity = src;
                        throw ex;
                    }
                }else if(additionalPropertiesType != null) 
                {
                    // se additionalProperties != false && additionalPropertiesType != null, contiene il tipo che possono avere le proprietà aggiuntive
                    var ap = src.GetItems().Except(sProperties.GetItems());
                    foreach(JPair p in ap)
                    {
                        if(p.GetJsonType() != additionalPropertiesType)
                        {
                            ValidatorException ex = new ValidatorException("Type of additional properties is not correct. Found "+ p.GetJsonType()+ " but expected "+ additionalPropertiesType);
                            ex.jsonEntity = src;
                            throw ex;
                        }
                    }

                }

                /* --------------------------------------------------------------------------
                 * verifica minProperties e maxProperties
                 * ----------------------------------------------------------------------- */

                //currentSubject = src.getItems().ElementAt(index); // questo contiene i pair su cui fare la valutazione






            }

            return true;
        }

         

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

    }
}
