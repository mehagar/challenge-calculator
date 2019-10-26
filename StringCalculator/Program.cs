using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringCalculator = new StringCalculator();
            while (true)
            {
                Console.WriteLine("Enter a string to Add:");
                var input = Console.ReadLine();
                Console.WriteLine($"Sum: {stringCalculator.Add(input)}");
            }
        }
    }

    /// <summary>
    /// Performs mathematical operations on numbers within formatted strings.
    /// </summary>
    public class StringCalculator
    {
        /// <summary>
        /// Adds two numbers withing the formatted string.
        /// </summary>
        /// <param name="s">The string containing the numbers to add.</param>
        /// <returns>The sum of the two numbers in <paramref name="s"/>.</returns>
        public int Add(string s)
        {
            List<int> numbers = new List<int>();

            // parse the delimiter and read past the \n to get to the string to parse
            var match = Regex.Match(s, @"(?://(?<delimiter>.)\\n)(?<stringtoparse>.+$)");

            var delimiters = new List<string> { @",", @"\n" };

            var customerDelimiter = match.Groups["delimiter"].ToString();
            delimiters.Add(customerDelimiter);

            var actualString = match.Groups["stringtoparse"];

            var splitNums = s.Split(delimiters.ToArray(), StringSplitOptions.None);
            foreach (var num in splitNums)
            {
                bool success = Int32.TryParse(num, out var parsedNum);

                if (success && parsedNum <= 1000)
                {
                    numbers.Add(parsedNum);
                }
                else
                {
                    numbers.Add(0);
                }
            }

            // check for negative numbers
            var negativeNumbers = numbers.Where(number => number < 0);

            if (negativeNumbers.Any())
            {
                var negativeNumbersString = string.Join(',', negativeNumbers);
                throw new ArgumentOutOfRangeException($"Can not add negative numbers. Negative numbers in string are: {negativeNumbersString}");
            }

            return numbers.Sum();
        }
    }
}
