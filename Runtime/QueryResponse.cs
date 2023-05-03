using System;

namespace GraphGLToolkit
{
    [Serializable]
    public record QueryResponse<T>
    {
        public T data;
        public GraphQlError[] errors;
    }
}