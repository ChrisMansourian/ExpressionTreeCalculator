using System;

namespace ExpressionTreeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpressionTree tree = ExpressionTree.CreateExpression("a +b-c*a/(c-b) + a * 2 +1 + 2^2^(1+1)");

            var result = tree.SolveEquation();
            Console.WriteLine();
            Console.WriteLine($"The result for the given equation and variables is: {result}");

        }
    }
}
