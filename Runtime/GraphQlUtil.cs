using System;
using System.Reflection;
using GraphQLToolkit.Query;

namespace GraphQLToolkit
{
    public static class GraphQlUtil
    {
        public static GraphQlQuery ToGraphQl(Type t)
        {
            var graphQl = new GraphQlQuery();
            var fields = t.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic || field.IsInitOnly)
                    continue;
                ToGraphQl(field, graphQl);
            }
            return graphQl;
        }

        private static void ToGraphQl(FieldInfo field, GraphQlQuery parent)
        {
            var fieldType = field.FieldType;
            FieldInfo[] childFields = Array.Empty<FieldInfo>();
            if (fieldType.BaseType == typeof(Array))
            {
                childFields = fieldType.GetElementType().GetFields();
            }
            else
            {
                childFields = fieldType.GetFields();
            }
            var fieldGraph = parent.Add(field.Name);
            foreach (var childField in childFields)
            {
                if (childField.IsStatic || childField.IsInitOnly)
                    continue;
                ToGraphQl(childField, fieldGraph);
            }
        }
    }
}