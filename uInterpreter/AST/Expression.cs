using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public abstract class Expression
    {
        public abstract double Evaluate(Context context);
    }
}
