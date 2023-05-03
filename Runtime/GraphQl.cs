using System.Collections.Generic;
using System.Linq;

namespace GraphQLToolkit
{
    public class GraphQl
    {
        private string query;
        private readonly GraphQl parent;
        private List<GraphQl> children;

        public override string ToString()
        {
            return $"{{\"query\": \"{{{GetQueryString(this)} }}\"}}";
        }

        private static string GetQueryString(GraphQl InternalGraphQl)
        {
            var queryString = "";
            if (InternalGraphQl.parent != null)
            {
                queryString += InternalGraphQl.query + " ";
            }
            if (InternalGraphQl.children.Any())
            {
                if (InternalGraphQl.parent != null)
                    queryString += "{";
                foreach (var childGraphQl in InternalGraphQl.children)
                {
                    queryString += GetQueryString(childGraphQl);
                }
                if (InternalGraphQl.parent != null) 
                    queryString += " }";
            }
            return queryString;
        }

        public GraphQl()
        {
            children = new List<GraphQl>();
        }

        private GraphQl(GraphQl parent, string query) : this()
        {
            this.query = query;
            this.parent = parent;
        }

        public GraphQl Add(string query)
        {
            var newChild = new GraphQl(this, query);
            children.Add(newChild);
            return newChild;
        }

        public void Remove(GraphQl graphQl)
        {
            children.Remove(graphQl);
        }
    }
}