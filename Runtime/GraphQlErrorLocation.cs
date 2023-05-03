using System;

namespace GraphQLToolkit
{
    [Serializable]
    public record GraphQlErrorLocation
    {
        public int line;
        public int column;
    }
}