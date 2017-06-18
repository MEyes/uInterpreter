using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    public enum Token
    {
        Illegal=-1,   //非法
        Add,          //加
        Sub,          //减  
        Mul,          //乘
        Div,          //除
        OParen,       //左括号 open parenthesis
        CParen,       //右括号 closed parenthesis
        Double,       //数字
        Param,        //参数
        Sin,          //Sine
        Cos,          //Cos
        Random,       //Random
        Null          //结束
    }
}
