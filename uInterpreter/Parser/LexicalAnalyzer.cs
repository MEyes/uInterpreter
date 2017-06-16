using System;

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
            get { return _expressionStr[_index]; }
        }
        /// <summary>
        /// 获取词法单元
        /// </summary>
        /// <returns></returns>
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
                    if (char.IsDigit(CurrentChar))
                    {
                        token = GrabDigitsFromStream();

                    }else if (char.IsLetter(CurrentChar))
                    {
                        token = GetSineCosineFromString();
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

        private Token GetSineCosineFromString()
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
            var str=Run();
            _number = Convert.ToDouble(str);
            return Token.Double;
        }



    }
}
