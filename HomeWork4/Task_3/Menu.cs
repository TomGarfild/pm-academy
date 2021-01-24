using System;

namespace Task_3
{
    public class Menu
    {
        public void Start()
        {
            Console.WriteLine("Task 3");
            Console.WriteLine("Notes manager");
            Console.WriteLine("Author: Safroniuk Oleksii\n");
            var notes = new Notes();
            while (true)
            {
                var command = GetInputCommand(new string[]
                    {"Find notes", "Browse note by Id", "Create note", "Remove note", "Exit"});
                switch (command)
                {
                    case 1:
                        notes.FindNotes();
                        break;
                    case 2:
                        notes.BrowseNote();
                        break;
                    case 3:
                        notes.CreateNote();
                        break;
                    case 4:
                        notes.RemoveNote();
                        break;
                    case 5:
                        //Exit from application
                        return;
                }
            }
        }
        private static int GetInputCommand(string[] commands)
        {
            Console.WriteLine("   Menu");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {commands[i]}");
            }

            Console.WriteLine("Enter number of command in menu");
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("\nWrong command\n");
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