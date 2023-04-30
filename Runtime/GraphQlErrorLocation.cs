using System;

namespace GraphGLToolkit
{
    [Serializable]
    public record GraphQlErrorLocation
    {
        public int line;
        public int column;
    }
}