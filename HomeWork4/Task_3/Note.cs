using System;

namespace Task_3
{
    public class Note : INote
    {
        public int Id { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTime CreatedOn { get; }

        public Note(int id, string title, string text, DateTime createdOn)
        {
            Id = id;
            Title = title;
            Text = text;
            CreatedOn = createdOn;
        }
    }
}