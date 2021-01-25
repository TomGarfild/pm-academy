using System;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests.ToolsTests
{
    public class NumberGeneratorTests
    {
        [Fact]
        public void GeneratePositiveLong_Should_Fail_If_Value_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(19));
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(-1));
        }
        [Fact]
        public void GeneratePositiveLong_Should_Return_Number_With_Set_Length()
        {
            var length = 10;
            var number = NumberGenerator.GeneratePositiveLong(length);
            Assert.Equal(length, number.ToString().Length);
        }
    }
}