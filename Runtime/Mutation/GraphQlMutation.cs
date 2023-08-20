using GraphQLToolkit.Query;

namespace GraphQLToolkit.Mutation
{
    public class GraphQlMutation
    {
        private readonly string mutationName;
        private readonly string mutationMethodName;
        private readonly GraphQlMutationArgument[] mutationArguments;
        private GraphQlQuery responseQuery;

        public GraphQlMutation(string name, string methodName,
            GraphQlMutationArgument[] arguments, GraphQlQuery responseQuery)
        {
            mutationName = name;
            mutationMethodName = methodName;
            mutationArguments = arguments;
            this.responseQuery = responseQuery;
        }

        public override string ToString()
        {
            const string curlyBracketStart = "{";
            const string curlyBracketEnd = "}";
            const string doubleQuotation = "\"";
            const string queryStr = "\"query\"";
            var responseQueryStr = $"{responseQuery.ToQueryString()}";
            var argumentsStr = "";
            var argumentsCount = mutationArguments.Length;
            for(int i = 0; i < argumentsCount; i++)
            {
                argumentsStr += mutationArguments[i].ToString();
                if (i < argumentsCount - 1)
                    argumentsStr += ", ";
            }
            if (argumentsCount > 0)
                argumentsStr = "(" + argumentsStr + ")";
            var methodStr = $"{mutationMethodName}{argumentsStr}";
            var mutationStr = $"{doubleQuotation}mutation {mutationName} {curlyBracketStart} {methodStr} {responseQueryStr}{curlyBracketEnd}{doubleQuotation}";
            return $"{curlyBracketStart}{queryStr}: {mutationStr}{curlyBracketEnd}";
        }
    }
}