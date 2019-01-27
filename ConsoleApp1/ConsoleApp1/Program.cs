using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputRPN= Console.ReadLine();
            Stack<string> stack = new Stack<string>();
            for(int i = 0; i < inputRPN.Length; i++)
            {
                if(HelperClass.IsEnglishLetter(inputRPN[i]))
                {
                    stack.Push(inputRPN[i].ToString());
                }
                else if(inputRPN[i].In('+','-','/'))
                {
                    string secondOperand = stack.Pop();
                    string firstOperand = stack.Pop();

                    string newExpression = firstOperand + inputRPN[i] + secondOperand;
                    stack.Push(newExpression);
                }
                else if(inputRPN[i].In('*'))
                {
                    string secondOperand = stack.Pop();
                    string firstOperand = stack.Pop();

                    //making the expression a-b => (a-b)
                    if (secondOperand.Length > 1)
                        secondOperand = "(" + secondOperand + ")";
                    if (firstOperand.Length > 1)
                        firstOperand = "(" + firstOperand + ")";

                    //making the expression (a-b)*c => ((a*b)*c)
                    string newExpression = "("+firstOperand + inputRPN[i] + secondOperand+")";
                    stack.Push(newExpression);
                }
            }

            if (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());

                //if leading brackets required
                //Console.WriteLine("(" + stack.Pop() + ")");
            }
            else
            {
                Console.WriteLine("Please try again with a valid expression.");
            }
            Console.ReadLine();
        }
    }
    static class Extensions
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            return items.Contains(item);
        }
    }
    static class HelperClass
    {
        public static bool IsEnglishLetter(char input)
        {
            return (input >= 'A' && input <= 'Z') || (input >= 'a' && input <= 'z');
        }
    }
}
