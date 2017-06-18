using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class Var:Expression
    {
        public override double Evaluate(Context context)
        {
            //TODO:扩展其他的变量
            //比如ATK,DEF...
            return context.CritRate;
        }
    }
}
