using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using Newtonsoft.Json;

namespace Task_3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Task 3");
            Console.WriteLine("Notes manager");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            List<Note> notes;
            try
            {
                var json = File.ReadAllText("notes.json");
                notes = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            catch (Exception)
            {
                notes = new List<Note>();
            }
            while (true)
            {
                var command = GetInputCommand(new string[]
                    {"Find notes", "Browse note by Id", "Create note", "Remove note", "Exit"});
                switch (command)
                {
                    case 1:
                        FindNotes(notes);
                        break;
                    case 2:
                        BrowseNote(notes);
                        break;
                    case 3:
                        CreateNote(notes);
                        break;
                    case 4:
                        RemoveNote(notes);
                        break;
                    case 5:
                        //Exit from application
                        return;
                }
            }

        }

        private static void FindNotes(in List<Note> notes)
        {
            Console.WriteLine("Enter string-filter");
            var filter = Console.ReadLine()?.Trim();
            List<Note> result;
            if (String.IsNullOrEmpty(filter))
            {
                result = notes.ToList();
            }
            else
            {
                result = notes.Where(n =>
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

        private static Note BrowseNote(in List<Note> notes)
        {
            Console.WriteLine("Enter id of note");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("Wrong input! Try again.");
            }

            var note = notes.FirstOrDefault(n => n.Id == id);
            if (default(Note) == note)
            {
                Console.WriteLine($"Note with id {id} wasn't found");
            }
            else
            {
                Console.WriteLine($"Id: {note.Id}\nTitle: {note.Title}\nCreatedOn: {note.CreatedOn}\n{note.Text}");
            }

            return note;
        }

        private static void CreateNote(List<Note> notes)
        {
            Console.WriteLine("Enter your note");

            var text = Console.ReadLine()?.Trim();
            if (String.IsNullOrEmpty(text))
            {
                Console.WriteLine("Empty string won't be added");
                return;
            }
            var title = text?.Substring(0, Math.Min(text.Length, 32));
            var id = notes.Count == 0 ? 1 : notes.Last().Id + 1;
            notes.Add(new Note(id, title, text, DateTime.UtcNow));

            var jsonSerialized = JsonConvert.SerializeObject(notes);
            File.WriteAllText("notes.json", jsonSerialized);
        }

        private static void RemoveNote(List<Note> notes)
        {
            var noteToRemove = BrowseNote(notes);
            if (noteToRemove == null) return;
            Console.WriteLine("Do you want remove this note?");
            Console.WriteLine("Press y to confirm");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                notes.Remove(noteToRemove);
                var jsonSerialized = JsonConvert.SerializeObject(notes);
                File.WriteAllText("notes.json", jsonSerialized);
            }
        }

        private static int GetInputCommand(string[] commands)
        {
            Console.WriteLine("\n   Menu");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {commands[i]}");
            }

            Console.WriteLine("Enter number of command in menu");
            int command;
            while (!Int32.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("\nWrong command");
                Console.WriteLine("   Menu");
                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {commands[i]}");
                }

                Console.WriteLine("Enter number of command in menu");
            }

            return command;
        }
    }
}