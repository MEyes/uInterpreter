using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uInterpreter.AST;

namespace uInterpreter.Parser
{
    public class Parser:Lexer
    {
        private Token _currentToken;


        public Parser(string expressionStr) : base(expressionStr)
        {
        }

        public Expression CallExpr()
        {
            _currentToken = GeToken();
            return Expr();
        }

        private Expression Expr()
        {
            return null;
        }

        private Expression Term()
        {
            return null;
        }

        private Expression Factor()
        {
            return null;
        }



    }
}
