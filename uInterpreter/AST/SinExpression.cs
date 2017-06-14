using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class SinExpression:Expression
    {
        private Expression _expression;

        public SinExpression(Expression expression)
        {
            _expression = expression;
        }
        public override double Evaluate()
        {
            var value = _expression.Evaluate();
            return Math.Sin(value);
        }
    }
}
