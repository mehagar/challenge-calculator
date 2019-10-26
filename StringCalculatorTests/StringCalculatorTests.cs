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

        [Fact]
        public void MoreThanTwoNumbers_Add_ThrowsException()
        {
            var stringCalculator = new StringCalculator();

            Assert.Throws<ArgumentException>(() => stringCalculator.Add("1,2,3"));
        }
    }
}
