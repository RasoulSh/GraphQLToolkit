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
            if (valueType == typeof(string))
                return $"\\\"{Value}\\\"";
            return Value.ToString();
        }
    }
}