using System;
using uInterpreter.AST;
using uInterpreter.Enum;

namespace uInterpreter.Parser
{
    public class Parser:LexicalAnalyzer
    {
        private Token _currentToken;


        public Parser(string expressionStr) : base(expressionStr)
        {
        }

        public Expression CallExpr()
        {
            _currentToken = GetToken();
            return Expr();
        }

        private Expression Expr()
        {
            Token old;
            Expression expression = Term();

            while (_currentToken==Token.Add|| _currentToken==Token.Sub)
            {
                old = _currentToken;
                _currentToken = GetToken();
                Expression e1 = Expr();

                expression=new BinaryExpression(expression,e1,old==Token.Add?Operator.Plus:Operator.Minus);
            }
            return expression;
        }

        private Expression Term()
        {
            Token old;
            Expression expression = Factor();

            while (_currentToken==Token.Mul || _currentToken==Token.Div)
            {
                old = _currentToken;
                _currentToken = GetToken();


                Expression e1 = Term();
                expression=new BinaryExpression(expression,e1,old==Token.Mul?Operator.Mul:Operator.Div);
            }

            return expression;
        }

        //Sin(Expr)
        //Random(10,20)
        //Avg([1,3,4])
        //Sum([10,20,30,40,50])


        private Expression Factor()
        {
            Token token;
            Expression expression;
            if (_currentToken==Token.Double)
            {
                expression=new NumericConstant(GetNumber());
                _currentToken = GetToken();
            }
            else if (_currentToken == Token.Param)
            {
                expression=new Var(null);
                _currentToken = GetToken();
            }
            else if(_currentToken==Token.Sin || _currentToken==Token.Cos)
            {
                Token old = _currentToken;
                _currentToken = GetToken();
                if (_currentToken!=Token.OParen)
                {
                    throw new Exception("Illegal Token");
                }

                _currentToken = GetToken();
                expression = Expr();
                if (_currentToken!=Token.CParen)
                {
                    throw new Exception("Missing Closeing Parenthesis\n");
                }

                if (old==Token.Cos)
                {
                    expression=new CosExpression(expression);
                }
                else
                {
                    expression=new SinExpression(expression);
                }
                _currentToken = GetToken();
            }
            else if (_currentToken==Token.OParen)
            {
                _currentToken = GetToken();
                expression = Expr();
                if (_currentToken!=Token.CParen)
                {
                    throw new Exception("Missing Closing Parenthesis\n");
                }
                _currentToken = GetToken();
            }
            else if(_currentToken==Token.Add || _currentToken==Token.Sub)
            {
                var old = _currentToken;
                _currentToken = GetToken();
                expression = Factor();

                expression=new UnaryExpression(expression,old==Token.Add?Operator.Plus:Operator.Minus);

            }
            else
            {
                throw new Exception("error");
            }
            return expression;
        }

    }
}
