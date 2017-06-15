using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace uInterpreter.AST
{
    public class Context
    {
        private double _t;

        public Context()
        {
            _t = 0;
        }

        public double T
        {
            get { return _t; }
            set { _t = value; }
        }
    }
}
