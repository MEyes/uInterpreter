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
        public override double Evaluate()
        {
            switch (_operator)
            {
                case Operator.Plus:
                    return _leftExpression.Evaluate() + _rightExpression.Evaluate();
                case Operator.Minus:
                    return _leftExpression.Evaluate() - _rightExpression.Evaluate();
                case Operator.Mul:
                    return _leftExpression.Evaluate() * _rightExpression.Evaluate();
                case Operator.Div:
                    return _leftExpression.Evaluate() / _rightExpression.Evaluate();
            }
            return Double.NaN;
        }
    }
}
