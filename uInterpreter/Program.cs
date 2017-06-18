using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uInterpreter.AST;
using uInterpreter.Builder;

namespace uInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder=new ExpressionBuilder("1*$c+(2+3)*4-5");
            var context = new Context {CritRate = 1.2f};

            var expression=builder.GetExpression();
            Console.WriteLine(expression.Evaluate(context));
            Console.ReadKey();
        }
    }
}
