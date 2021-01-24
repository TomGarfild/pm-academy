using System;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Services;
using Moq;
using NotesApp.Services.Models;
using Xunit;

namespace NotesApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddNote_Should_Fail_If_Value_Invalid()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var service = new NotesService(storage.Object, events.Object);
            Assert.Throws<ArgumentNullException>(() => service.AddNote(null, 0));
        }

        [Fact]
        public void AddNote_Should()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var note = new Note();
            storage.Object.AddNote(note, 1);
            storage.Verify(s => s.AddNote(note, 1), Times.AtMostOnce);
            events.Verify(e => e.NotifyAdded(note, 1), Times.AtMostOnce);
        }
        [Fact]
        public void AddNote_Should_Not()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var note = new Note();
            storage.Object.AddNote(note, 1);
            storage.Verify(s => s.AddNote(note, 1), Times.Never);
            events.Verify(e => e.NotifyAdded(note, 1), Times.Never);
        }

    }
}
