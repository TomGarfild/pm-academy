using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task_3
{
    public class Notes
    {
        private readonly List<Note> _listOfNotes;

        public Notes() : this("notes.json")
        {
        }
        public Notes(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                _listOfNotes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            catch (Exception)
            {
                _listOfNotes = new List<Note>();
            }
        }
        public void FindNotes()
        {
            Console.WriteLine("Enter string-filter");
            var filter = Console.ReadLine()?.Trim();
            List<Note> result;
            if (string.IsNullOrEmpty(filter))
            {
                result = _listOfNotes.ToList();
            }
            else
            {
                result = _listOfNotes.Where(n =>
                        n.Id.ToString().Contains(filter)
                        || n.Title.Contains(filter)
                        || n.CreatedOn.ToString(CultureInfo.CurrentCulture).Contains(filter)
                        || n.Text.Contains(filter)).ToList();
            }

            if (result.Count == 0)
            {
                Console.WriteLine("Notes wasn't found");
            }
            else
            {
                result.ForEach(n => Console.WriteLine($"{n.Id} | {n.Title} | {n.CreatedOn}"));
            }
        }

        public Note BrowseNote()
        {
            Console.WriteLine("Enter id of note");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("Wrong input! Try again.");
            }

            var note = _listOfNotes.FirstOrDefault(n => n.Id == id);

            Console.WriteLine(default(Note) == note
                ? $"Note with id {id} wasn't found"
                : $"Id: {note.Id}\nTitle: {note.Title}\nCreatedOn: {note.CreatedOn}\n{note.Text}");

            return note;
        }

        public void CreateNote()
        {
            Console.WriteLine("Enter your note");

            var text = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Empty string won't be added");
                return;
            }
            var title = text?.Substring(0, Math.Min(text.Length, 32));
            var id = _listOfNotes.Count == 0 ? 1 : _listOfNotes.Last().Id + 1;
            _listOfNotes.Add(new Note(id, title, text, DateTime.UtcNow));

            var jsonSerialized = JsonConvert.SerializeObject(_listOfNotes);
            File.WriteAllText("notes.json", jsonSerialized);
        }

        public void RemoveNote()
        {
            var noteToRemove = BrowseNote();
            if (noteToRemove == null) return;
            Console.WriteLine("Do you want remove this note?");
            Console.WriteLine("Press y to confirm");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                _listOfNotes.Remove(noteToRemove);
                Console.WriteLine($"Note with id {noteToRemove.Id} was successfully removed");
                var jsonSerialized = JsonConvert.SerializeObject(_listOfNotes);
                File.WriteAllText("notes.json", jsonSerialized);
            }
        }
    }
}