using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionTreeCalculator
{
    class ExpressionTree
    {

        Node<string> root;
        Dictionary<string, double> variables = new Dictionary<string, double>();

        private ExpressionTree(Node<string> root)
        {
            this.root = root;
            PostOrder();
        }

        public static ExpressionTree CreateExpression(string equation)
        {
            Queue<string> eq = SplitEquation(equation);
            Queue<string> rpn = ShuntingYard(eq);


            return new ExpressionTree(ConstructExpression(rpn));
        }

        public double SolveEquation()
        {
            SetVariables();
            return Solve();
        }

        public double Solve()
        {
            var queue = PostOrder();
            var stack = new Stack<double>();

            while (queue.Count > 0)
            {
                if (!precedence.ContainsKey(queue.Peek()))
                {
                    double num = 0;
                    if (!double.TryParse(queue.Peek(), out num))
                    {
                        num = variables[queue.Peek()];
                    }
                    stack.Push(num);
                    queue.Dequeue();
                }
                else
                {
                    string operation = queue.Dequeue();

                    var num2 = stack.Pop();
                    var num1 = stack.Pop();

                    if (operation == "+")
                    {
                        stack.Push(num1 + num2);
                    }
                    else if (operation == "*")
                    {
                        stack.Push(num1 * num2);
                    }
                    else if (operation == "/")
                    {
                        stack.Push(num1 / num2);
                    }
                    else if (operation == "-")
                    {
                        stack.Push(num1 - num2);
                    }
                    if (operation == "^")
                    {
                        stack.Push(Math.Pow(num1, num2));
                    }
                }
            }

            return stack.Pop();

        }

        public void SetVariables()
        {
            foreach (var item in variables.ToArray())
            {
                Console.WriteLine($"Enter a value for: {item.Key}");
                double input = 0;
                while (!double.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Input was invalid");
                }
                variables[item.Key] = input;
            }
        }

        public Queue<string> PostOrder()
        {
            Queue<string> queue = new Queue<string>();
            PostOrderHelper(queue, root);
            return queue;
        }

        private void PostOrderHelper(Queue<string> queue, Node<string> current)
        {
            if (current == null)
            {
                return;
            }

            PostOrderHelper(queue, current.LeftChild);
            PostOrderHelper(queue, current.RightChild);
            queue.Enqueue(current.Value);
            if (!precedence.ContainsKey(current.Value) && !double.TryParse(current.Value, out double temp) && !variables.ContainsKey(current.Value))
            {
                variables.Add(current.Value, 0);
            }

        }

        private static Node<string> ConstructExpression(Queue<string> rpn)
        {
            Stack<Node<string>> stack = new Stack<Node<string>>();
            while (rpn.Count > 0)
            {
                if (precedence.ContainsKey(rpn.Peek()))
                {
                    Node<string> newNode = new Node<string>(rpn.Dequeue());
                    newNode.RightChild = stack.Pop();
                    newNode.LeftChild = stack.Pop();
                    stack.Push(newNode);
                }
                else
                {
                    stack.Push(new Node<string>(rpn.Dequeue()));
                }
            }

            return stack.Peek();
        }

        private static Queue<string> ShuntingYard(Queue<string> equation)
        {

            Stack<string> operatorStack = new Stack<string>();
            Queue<string> outputQueue = new Queue<string>();

            while (equation.Count > 0)
            {
                if (precedence.ContainsKey(equation.Peek()))
                {
                    /*
                     *        while there is an operator o2 other than the left parenthesis at the top
            of the operator stack, and (o2 has greater precedence than o1
            or they have the same precedence and o1 is left-associative)
        ):
            pop o2 from the operator stack into the output queue
        push o1 onto the operator stack

                     */
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(" && ((precedence[operatorStack.Peek()] > precedence[equation.Peek()]) || precedence[operatorStack.Peek()] == precedence[equation.Peek()] && equation.Peek() != "^"))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(equation.Dequeue());
                }
                else if (equation.Peek() == "(")
                {
                    operatorStack.Push(equation.Dequeue());
                }
                else if (equation.Peek() == ")")
                {
                    while (operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                        if (operatorStack.Count == 0)
                        {
                            throw new Exception("Mismatched Psarenthesis");
                        }

                    }
                    operatorStack.Pop();
                    equation.Dequeue();
                }
                else
                {
                    outputQueue.Enqueue(equation.Dequeue());
                }
            }
            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == "(")
                {
                    throw new Exception("Mismatched Parenthesis");
                }
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;

        }

        static Dictionary<string, int> precedence = new Dictionary<string, int>()
        {
            { "+",1 },
            { "-",1 },
            { "*",2 },
            { "/",2 },
            { "^",3 }

        };


        private static Queue<string> SplitEquation(string equation)
        {
            StringBuilder stringBuilder = new StringBuilder(equation);
            Queue<string> queue = new Queue<string>();

            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (precedence.ContainsKey(stringBuilder[i].ToString()) || stringBuilder[i] == '(' || stringBuilder[i] == ')')
                {
                    stringBuilder.Insert(i, ' ');
                    stringBuilder.Insert(i + 2, ' ');
                    i++;
                }
            }

            string[] arr = stringBuilder.ToString().Split(' ');

            foreach (var item in arr)
            {
                if (item.Length > 0)
                {
                    queue.Enqueue(item);
                }
            }

            return queue;
        }
    }
}
