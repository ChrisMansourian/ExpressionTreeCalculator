using System;

namespace ExpressionTreeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExpressionTree tree = ExpressionTree.CreateExpression("a +b-c*a/(c-b) + a * 2 +1 + 2^2^(1+1)");

            //var result = tree.SolveEquation();
            //Console.WriteLine();
            //Console.WriteLine($"The result for the given equation and variables is: {result}");

            bool exit = false;
            while(!exit)
            {
                Console.WriteLine("Enter The Expression You Would Like To Compute:");
                try
                {
                    ExpressionTree tree = ExpressionTree.CreateExpression(Console.ReadLine());

                    bool solve = true;
                    while(solve)
                    {
                        Console.WriteLine("Solving the expression.");                        
                        var result = tree.SolveEquation();
                        Console.WriteLine();
                        Console.WriteLine($"The result for the given equation and variables is: {result}");

                        string exitSolve = "";
                        while (exitSolve != "1" && exitSolve != "2")
                        {
                            Console.WriteLine("Type 1 to solve the equation again or Type 2 to genter a new expression/exit");
                            exitSolve = Console.ReadLine();
                        }
                        if(exitSolve == "2")
                        {
                            solve = false;
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Error creating that expression.");
                }

                string restart = "";
                while (restart != "1" && restart != "2")
                {
                    Console.WriteLine("Type 1 to compute a new expression or Type 2 to exit.");
                    restart = Console.ReadLine();
                }
                if(restart == "2")
                {
                    exit = true; 
                }

            }

        }
    }
}
