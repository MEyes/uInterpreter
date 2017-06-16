using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    /// <summary>
    /// 有穷自动机从字符串中获取数字
    /// </summary>
    public class DigitsDFA
    {

        /// <summary>
        /// 有穷自动机,这些自动机本质上是与状态转换图类似
        /// 有穷自动机是识别器，它们只能对每个可能的输入串简单地回答“是”或者“否"
        /// 有且只有一条离开状态，当为非数字时，即离开状态，
        /// </summary>
        private enum State
        {
            Init,
            Integer,
            Float,
            Quit
        }

        private State _currentState;
        private readonly MathExpression _mathExpression;

        public DigitsDFA(MathExpression mathExpression)
        {
            _mathExpression = mathExpression;
        }
        /// <summary>
        /// 通过有穷自动机的状态转换取数字
        /// </summary>
        public string Run()
        {
            //保存原先字符索引
            var oldCharIndex = _mathExpression.CurrentIndex;
            //当前字符
            char currentChar;
            //是否是最后一个字符
            bool isEndOfString;
            //设置有穷自动机为初始状态
            _currentState = State.Init;
            do
            {
                isEndOfString = _mathExpression.IsEndOfString;
                currentChar = _mathExpression.CurrentChar;

                switch (_currentState)
                {
                    case State.Init:
                        if (char.IsDigit(currentChar))
                        {
                            _currentState = State.Integer;
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        else
                        {
                           //Init状态非数字则退出
                           _currentState= State.Quit;
                        }
                        break;
                    case State.Integer:
                        if (currentChar == '.')
                        {
                            _currentState = State.Float;//输入小数点，状态转移到Float
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(currentChar))//既不是数字也不是小数
                            {
                                _currentState = State.Quit;
                            }
                            else
                            {
                                if (!isEndOfString)
                                {
                                    _mathExpression.CurrentIndex++;//读取下一个字符
                                }
                            }
                        }
                        break;
                    case State.Float:
                        if (!char.IsDigit(currentChar))//非数字，退出
                        {
                            _currentState = State.Quit;
                        }
                        else
                        {
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        break;
                    case State.Quit:
                        break;

                }
            } while (_currentState != State.Quit && !isEndOfString);

            //取出数字

            if (isEndOfString && char.IsDigit(currentChar))
            {
                return _mathExpression.Expression.Substring(oldCharIndex, _mathExpression.CurrentIndex - oldCharIndex + 1);
            }
            else
            {
                return _mathExpression.Expression.Substring(oldCharIndex, _mathExpression.CurrentIndex - oldCharIndex);
            }


        }
    }
}
