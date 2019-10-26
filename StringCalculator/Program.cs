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
            // This regex separates the potential format prefix (containing customer delimiters) from the string to be parsed.
            var match = Regex.Match(s, @"(?<formatprefix>//(?:(?:\[(?<delimiter>[^\]]+)\])+|(?<delimiter>.))\\n)?(?<stringtoparse>.+$)");

            var delimiters = GetCustomDelimiters(match);

            var actualString = match.Groups["stringtoparse"].ToString();

            var numbersToAdd = GetNumbersToAdd(actualString, delimiters);

            var negativeNumbers = numbersToAdd.Where(number => number < 0);
            if (negativeNumbers.Any())
            {
                var negativeNumbersString = string.Join(',', negativeNumbers);
                throw new ArgumentOutOfRangeException($"Can not add negative numbers. Negative numbers in string are: {negativeNumbersString}");
            }

            return numbersToAdd.Sum();
        }

        private static List<int> GetNumbersToAdd(string actualString, List<string> delimiters)
        {
            var splitNums = actualString.Split(delimiters.ToArray(), StringSplitOptions.None);
            List<int> numbersToAdd = new List<int>();
            foreach (var num in splitNums)
            {
                bool success = Int32.TryParse(num, out var parsedNum);

                if (success && parsedNum <= 1000)
                {
                    numbersToAdd.Add(parsedNum);
                }
                else
                {
                    numbersToAdd.Add(0);
                }
            }

            return numbersToAdd;
        }

        private static List<string> GetCustomDelimiters(Match match)
        {
            var formatPrefix = match.Groups["formatprefix"].ToString();

            // If there are multiple custom delimiters, this line will find all of them.
            var delimiterMatches = Regex.Matches(formatPrefix, @"\[(?<delimiter>[^\]]+)\]");

            var delimiters = new List<string> {@",", @"\n"};

            if (delimiterMatches.Count > 1)
            {
                // Multiple delimiters were specified, so add each of them in turn.
                foreach (var delimiterMatch in delimiterMatches)
                {
                    var delimiterSubMatch = Regex.Match(delimiterMatch.ToString(), @"\[(?<delimiter>[^\]]+)\]");
                    delimiters.Add(delimiterSubMatch.Groups["delimiter"].ToString());
                }
            }
            else if (match.Groups["delimiter"].Success)
            {
                // a single delimiter was specified, so add it.
                var customerDelimiter = match.Groups["delimiter"].ToString();
                delimiters.Add(customerDelimiter);
            }

            return delimiters;
        }
    }
}
