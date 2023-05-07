using System.Collections.Generic;
using System.Linq;

namespace GraphQLToolkit.Query
{
    public class GraphQlQuery
    {
        private string query;
        private readonly GraphQlQuery parent;
        private List<GraphQlQuery> children;

        public override string ToString()
        {
            return $"{{\"query\": \"{ToQueryString()}\"}}";
        }

        public string ToQueryString()
        {
            return $"{{{GetQueryString(this)} }}";
        }

        private static string GetQueryString(GraphQlQuery graphQlQuery)
        {
            var queryString = "";
            if (graphQlQuery.parent != null)
            {
                queryString += graphQlQuery.query + " ";
            }
            if (graphQlQuery.children.Any())
            {
                if (graphQlQuery.parent != null)
                    queryString += "{";
                foreach (var childGraphQl in graphQlQuery.children)
                {
                    queryString += GetQueryString(childGraphQl);
                }
                if (graphQlQuery.parent != null) 
                    queryString += " }";
            }
            return queryString;
        }

        public GraphQlQuery()
        {
            children = new List<GraphQlQuery>();
        }

        private GraphQlQuery(GraphQlQuery parent, string query) : this()
        {
            this.query = query;
            this.parent = parent;
        }

        public GraphQlQuery Add(string query)
        {
            var newChild = new GraphQlQuery(this, query);
            children.Add(newChild);
            return newChild;
        }

        public void Remove(GraphQlQuery graphQlQuery)
        {
            children.Remove(graphQlQuery);
        }
    }
}