using System;

namespace GraphGLToolkit
{
    [Serializable]
    public record GraphQlError
    {
        public string message;
        public GraphQlErrorLocation[] locations;
    }
}