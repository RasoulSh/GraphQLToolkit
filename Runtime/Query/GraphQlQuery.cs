using System;
using System.Collections.Generic;
using System.Linq;
using GraphQLToolkit.Argument;

namespace GraphQLToolkit.Query
{
    public class GraphQlQuery
    {
        private string query;
        private readonly GraphQlQuery parent;
        private List<GraphQlQuery> children;
        private readonly GraphQlArgument[] queryArguments;


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
                var argumentsStr = "";
                var argumentsCount = graphQlQuery.queryArguments?.Length ?? 0;
                for(int i = 0; i < argumentsCount; i++)
                {
                    argumentsStr += graphQlQuery.queryArguments[i].ToString();
                    if (i < argumentsCount - 1)
                        argumentsStr += ", ";
                }
                if (argumentsCount > 0)
                    argumentsStr = "(" + argumentsStr + ")";
                queryString += $"{graphQlQuery.query}{argumentsStr}";
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

        private GraphQlQuery(GraphQlQuery parent, string query, GraphQlArgument[] arguments) : this()
        {
            this.query = query;
            this.parent = parent;
            queryArguments = arguments;
        }

        public GraphQlQuery Add(string query, GraphQlArgument[] arguments = null)
        {
            var newChild = new GraphQlQuery(this, query, arguments);
            children.Add(newChild);
            return newChild;
        }

        public void Remove(GraphQlQuery graphQlQuery)
        {
            children.Remove(graphQlQuery);
        }
    }
}