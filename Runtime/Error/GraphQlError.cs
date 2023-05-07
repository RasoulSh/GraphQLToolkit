using System;

namespace GraphQLToolkit.Error
{
    [Serializable]
    public record GraphQlError
    {
        public string message;
        public GraphQlErrorLocation[] locations;
    }
}