# What is GeJSON

It is a library for json manipulation, parsing json structures from a string and validating json through a schema, (RFC 9535 - Schema 2020-12, https://www.rfc-editor.org/rfc/rfc9535.html ,https://www.learnjsonschema.com/2020-12/) 

**Warning: not all validation keyworks have been implemented**
# Keyworks
Below are the keyworks that have been implemented in the validation

## Objects
type, required, minProperties, maxProperties, additionalProperties, title, description properties

## Array
type, minItems, maxItems, items, title, and description

## Properties
type, title, and description
# Data types
The data types processed are:
JObject, JArray, string, bool, byte, int, short, double, float, decimal, and long
## Numbers
The json numeric type is converted to the most suitable type based on the value. Numbers in scientific notation can also be used. The same happens when the value is taken from a property.
# Usage examples
## Base
```
JObject o = new JObject(); // empty object
o = new JObject("key","value"); // string
o.Add ("key2", "value2");

Console.WriteLine(o.GetValue("key2","salveeee")); // I take the value of the "key2" property, if there is none I return "salveeee"

JArray a = new JArrat(); // empty array
a = new JArray("string"); // array with a string element
a.Add("string2"); // add a string
a.Add(12.22); // I add a floating point number
a.Add(1E-2); // I add a number in scientific notation

Console.WriteLine(a.GetValue(1,55)); // I take the value of element 2, if there is none I return 55
Console.WriteLine(a.GetValue(0,"ciao")); // I take the value of element 1, if there is none I return "ciao"
```
## Parsing and validate
```
string src=@{ \"key1\" : \"value1\" }
string schema=@{ \"type\" : \"string\" }
JObject o = new JObject(src)
o.Validate(schema);
```
