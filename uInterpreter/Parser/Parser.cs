using System;
using uInterpreter.AST;
using uInterpreter.Enum;

namespace uInterpreter.Parser
{
    /// <summary>
    /// 表达式文法的推广
    /// 我们可以将因子(factor)理解成不能被任何运算符分开的表达式。“不能分开”的意思是说当我们在任意因子的任意
    /// 一边放置一个运算符，都不会导致这个因子的任何部分分离出来，成为这个运算符的运算分量。
    /// 当然，因子本身作为一个整体可以成为该运算符的一个运算分量。如果这个因子是由一个括号括起来的表达式，那么这个括号将
    /// 起到保护其不被分开的作用
    /// 
    /// 一个（不是因子）项（term）是一个可能被高优先级的运算符*和/分开，但不能被低优先级运算符分开的表达式。
    /// 一个（不是因子也不是项）的表达式可能被任何一个运算符分开。
    /// 
    /// 使用这个文法时，一个表达式就是一个由+或-分割开来的项（term）列表，而项是由*或者/分隔的因子（factor）列表。
    /// 请注意，任何由括号括起来的表达式都是一个因子
    /// </summary>
    public class Parser
    {
        private Token _currentToken;

        private readonly LexicalAnalyzer _lexicalAnalyzer;

        public Parser(string expression)
        {
            _lexicalAnalyzer=new LexicalAnalyzer(new MathExpression(expression));

        }

        public Expression CallExpr()
        {
            _currentToken = _lexicalAnalyzer.GetToken();
            return Expr();
        }

        private Expression Expr()
        {
            Token old;
            Expression expression = Term();

            while (_currentToken==Token.Add|| _currentToken==Token.Sub)
            {
                old = _currentToken;
                _currentToken = _lexicalAnalyzer.GetToken();
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
                _currentToken = _lexicalAnalyzer.GetToken();


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
                expression=new NumericConstant(_lexicalAnalyzer.GetDigits());
                _currentToken = _lexicalAnalyzer.GetToken();
            }
            else if (_currentToken == Token.Param)
            {
                expression=new Var();
                _currentToken = _lexicalAnalyzer.GetToken();
            }
            else if(_currentToken==Token.Sin || _currentToken==Token.Cos)
            {
                Token old = _currentToken;
                _currentToken = _lexicalAnalyzer.GetToken();
                if (_currentToken!=Token.OParen)
                {
                    throw new Exception("Illegal Token");
                }

                _currentToken = _lexicalAnalyzer.GetToken();
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
                _currentToken = _lexicalAnalyzer.GetToken();
            }
            else if (_currentToken==Token.OParen)
            {
                _currentToken = _lexicalAnalyzer.GetToken();
                expression = Expr();
                if (_currentToken!=Token.CParen)
                {
                    throw new Exception("Missing Closing Parenthesis\n");
                }
                _currentToken = _lexicalAnalyzer.GetToken();
            }
            else if(_currentToken==Token.Add || _currentToken==Token.Sub)
            {
                var old = _currentToken;
                _currentToken = _lexicalAnalyzer.GetToken();
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
