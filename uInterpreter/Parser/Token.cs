using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    public enum Token
    {
        Illegal=-1,
        Plus,
        Sub,
        Mul,
        Div,
        OParen,
        CParen,
        Double,
        Param,
        Sin,
        Cos,
        Null
    }
}
