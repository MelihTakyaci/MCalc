using System;

namespace MCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                PrintUsage();
                return;
            }
            
            string operation = args[0].ToLower();

            try
            {
                switch(operation)
                {
                    case "sum":
                        if(args.Length != 3)
                        {
                            PrintUsage();
                            return;
                        }
                        double a = Convert.ToDouble(args[1]);
                        double b = Convert.ToDouble(args[2]);
                        Console.WriteLine("Result: " + (a + b));
                        break;

                    case "sub":
                        if(args.Length != 3)
                        {
                            PrintUsage();
                            return;
                        }
                        a = Convert.ToDouble(args[1]);
                        b = Convert.ToDouble(args[2]);
                        Console.WriteLine("Result: " + (a - b));
                        break;

                    case "mul":
                        if(args.Length != 3)
                        {
                            PrintUsage();
                            return;
                        }
                        a = Convert.ToDouble(args[1]);
                        b = Convert.ToDouble(args[2]);
                        Console.WriteLine("Result: " + (a * b));
                        break;

                    case "div":
                        if(args.Length != 3)
                        {
                            PrintUsage();
                            return;
                        }
                        a = Convert.ToDouble(args[1]);
                        b = Convert.ToDouble(args[2]);
                        if(b == 0)
                        {
                            Console.WriteLine("Error: Division by zero.");
                            return;
                        }
                        Console.WriteLine("Result: " + (a / b));
                        break;

                    case "fact":
                        if(args.Length != 2)
                        {
                            PrintUsage();
                            return;
                        }
                        int n = Convert.ToInt32(args[1]);
                        if(n < 0)
                        {
                            Console.WriteLine("Error: Factorial is not defined for negative numbers.");
                            return;
                        }
                        Console.WriteLine("Result: " + Factorial(n));
                        break;

                    case "sqr":
                        if(args.Length != 2)
                        {
                            PrintUsage();
                            return;
                        }
                        a = Convert.ToDouble(args[1]);
                        Console.WriteLine("Result: " + (a * a));
                        break;

                    default:
                        Console.WriteLine("Unknown operation: " + operation);
                        PrintUsage();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static ulong Factorial(int n)
        {
            ulong result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= (ulong)i;
            }
            return result;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: mcalc <operation> [operands...]");
            Console.WriteLine("Operations:");
            Console.WriteLine("  sum num1 num2    : Addition");
            Console.WriteLine("  sub num1 num2    : Subtraction");
            Console.WriteLine("  mul num1 num2    : Multiplication");
            Console.WriteLine("  div num1 num2    : Division");
            Console.WriteLine("  fact num         : Factorial");
            Console.WriteLine("  sqr num          : Square");
        }
    }
}
