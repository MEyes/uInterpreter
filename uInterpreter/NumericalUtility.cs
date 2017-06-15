using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter
{
    /// <summary>
    /// 提取操作数 有穷自动机算法从表达式中抽取一个数字
    /// </summary>
    public class NumericalUtility
    {
        public string Result { get; set; }
        private DFAState currentState;

        /// <summary>
        /// 通过有穷自动机的状态转换取数字
        /// </summary>
        public void Run()
        {
            //保存原先字符索引，只有在状态转换时才记录
            int oldCharIndex;
            //是否当前字符串索引代表字符串的最后一个字符
            bool isEndOfString=false;
            //设置有穷自动机的初态
            currentState=DFAState.q0;

            int _index=0;

            //当前字符
            char currentChar;
            do
            {
                //TODO：isEndOfString=index=length-1
                //TODO:currentChar=Expr[_index]
                currentChar = '1';
                switch (currentState)
                {
                    case DFAState.q0:
                        if (char.IsDigit(currentChar))
                        {
                            currentState = DFAState.qI;
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        break;
                    case DFAState.qI:
                        if (currentChar=='.')
                        {
                            currentState=DFAState.qF;//输入小数点，状态转移到qF
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(currentChar))//既不是数字也不是小数
                            {
                                currentState=DFAState.qQ;
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
                    case DFAState.qF:
                        if (!char.IsDigit(currentChar))//非数字，退出
                        {
                            currentState=DFAState.qQ;
                        }
                        else
                        {
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        break;
                    case DFAState.qQ:
                        break;
                      
                }
            } while (currentState!=DFAState.qQ && !isEndOfString);

            //取出数字
        }
    }

    public enum DFAState
    {
        q0,
        qI,
        qF,
        qQ
    }
}
