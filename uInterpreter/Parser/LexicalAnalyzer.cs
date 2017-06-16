using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    /// <summary>
    /// 词法分析器
    /// 可以处理由多个字符组成的构造，比如标识符。标识符由多个字符组成，但是在语法分析阶段被当作一个单元进行处理
    /// 这样的单元称作词法单元(Token)
    /// 例如表达式count+1中，标识符count被当作一个单元
    /// </summary>
    public class LexicalAnalyzer
    {
        private readonly string _expressionStr;
        private int _index;
        private readonly int _length;
        private double _number;

        public LexicalAnalyzer(string expressionStr)
        {
            _expressionStr = expressionStr;
            _index = 0;
            _length = _expressionStr.Length;
        }

        private char CurrentChar
        {
            get
            {
                return _expressionStr[_index];
            }
        }

        public Token GetToken()
        {
            var token = Token.Illegal;
            //跳过非法字符：空格和Tab
            while (_index<_length && (CurrentChar==' ' || CurrentChar == '\t'))
            {
                _index++;
            }
            if (_index==_length)
            {
                return Token.Null;
            }

            switch (CurrentChar)
            {
                case '+':
                    token = Token.Add;
                    _index++;
                    break;
                case '-':
                    token = Token.Sub;
                    _index++;
                    break;
                case '*':
                    token=Token.Mul;
                    _index++;
                    break;
                case '/':
                    token = Token.Div;
                    _index++;
                    break;
                case '(':
                    token = Token.OParen;
                    _index++;
                    break;
                case ')':
                    token = Token.CParen;
                    _index++;
                    break;
                case '$':
                    if (_expressionStr[_index+1]=='t')
                    {
                        _index += 2;
                        token = Token.Param;
                    }
                    else
                    {
                        _index++;
                        token=Token.Illegal;
                    }
                    break;
                default:
                    if (char.IsDigit(_expressionStr[_index]))
                    {
                        token = GrabDigitsFromStream();
                    }else if (char.IsLetter(_expressionStr[_index]))
                    {
                        token = GetSineCosineFromStream();
                    }
                    else
                    {
                        throw  new Exception("Illegal Token");
                    }
                    break;
            }
            return token;
        }

        public double GetNumber()
        {
            return _number;
        }

        private Token GetSineCosineFromStream()
        {
            var tem = Convert.ToString(_expressionStr[_index]);
            _index++;
            while (_index<_length && (char.IsLetter(_expressionStr[_index])))
            {
                tem += _expressionStr[_index];
                _index++;
            }
            //表示怀疑
            tem = tem.ToUpper();
            if (tem=="SIN")
            {
                return Token.Sin;

            }else if (tem=="COS")
            {
                return Token.Cos;
            }
            return Token.Illegal;
        }

        private Token GrabDigitsFromStream()
        {
            var str=GetStringFromStream();
            _number = Convert.ToDouble(str);
            return Token.Double;
        }

        private DFAState currentState;

        /// <summary>
        /// 通过有穷自动机的状态转换取数字
        /// </summary>
        public string GetStringFromStream()
        {

            //保存原先字符索引，只有在状态转换时才记录
            int oldCharIndex = _index;
            //是否当前字符串索引代表字符串的最后一个字符
            bool isEndOfString = false;
            //设置有穷自动机的初态
            currentState = DFAState.Init;
            //当前字符
            char currentChar;
            do
            {
                isEndOfString = _index == _length - 1;
                currentChar = _expressionStr[_index];
                switch (currentState)
                {
                    case DFAState.Init:
                        if (char.IsDigit(currentChar))
                        {
                            currentState = DFAState.Integer;
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        break;
                    case DFAState.Integer:
                        if (currentChar == '.')
                        {
                            currentState = DFAState.Float;//输入小数点，状态转移到qF
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        else
                        {
                            if (!char.IsDigit(currentChar))//既不是数字也不是小数
                            {
                                currentState = DFAState.Quit;
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
                    case DFAState.Float:
                        if (!char.IsDigit(currentChar))//非数字，退出
                        {
                            currentState = DFAState.Quit;
                        }
                        else
                        {
                            if (!isEndOfString)
                            {
                                _index++;
                            }
                        }
                        break;
                    case DFAState.Quit:
                        break;

                }
            } while (currentState != DFAState.Quit && !isEndOfString);

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
