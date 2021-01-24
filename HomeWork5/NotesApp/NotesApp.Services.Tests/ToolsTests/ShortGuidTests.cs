using System;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests.ToolsTests
{
    public class ShortGuidTests
    {
        [Fact]
        public void ToShortId_Transform_Guid_To_ShortGuid()
        {
            Assert.IsType<string>(Guid.NewGuid().ToShortId());
        }
        [Fact]
        public void FromShortId_Transform_String_To_Guid()
        {
            Assert.IsType<Guid>(Guid.NewGuid().ToString().FromShortId());
        }
        [Fact]
        public void FromShortId_Transform_When_Add_Two_Equals()
        {
            Assert.IsType<Guid>((Guid.NewGuid().ToShortId()+"==").FromShortId());
        }
        [Fact]
        public void FromShortId_Should_Fail_If_Value_Invalid()
        {
            Assert.Throws<FormatException>(() => "".FromShortId());
            Assert.Throws<FormatException>(() => (Guid.NewGuid().ToString()+"r").FromShortId());
        }
        [Fact]
        public void FromShortId_Should_Return_Null_If_Value_Null()
        {
            Assert.Null(ShortGuid.FromShortId(null));
        }
    }
}