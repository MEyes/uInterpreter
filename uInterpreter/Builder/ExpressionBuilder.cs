using System;
using uInterpreter.AST;

namespace uInterpreter.Builder
{
    public class ExpressionBuilder:AbstractBuilder
    {
        private string _expressionStr;

        public ExpressionBuilder(string expressionStr)
        {
            _expressionStr = expressionStr;
        }

        public Expression GetExpression()
        {
            try
            {
                Parser.Parser parser=new Parser.Parser(_expressionStr);
                return parser.CallExpr();
            }
            catch (Exception)
            {
                
                throw new Exception("error");
            }
        }
    }
}
