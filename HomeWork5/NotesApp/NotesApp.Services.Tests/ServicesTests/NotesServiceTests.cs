using System;
using System.Data.SqlTypes;
using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using Xunit;

namespace NotesApp.Tests.ServicesTests
{
    public class NotesServiceTests
    {
        [Fact]
        public void AddNote_Should_Fail_If_Value_Invalid()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var note = (Note)default;
            var id = It.Is<int>(i => i > 0);
            storage.Setup(s => s.AddNote(note, id));
            events.Setup(e => e.NotifyAdded(note, id));
            var service = new NotesService(storage.Object, events.Object);
            Assert.Throws<ArgumentNullException>(() => service.AddNote(note, id));
        }

        [Fact]
        public void AddNote_Should_Notify_If_Add()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var note = new Note();
            var id = It.Is<int>(i => i > 0);

            storage.Setup(s => s.AddNote(note, id));
            events.Setup(e => e.NotifyAdded(note, id));

            var service = new NotesService(storage.Object, events.Object);
            service.AddNote(note, id);
            events.Verify(e => e.NotifyAdded(note, id), Times.Exactly(1));
        }
        [Fact]
        public void AddNote_Should_Not_Notify_If_Not_Add()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var note = new Note();
            var id = It.Is<int>(i => i > 0);

            storage.Setup(s => s.AddNote(note, id));
            events.Setup(e => e.NotifyAdded(note, id));

            var service = new NotesService(storage.Object, events.Object);
            events.Verify(e => e.NotifyAdded(note, id), Times.Exactly(0));
        }
        [Fact]
        public void DeleteNote_Should_Notify_If_Delete()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var noteId = new Guid();
            var id = It.Is<int>(i => i > 0);

            storage.Setup(s => s.DeleteNote(noteId)).Returns(true);
            events.Setup(e => e.NotifyDeleted(noteId, id));
            var service = new NotesService(storage.Object, events.Object);
            service.DeleteNote(noteId, id);
            events.Verify(e => e.NotifyDeleted(noteId, id), Times.Exactly(1));
        }
        [Fact]
        public void DeleteNote_Should_Not_Notify_If_Not_Delete()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var noteId = new Guid();
            var id = It.Is<int>(i => i > 0);

            storage.Setup(s => s.DeleteNote(noteId)).Returns(false);
            events.Setup(e => e.NotifyDeleted(noteId, id));
            var service = new NotesService(storage.Object, events.Object);
            events.Verify(e => e.NotifyDeleted(noteId, id), Times.Exactly(0));

        }
    }
}
