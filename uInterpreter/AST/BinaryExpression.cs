using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uInterpreter.Enum;

namespace uInterpreter.AST
{
    public class BinaryExpression:Expression
    {
        private Expression _leftExpression;
        private Expression _rightExpression;
        private Operator _operator;

        public BinaryExpression(Expression leftExpression,Expression righExpression,Operator op)
        {
            _leftExpression = leftExpression;
            _rightExpression = righExpression;
            _operator = op;
        }
        public override double Evaluate(Context context)
        {
            switch (_operator)
            {
                case Operator.Plus:
                    return _leftExpression.Evaluate(context) + _rightExpression.Evaluate(context);
                case Operator.Minus:
                    return _leftExpression.Evaluate(context) - _rightExpression.Evaluate(context);
                case Operator.Mul:
                    return _leftExpression.Evaluate(context) * _rightExpression.Evaluate(context);
                case Operator.Div:
                    return _leftExpression.Evaluate(context) / _rightExpression.Evaluate(context);
            }
            return Double.NaN;
        }
    }
}
