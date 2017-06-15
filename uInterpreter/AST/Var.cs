using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class Var:Expression
    {
        private Context _context;
        public Var(Context context)
        {
            _context = context;
        }
        public override double Evaluate()
        {
            return _context.T;
        }
    }
}
