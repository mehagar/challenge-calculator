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
                Add("", 0);
                Add("1,5000", 1);
                Add("5000, 4000", 0);
                Add(",", 0);
                Add("5,tytyt", 5);
                Add("5,", 5);
                Add("1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12", 78);
                Add(@"1\n2,3", 6);
                Add(@"5\n6\n7", 18);
                Add(@"//#\n2#5", 7);
                Add(@"//,\n2,ff,100", 102);
                Add(@"//[***]\n11***22***33", 66);
                Add(@"//[*][!!][r9r]\n11r9r22*hh*33!!44", 110);
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
        public void NegativeNumbers_Add_Throws()
        {
            var stringCalculator = new StringCalculator();

            Assert.Throws<ArgumentOutOfRangeException>(() => stringCalculator.Add("4,-3"));
        }
    }
}
