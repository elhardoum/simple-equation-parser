using System;
using System.Text.RegularExpressions;

namespace Assignment1
{
    public class EquationParser
    {
        // improvements: (below expressions should throw invalid input error
        // - prohibit characters other than ()+-*/.0-9
        // - translate .N (= 0.N)
        private string command;

        public EquationParser(string command)
        {
            this.command = command;
        }

        private void sanitizeCommand(ref string raw)
        {
            raw = Regex.Replace(raw, @"\s{1,}", "");
        }

        private string evaluateOperation(string raw, string op)
        {
            return Regex.Replace(raw, @"\[([^\]]+)\]" + Regex.Escape(op) + @"\[([^\]]+)\]", matches =>
            {
                double n1 = Double.Parse(matches.Groups[1].ToString())
                     , n2 = Double.Parse(matches.Groups[2].ToString());

                if ("/" == op && 0 == n2)
                    throw new Exception("cannot divide by zero.");

                switch (op)
                {
                    case "/":
                        return "[" + (n1 / n2).ToString() + "]";

                    case "*":
                        return "[" + (n1 * n2).ToString() + "]";

                    case "+":
                        return "[" + (n1 + n2).ToString() + "]";

                    case "-":
                        return "[" + (n1 - n2).ToString() + "]";

                    default:
                        throw new Exception("unsupported operation.");
                }
            });
        }

        private string evaluateGroup(string group)
        {
            // first, extract constants and their signs, and wrap in brackets for easier access later
            string result = Regex.Replace(group, @"((\+|\-|\*|\/|^)(?<s>\+|\-))?(?<n>\d+(\.\d+)?)", matches =>
            {
                string tpl = matches.Groups[0].ToString();
                string symbol = tpl.Substring(0, 1);

                if ("/*".Contains(symbol)) // handle *N, /N captures
                {
                    return String.Format("{0}[{1}]", symbol, tpl.Substring(1));
                }
                else if ("+-".Contains(symbol) && "+-".Contains(tpl.Substring(1, 1))) // handle --N, ++N, +-N, -+N captures
                {
                    return String.Format("{0}[{1}]", symbol, tpl.Substring(1));
                }
                else // clean capture, contains digit and optional sign
                {
                    return String.Format("[{0}]", tpl);
                }
            });

            while (result.Contains("/"))
                result = evaluateOperation(result, "/");

            while (result.Contains("*"))
                result = evaluateOperation(result, "*");

            while (result.Contains("]-["))
                result = evaluateOperation(result, "-");

            while (result.Contains("]+["))
                result = evaluateOperation(result, "+");

            // lastly, remove wrapping brackets
            result = Regex.Replace(result, @"((^\[)|(\]$))", "");

            if (result.Contains("["))
                throw new Exception("invalid input.");

            return result;
        }

        public string evaluate()
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new Exception("no command supplied.");
            }

            sanitizeCommand(ref command);

            while (command.Contains("("))
            {
                command = Regex.Replace(command, @"\([^\(\)]+\)", matches =>
                {
                    string subcommand = matches.Groups[0].ToString();
                    subcommand = Regex.Replace(subcommand, @"(^\(|\)$)", "");
                    return evaluateGroup(subcommand);
                });
            }

            return command = evaluateGroup(command);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EquationParser parser;
            string input;

            Console.Write("> ");

            while ((input = Console.ReadLine()) != "exit")
            {
                parser = new EquationParser(input);

                try
                {
                    Console.WriteLine("= {0}", parser.evaluate());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }

                Console.Write("> ");
            }
        }
    }
}
