using System;
using System.Reflection;

namespace GraphGLToolkit
{
    public static class GraphQlUtil
    {
        public static GraphQl ToGraphQl(Type t)
        {
            var graphQl = new GraphQl();
            var fields = t.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic || field.IsInitOnly)
                    continue;
                ToGraphQl(field, graphQl);
            }
            return graphQl;
        }

        private static void ToGraphQl(FieldInfo field, GraphQl parent)
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