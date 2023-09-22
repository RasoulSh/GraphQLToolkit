using System;
using Newtonsoft.Json;

namespace GraphQLToolkit.Argument
{
    public class GraphQlArgument
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public GraphQlArgument(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {GetValueString()}";
        }

        private string GetValueString()
        {
            var valueType = Value.GetType();
            if (valueType.BaseType == typeof(Array))
            {
                var arr = Value as object[];
                var arrValueStr = "[";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i > 0)
                        arrValueStr += ", ";
                    arrValueStr += GetSingleValueString(arr[i]);
                }
                arrValueStr += "]";
                return arrValueStr;
            }
            return GetSingleValueString(Value);
        }
        
        private static string GetSingleValueString(object value)
        {
            var valueType = value.GetType();
            if (valueType == typeof(string))
                return $"\\\"{value}\\\"";
            if (valueType == typeof(bool))
                return value.ToString().ToLower();
            return value.ToString();
        }
    }
}