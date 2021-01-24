using System;
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
            var service = new NotesService(storage.Object, events.Object);
            Assert.Throws<ArgumentNullException>(() => service.AddNote(null, It.IsAny<int>()));
        }

        [Fact]
        public void AddNote_Should_Notify_If_Add()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            storage.Setup(s => s.AddNote(It.IsAny<Note>(), It.IsAny<int>()))
                .Callback<Note, int>((n,i) => events.Object.NotifyAdded(n,i));
        }
        [Fact]
        public void AddNote_Should_Not_Notify_If_Add()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var service = new NotesService(storage.Object, events.Object);
            var note = new Note();
            service.AddNote(note, It.IsAny<int>());
        }
        [Fact]
        public void DeleteNote_Should_Notify_If_Delete()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            //storage.Setup(s => s.DeleteNote(It.IsAny<Guid>())).Callback();
        }
        [Fact]
        public void DeleteNote_Should_Not_Notify_If_Not_Delete()
        {
            var storage = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var service = new NotesService(storage.Object, events.Object);
           
        }
    }
}
