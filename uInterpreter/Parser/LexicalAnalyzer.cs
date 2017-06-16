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

        private readonly MathExpression _mathExpression;
        private DigitsDFA _digitsDFA;
        private double _digits;
        public LexicalAnalyzer(MathExpression mathExpression)
        {
            _mathExpression = mathExpression;
        }

        /// <summary>
        /// 获取词法单元
        /// </summary>
        /// <returns></returns>
        public Token GetToken()
        {
            var token = Token.Illegal;
            //跳过非法字符：空格和Tab
            while (!_mathExpression.IsIndexOutOfRange && (_mathExpression.CurrentChar==' ' || _mathExpression.CurrentChar == '\t'))
            {
                _mathExpression.CurrentIndex++;
            }
            if (_mathExpression.IsIndexOutOfRange)
            {
                return Token.Null;
            }

            switch (_mathExpression.CurrentChar)
            {
                case '+':
                    token = Token.Add;
                    _mathExpression.CurrentIndex++;
                    break;
                case '-':
                    token = Token.Sub;
                    _mathExpression.CurrentIndex++;
                    break;
                case '*':
                    token=Token.Mul;
                    _mathExpression.CurrentIndex++;
                    break;
                case '/':
                    token = Token.Div;
                    _mathExpression.CurrentIndex++;
                    break;
                case '(':
                    token = Token.OParen;
                    _mathExpression.CurrentIndex++;
                    break;
                case ')':
                    token = Token.CParen;
                    _mathExpression.CurrentIndex++;
                    break;
                case '$':
                    if (_mathExpression.GetSpecificCharByIndex(_mathExpression.CurrentIndex + 1) =='t')
                    {
                        _mathExpression.CurrentIndex += 2;
                        token = Token.Param;
                    }
                    else
                    {
                        _mathExpression.CurrentIndex++;
                        token=Token.Illegal;
                    }
                    break;
                default:
                    if (char.IsDigit(_mathExpression.CurrentChar))
                    {
                        token = GetDigitsFromString();

                    }else if (char.IsLetter(_mathExpression.CurrentChar))
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

        public double GetDigits()
        {
            return _digits;
        }

        private Token GetSineCosineFromString()
        {
//            var tem = Convert.ToString(_expressionStr[_index]);
//            _index++;
//            while (_index<_length && (char.IsLetter(_expressionStr[_index])))
//            {
//                tem += _expressionStr[_index];
//                _index++;
//            }
//            //表示怀疑
//            tem = tem.ToUpper();
//            if (tem=="SIN")
//            {
//                return Token.Sin;
//
//            }else if (tem=="COS")
//            {
//                return Token.Cos;
//            }
            return Token.Illegal;

        }

        private Token GetDigitsFromString()
        {
            _digitsDFA = new DigitsDFA(_mathExpression);
            var str = _digitsDFA.Run();
            _digits = Convert.ToDouble(str);
            return Token.Double;
        }
    }
}
