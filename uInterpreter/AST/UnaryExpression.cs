using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uInterpreter.Enum;

namespace uInterpreter.AST
{
    public class UnaryExpression:Expression
    {
        private Expression _expression;
        private Operator _operator;

        public UnaryExpression(Expression expression,Operator op)
        {
            _expression = expression;
            _operator = op;
        }
        public override double Evaluate()
        {
            switch (_operator)
            {
                case Operator.Plus:
                    return _expression.Evaluate();
                case Operator.Minus:
                    return -_expression.Evaluate();
            }
            return Double.NaN;
        }
    }
}
