using System;
using System.Collections.Generic;
using System.IO;

namespace MCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            string command = args[0].ToLower();

            // Special command: history
            if (command == "history")
            {
                var calculator = new Calculator();
                calculator.PrintHistory();
                return;
            }

            // New command: graph
            if (command == "graph")
            {
                GraphPlotter.Plot(args);
                return;
            }

            // Other calculator operations
            var calc = new Calculator();
            try
            {
                double result = calc.Execute(command, args);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                PrintUsage();
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: mcalc <operation> [operands...]");
            Console.WriteLine();
            Console.WriteLine("Operations:");
            Console.WriteLine("  sum num1 num2           : Addition. Example: mcalc sum 3 4");
            Console.WriteLine("  sub num1 num2           : Subtraction. Example: mcalc sub 10 5");
            Console.WriteLine("  mul num1 num2           : Multiplication. Example: mcalc mul 6 7");
            Console.WriteLine("  div num1 num2           : Division. Example: mcalc div 10 2");
            Console.WriteLine("  fact num                : Factorial. Example: mcalc fact 5");
            Console.WriteLine("  sqr num                 : Square. Example: mcalc sqr 4");
            Console.WriteLine("  history                 : Show calculation history");
            Console.WriteLine();
            Console.WriteLine("  graph <function> ...    : Draw a graph in the terminal.");
            Console.WriteLine("     Supported functions: sin, cos, tan, sqr, exp, log");
            Console.WriteLine("     For polynomial graphs use:");
            Console.WriteLine("       poly <coefficients...> [--range start end]");
            Console.WriteLine("       (Coefficients must be in descending order of power.)");
            Console.WriteLine();
            Console.WriteLine("     Examples:");
            Console.WriteLine("       mcalc graph sin");
            Console.WriteLine("         => Plots sin(x) with a default range (-2π to 2π).");
            Console.WriteLine("       mcalc graph cos -6.28 6.28");
            Console.WriteLine("         => Plots cos(x) from -6.28 to 6.28.");
            Console.WriteLine("       mcalc graph poly 1 -3 2 --range -10 10");
            Console.WriteLine("         => Plots the polynomial x² - 3x + 2 from -10 to 10.");
        }
    }

    public class Calculator
    {
        private readonly List<string> _history;
        private readonly string _historyFilePath = "calc_history.txt";

        public Calculator()
        {
            _history = new List<string>();

            // Load existing history if available.
            if (File.Exists(_historyFilePath))
            {
                string[] lines = File.ReadAllLines(_historyFilePath);
                _history.AddRange(lines);
            }
        }

        public double Execute(string operation, string[] args)
        {
            double result = 0;
            switch (operation)
            {
                case "sum":
                    ValidateArgs(args, expectedCount: 3);
                    double a = ParseDouble(args[1]);
                    double b = ParseDouble(args[2]);
                    result = a + b;
                    break;

                case "sub":
                    ValidateArgs(args, expectedCount: 3);
                    a = ParseDouble(args[1]);
                    b = ParseDouble(args[2]);
                    result = a - b;
                    break;

                case "mul":
                    ValidateArgs(args, expectedCount: 3);
                    a = ParseDouble(args[1]);
                    b = ParseDouble(args[2]);
                    result = a * b;
                    break;

                case "div":
                    ValidateArgs(args, expectedCount: 3);
                    a = ParseDouble(args[1]);
                    b = ParseDouble(args[2]);
                    if (b == 0)
                    {
                        throw new Exception("Division by zero is not allowed.");
                    }
                    result = a / b;
                    break;

                case "fact":
                    ValidateArgs(args, expectedCount: 2);
                    int n = ParseInt(args[1]);
                    if (n < 0)
                    {
                        throw new Exception("Factorial is not defined for negative numbers.");
                    }
                    result = Factorial(n);
                    break;

                case "sqr":
                    ValidateArgs(args, expectedCount: 2);
                    a = ParseDouble(args[1]);
                    result = a * a;
                    break;

                default:
                    throw new Exception("Unknown operation: " + operation);
            }

            // Record and persist the calculation.
            string record = $"{DateTime.Now}: {operation} {string.Join(" ", args, 1, args.Length - 1)} = {result}";
            _history.Add(record);
            File.AppendAllText(_historyFilePath, record + Environment.NewLine);

            return result;
        }

        private void ValidateArgs(string[] args, int expectedCount)
        {
            if (args.Length != expectedCount)
            {
                throw new Exception("Invalid number of arguments.");
            }
        }

        private double ParseDouble(string input)
        {
            if (!double.TryParse(input, out double value))
            {
                throw new Exception("Invalid number: " + input);
            }
            return value;
        }

        private int ParseInt(string input)
        {
            if (!int.TryParse(input, out int value))
            {
                throw new Exception("Invalid integer: " + input);
            }
            return value;
        }

        private ulong Factorial(int n)
        {
            ulong result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= (ulong)i;
            }
            return result;
        }

        public void PrintHistory()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("No calculations performed yet.");
                return;
            }

            Console.WriteLine("Calculation History:");
            foreach (string record in _history)
            {
                Console.WriteLine(record);
            }
        }
    }

    public static class GraphPlotter
    {
        public static void Plot(string[] args)
        {
            // Expected usage: mcalc graph <function> [parameters...]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: mcalc graph <function> [parameters...]");
                return;
            }

            string funcName = args[1].ToLower();

            // Handle polynomial separately.
            if (funcName == "poly")
            {
                PlotPolynomial(args);
                return;
            }

            // Dictionary for built-in functions.
            var functions = new Dictionary<string, Func<double, double>>(StringComparer.OrdinalIgnoreCase)
            {
                {"sin", Math.Sin },
                {"cos", Math.Cos },
                {"tan", Math.Tan },
                {"sqr", x => x * x },
                {"exp", Math.Exp },
                {"log", Math.Log }  // Note: log(x) valid only for x>0.
            };

            if (!functions.ContainsKey(funcName))
            {
                Console.WriteLine("Unsupported function. Supported functions: " + string.Join(", ", functions.Keys) + ", poly");
                return;
            }
            Func<double, double> f = functions[funcName];

            // Default ranges: for sin, cos, tan use -2π to 2π; otherwise -10 to 10.
            double defaultStart = (funcName == "sin" || funcName == "cos" || funcName == "tan") ? -2 * Math.PI : -10;
            double defaultEnd = (funcName == "sin" || funcName == "cos" || funcName == "tan") ? 2 * Math.PI : 10;
            double startVal = defaultStart;
            double endVal = defaultEnd;
            if (args.Length >= 3)
            {
                if (!double.TryParse(args[2], out startVal))
                {
                    Console.WriteLine("Invalid start value: " + args[2]);
                    return;
                }
            }
            if (args.Length >= 4)
            {
                if (!double.TryParse(args[3], out endVal))
                {
                    Console.WriteLine("Invalid end value: " + args[3]);
                    return;
                }
            }

            Console.WriteLine($"Plotting {funcName} from {startVal} to {endVal}:");
            PlotGraph(f, startVal, endVal);
        }

        private static void PlotPolynomial(string[] args)
        {
            // Expected usage: mcalc graph poly <coeff1> <coeff2> ... [--range start end]
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: mcalc graph poly <coefficients...> [--range start end]");
                return;
            }

            List<double> coefficients = new List<double>();
            double start = -10;
            double end = 10;
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i].Equals("--range", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 2 >= args.Length)
                    {
                        Console.WriteLine("Usage: mcalc graph poly <coefficients...> [--range start end]");
                        return;
                    }
                    if (!double.TryParse(args[i + 1], out start) || !double.TryParse(args[i + 2], out end))
                    {
                        Console.WriteLine("Invalid range values.");
                        return;
                    }
                    i += 2; // Skip range values.
                }
                else
                {
                    if (!double.TryParse(args[i], out double coeff))
                    {
                        Console.WriteLine("Invalid coefficient: " + args[i]);
                        return;
                    }
                    coefficients.Add(coeff);
                }
            }

            if (coefficients.Count == 0)
            {
                Console.WriteLine("At least one coefficient is required for the polynomial.");
                return;
            }

            // Build the polynomial function using Horner's method.
            // Coefficients should be provided in descending order.
            Func<double, double> polyFunc = (x) =>
            {
                double result = coefficients[0];
                for (int i = 1; i < coefficients.Count; i++)
                {
                    result = result * x + coefficients[i];
                }
                return result;
            };

            Console.WriteLine($"Plotting polynomial with coefficients (descending order): {string.Join(", ", coefficients)} from {start} to {end}:");
            PlotGraph(polyFunc, start, end);
        }

        public static void PlotGraph(Func<double, double> f, double xStart, double xEnd, int width = 80, int height = 20)
        {
            // Create a grid for the graph.
            char[,] grid = new char[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    grid[i, j] = ' ';

            // Sample function values.
            double[] yValues = new double[width];
            double xStep = (xEnd - xStart) / (width - 1);
            for (int col = 0; col < width; col++)
            {
                double x = xStart + col * xStep;
                yValues[col] = f(x);
            }

            // Determine vertical scale.
            double minY = yValues[0], maxY = yValues[0];
            for (int i = 0; i < width; i++)
            {
                if (yValues[i] < minY) minY = yValues[i];
                if (yValues[i] > maxY) maxY = yValues[i];
            }
            if (minY == maxY)
            {
                minY -= 1;
                maxY += 1;
            }

            // Plot the function points.
            for (int col = 0; col < width; col++)
            {
                double normalized = (yValues[col] - minY) / (maxY - minY); // 0 to 1
                int row = (int)Math.Round((height - 1) * (1 - normalized)); // invert: 0 at top
                if (row >= 0 && row < height)
                    grid[row, col] = '*';
            }

            // Draw x-axis if y=0 lies in range.
            if (minY <= 0 && maxY >= 0)
            {
                double normZero = (0 - minY) / (maxY - minY);
                int row = (int)Math.Round((height - 1) * (1 - normZero));
                for (int col = 0; col < width; col++)
                {
                    if (grid[row, col] == ' ')
                        grid[row, col] = '-';
                }
            }

            // Draw y-axis if x=0 is within range.
            if (xStart <= 0 && xEnd >= 0)
            {
                int col = (int)Math.Round((0 - xStart) / (xEnd - xStart) * (width - 1));
                for (int row = 0; row < height; row++)
                {
                    if (grid[row, col] == ' ')
                        grid[row, col] = '|';
                }
            }

            // Print the grid.
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    Console.Write(grid[i, j]);
                Console.WriteLine();
            }
        }
    }
}
