using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uInterpreter.Builder;

namespace uInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder=new ExpressionBuilder("123.345+2*3-3");
            var expression=builder.GetExpression();
            Console.WriteLine(expression.Evaluate());
            Console.ReadKey();
        }
    }
}
