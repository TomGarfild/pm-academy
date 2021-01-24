using System;
using Newtonsoft.Json;
namespace Task_3
{
    public class Note : INote
    {
        [JsonProperty("id")]
        public int Id { get; }
        [JsonProperty("tittle")]
        public string Title { get; }
        [JsonProperty("text")]
        public string Text { get; }
        [JsonProperty("createdon")]
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