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
            for (index = 0; index < src.getItems().Count; index++)
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
                    throw new Exception("Schema error: Parameter \"Type\" is required for node " + src.getItems().ElementAt(0).ToString());
                }

                //additionalProperties
                // ----------------------------------------------------------------
                bool additionalProperties=true;
                JType additionalPropertiesType;

                if (schema.Get("additionalProperties", "").getType() == JType.Object)
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
                JObject properties = schema.Get("properties", new JObject()).Value;

                // ****************************************************************************************************
                // verifica oggetto di partenza
                // ****************************************************************************************************

                /* --------------------------------------------------------------------------
                 * verifica type
                 * ----------------------------------------------------------------------- */
                if (type == JType.Object && src is not JObject)
                {
                    throw new Exception("The type is wrong, expected object but it's "+type);
                }

                /* --------------------------------------------------------------------------
                 * verifica additionalProperties
                 * ----------------------------------------------------------------------- */
                if (src.Size()>properties.Size() && !additionalProperties)
                {
                    throw new Exception("Node not permit additional properties");
                }
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
