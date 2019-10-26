using StringCalculatorProject;
using System;
using Xunit;

namespace StringCalculatorTests
{
    public class StringCalculatorTests
    {
        private class StringCalculatorTestData : TheoryData<string, int>
        {
            public StringCalculatorTestData()
            {
                Add("1,5000", 5001);
                Add("4,-3", 1);
                Add(",", 0);
                Add("5,tytyt", 5);
                Add("5,", 5);
                Add("1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12", 78);
                Add(@"1\n2,3", 6);
                Add(@"5\n6\n7", 18);
            }
        }

        [Theory]
        [ClassData(typeof(StringCalculatorTestData))]
        public void TwoNumbers_Add_ReturnsSum(string s, int expected)
        {
            var stringCalculator = new StringCalculator();

            var actual = stringCalculator.Add(s);

            Assert.Equal(expected, actual);
        }
    }
}
