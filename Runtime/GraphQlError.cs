using System;

namespace GraphQLToolkit
{
    [Serializable]
    public record GraphQlError
    {
        public string message;
        public GraphQlErrorLocation[] locations;
    }
}