using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uInterpreter.Parser
{
    /// <summary>
    /// 有穷自动机,这些自动机本质上是与状态转换图类似
    /// 有穷自动机是识别器，它们只能对每个可能的输入串简单地回答“是”或者“否"
    /// 有且只有一条离开状态，当为非数字时，即离开状态，
    /// </summary>
    public enum DigitsDFAState
    {
        Init,
        Integer,
        Float,
        Quit
    }
}
