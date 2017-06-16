using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    /// <summary>
    /// 数学表达式
    /// </summary>
    public class MathExpression
    {
        public MathExpression(string expression)
        {
            _expression = expression;
            _index = 0;
        }

        private readonly string _expression;
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Illegal Expression");
                }
            }
        }

        private int _index;
        public int CurrentIndex
        {
            get { return _index; }
            set
            {
                if (value<0)
                {
                    throw new Exception("");
                }
                if (value>=_expression.Length)
                {
                    throw new Exception("");
                }
                _index = value;
            }
        }

        public bool IsIndexOutOfRange
        {
            get { return _index >= _expression.Length; }
        }

        public bool IsEndOfString
        {
            get { return _index == _expression.Length-1; }
        }

        public char CurrentChar
        {
            get
            {
                if (CurrentIndex >= _expression.Length)
                {
                    throw new ArgumentOutOfRangeException("");
                }
                return _expression[CurrentIndex];
            }
        }

        public char GetSpecificCharByIndex(int index)
        {
            if (index>=_expression.Length)
            {
                throw new ArgumentOutOfRangeException("");
            }
            return _expression[index];
        }

    }
}
