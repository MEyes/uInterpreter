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
        private DigitsDFAState currentState;
        private MathExpression _mathExpression;

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
            //是否当前字符串索引代表字符串的最后一个字符
            bool isEndOfString = false;
            //设置有穷自动机的初态
            currentState = DigitsDFAState.Init;
            //当前字符
            char currentChar;
            do
            {
                isEndOfString = _mathExpression.IsEndOfString;
                currentChar = _mathExpression.CurrentChar;

                switch (currentState)
                {
                    case DigitsDFAState.Init:
                        if (char.IsDigit(currentChar))
                        {
                            currentState = DigitsDFAState.Integer;
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        break;
                    case DigitsDFAState.Integer:
                        if (currentChar == '.')
                        {
                            currentState = DigitsDFAState.Float;//输入小数点，状态转移到qF
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(currentChar))//既不是数字也不是小数
                            {
                                currentState = DigitsDFAState.Quit;
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
                    case DigitsDFAState.Float:
                        if (!char.IsDigit(currentChar))//非数字，退出
                        {
                            currentState = DigitsDFAState.Quit;
                        }
                        else
                        {
                            if (!isEndOfString)
                            {
                                _mathExpression.CurrentIndex++;
                            }
                        }
                        break;
                    case DigitsDFAState.Quit:
                        break;

                }
            } while (currentState != DigitsDFAState.Quit && !isEndOfString);

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
