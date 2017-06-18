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
        /// <summary>
        /// 暴击率的缩写CritRate
        /// </summary>
        private double _c;

        public Context()
        {
            _c = 0;
        }

        public double CritRate
        {
            get { return _c; }
            set { _c = value; }
        }
    }
}
