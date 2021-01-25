using System;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests.ToolsTests
{
    public class ShortGuidTests
    {
        [Fact]
        public void ToShortId_Should_Transform_Guid_To_ShortGuid()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(guid, guid.ToShortId().FromShortId());
        }
        [Fact]
        public void FromShortId_Should_Transform_ShortGuid_To_Guid()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(guid, guid.ToString().FromShortId());
        }
        [Fact]
        public void FromShortId_Should_Transform_When_Add_Two_Equals_Sign()
        {
            var guid = Guid.NewGuid();
            Assert.Equal(guid, (guid.ToShortId()+"==").FromShortId());
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