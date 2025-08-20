using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WorkingWithCSVExample
{
    public class Student //Record OR Row in the flat file DB that is student.csv
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }

    class Program
    {
        const string CsvPath = "student.csv";
        static List<Student> students = new();

        static void Main()
        {
            // Load once at startup (do NOT clear the list again later).
            LoadStudents();

            bool running = true;
            while (running)
            {
                switch (ShowMenu())
                {
                    case 1: PrintStudents(); break;
                    case 2: AddStudent(); break;
                    case 3: SaveStudents(); break;
                    case 4: running = false; break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static int ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Student CSV App ===");
            Console.WriteLine("1) Print students");
            Console.WriteLine("2) Add student");
            Console.WriteLine("3) Save students");
            Console.WriteLine("4) Exit");
            Console.Write("Choice: ");
            return int.TryParse(Console.ReadLine(), out int c) ? c : -1;
        }

        static void LoadStudents()
        {
            if (!File.Exists(CsvPath))
            {
                Console.WriteLine($"No '{CsvPath}' found. Create it with a header:");
                Console.WriteLine("Id,Name,DateAdded");
                return;
            }

            // Read all lines and skip header
            var lines = File.ReadAllLines(CsvPath).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                if (parts.Length < 3) continue;

                // Be tolerant of spaces and format
                if (!int.TryParse(parts[0].Trim(), out int id)) continue;
                string name = parts[1].Trim();

                if (!DateTime.TryParseExact(
                        parts[2].Trim(),
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime added))
                {
                    continue;
                }

                // Add to list once at startup. Do NOT clear later.
                students.Add(new Student { Id = id, Name = name, DateAdded = added });
            }
        }

        static void PrintStudents()
        {
            Console.WriteLine();
            Console.WriteLine("Students:");
            if (students.Count == 0)
            {
                Console.WriteLine("(none)");
            }
            else
            {
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.Id} - {s.Name} (added {s.DateAdded:yyyy-MM-dd HH:mm:ss})");
                }
            }
            Pause();
        }

        static void AddStudent()
        {
            Console.Write("Enter name: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                Pause();
                return;
            }

            int nextId = students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;

            students.Add(new Student
            {
                Id = nextId,
                Name = name,
                DateAdded = DateTime.Now
            });

            Console.WriteLine("Student added.");
            Pause();
        }

        static void SaveStudents()
        {
            using var sw = new StreamWriter(CsvPath, false);
            sw.WriteLine("Id,Name,DateAdded"); // header
            foreach (var s in students)
            {
                sw.WriteLine($"{s.Id},{s.Name},{s.DateAdded:yyyy-MM-dd HH:mm:ss}");
            }

            Console.WriteLine("Students saved.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
        }
    }
}
