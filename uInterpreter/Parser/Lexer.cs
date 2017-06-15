using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    public class Lexer
    {
        private string _expressionStr;
        private int _index;
        private int _length;
        private double _number;

        public Lexer(string expressionStr)
        {
            _expressionStr = expressionStr;
            _index = 0;
            _length = _expressionStr.Length;
        }

        public Token GeToken()
        {
            var token = Token.Illegal;
            //跳过空格
            while (_index<_length && (_expressionStr[_index]==' ' || _expressionStr[_index]=='\t'))
            {
                _index++;
            }
            if (_index==_length)
            {
                return Token.Null;
            }

            switch (_expressionStr[_index])
            {
                case '+':
                    token = Token.Plus;
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

        private Token GrabDigitsFromStream()
        {
            string str = "";
            while (_index<_length && (char.IsDigit(_expressionStr[_index])))
            {
                str += _expressionStr[_index++];
            }

            if ((_index<_length) && (_expressionStr[_index]=='.'))
            {
                str += ".";
                _index++;
                while (_index<_length && char.IsDigit(_expressionStr[_index]))
                {
                    str += _expressionStr[_index++];
                }
            }
            _number = Convert.ToDouble(str);
            return Token.Double;
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

    }
}
