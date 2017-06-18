using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class NumericConstant:Expression
    {
        private double _value;

        public NumericConstant(double value)
        {
            _value = value;
        }
        public override double Evaluate(Context context)
        {
            return _value;
        }
    }
}
