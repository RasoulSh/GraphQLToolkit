using System;

namespace GraphQLToolkit
{
    [Serializable]
    public record QueryResponse<T>
    {
        public T data;
        public GraphQlError[] errors;
    }
}