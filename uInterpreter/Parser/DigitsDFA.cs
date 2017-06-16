using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    public class DigitsDFA
    {
        private DigitsDFAState currentState;

        /// <summary>
        /// 通过有穷自动机的状态转换取数字
        /// </summary>
        public string Run()
        {
            //保存原先字符索引
            var oldCharIndex = _index;
            //是否当前字符串索引代表字符串的最后一个字符
            bool isEndOfString = false;
            //设置有穷自动机的初态
            currentState = DigitsDFAState.Init;
            //当前字符
            char currentChar;
            do
            {
                isEndOfString = _index == _length - 1;
                currentChar = _expressionStr[_index];
                switch (currentState)
                {
                    case DigitsDFAState.Init:
                        if (char.IsDigit(currentChar))
                        {
                            currentState = DigitsDFAState.Integer;
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        break;
                    case DigitsDFAState.Integer:
                        if (currentChar == '.')
                        {
                            currentState = DigitsDFAState.Float;//输入小数点，状态转移到qF
                            if (!isEndOfString)
                            {
                                _index++;
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
                                    _index++;//读取下一个字符
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
                                _index++;
                            }
                        }
                        break;
                    case DigitsDFAState.Quit:
                        break;

                }
            } while (currentState != DigitsDFAState.Quit && !isEndOfString);

            //取出数字

            if (_index == _length - 1 && char.IsDigit(currentChar))
            {
                return _expressionStr.Substring(oldCharIndex, _index - oldCharIndex + 1);
            }
            else
            {
                return _expressionStr.Substring(oldCharIndex, _index - oldCharIndex);
            }


        }
    }
}
