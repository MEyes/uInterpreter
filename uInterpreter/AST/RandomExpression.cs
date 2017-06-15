using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class RandomExpression:Expression
    {
        private int _minValue;
        private int _maxValue;
        public RandomExpression(int minValue,int maxValue)
        {
            this._minValue = minValue;
            _maxValue = maxValue;
        }
        public override double Evaluate()
        {
            Random random=new Random();
            return Convert.ToDouble(random.Next(_minValue, _maxValue));
        }
    }
}
