using System;
using System.Collections.Generic;
using System.Data;

namespace RPN
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Queue<char> output = new Queue<char>();
            Stack<char> signs = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    continue;
                }
                else if (Char.IsLetterOrDigit(input[i]))
                {
                    output.Enqueue(input[i]);
                }
                else
                {
                    if (signs.Count == 0)
                    {
                        signs.Push(input[i]);
                    }
                    else
                    {
                        int currentOpPr = CheckPrecedence(signs.Peek());
                        int nextOpPr = CheckPrecedence(input[i]);
                        if (currentOpPr < nextOpPr || input[i] == '(')
                        {
                            signs.Push(input[i]);
                        }
                        else if (currentOpPr > nextOpPr || currentOpPr == nextOpPr && nextOpPr == 1 || currentOpPr == nextOpPr && nextOpPr == 2)
                        {
                            output.Enqueue(signs.Pop());
                            i--;
                        }
                        if (input[i] == ')')
                        {
                            while (signs.Contains('('))
                            {
                                output.Enqueue(signs.Pop());
                            }
                        }
                    }
                }

            }

            foreach (char sign in signs)
            {
                output.Enqueue(sign);
            }

            foreach (char item in output)
            {
                if (item.ToString() != "(" && item.ToString() != ")")
                {
                    Console.Write(item.ToString() + ' ');
                }
            }

            string value = new DataTable().Compute(input, null).ToString();

            Console.WriteLine();
            Console.WriteLine(value);

        }
        static int CheckPrecedence(char opr)
        {
            switch (opr)
            {
                case '(':
                case ')':
                    return 0;
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    throw new Exception("Not valid operator!");
            }
        }
    }
}
