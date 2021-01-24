using System;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests.ToolsTests
{
    public class StringGeneratorTests
    {
        [Fact]
        public void GenerateNumbersString_Returns_Empty_String_If_Length_Equal_Zero()
        {
            Assert.Equal(string.Empty, StringGenerator.GenerateNumbersString(0, true));
            Assert.Equal(string.Empty, StringGenerator.GenerateNumbersString(0, false));
        }
        [Fact]
        public void GenerateNumbersString_Fail_If_Value_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-1, true));
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-1, false));
        }
        [Fact]
        public void GenerateNumbersString_Returns_String_Without_Leading_Zero()
        {
            Assert.NotEqual('0', StringGenerator.GenerateNumbersString(2, false)[0]);
        }

        [Fact]
        public void GenerateNumberString_Returns_String_With_Set_Length()
        {
            var length = 100;
            Assert.Equal(length, StringGenerator.GenerateNumbersString(length, false).Length);
            Assert.Equal(length, StringGenerator.GenerateNumbersString(length, true).Length);
            
        }
        [Fact]
        public void GenerateNumberString_Returns_String_That_Is_Number_With_Set_Length()
        {
            var length = 15;
            var number = StringGenerator.GenerateNumbersString(length, false);
            Assert.Equal(length, number.Length);
            Assert.True(long.TryParse(number, out _));

            var numberWithLeadingZeros = StringGenerator.GenerateNumbersString(length, true);
            Assert.Equal(length, numberWithLeadingZeros.Length);
            Assert.True(long.TryParse(numberWithLeadingZeros, out _));

        }
    }
}