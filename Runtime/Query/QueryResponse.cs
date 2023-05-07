using System;
using GraphQLToolkit.Error;

namespace GraphQLToolkit.Query
{
    [Serializable]
    public record QueryResponse<T>
    {
        public T data;
        public GraphQlError[] errors;
    }
}