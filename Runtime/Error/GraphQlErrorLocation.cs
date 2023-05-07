using System;

namespace GraphQLToolkit.Error
{
    [Serializable]
    public record GraphQlErrorLocation
    {
        public int line;
        public int column;
    }
}