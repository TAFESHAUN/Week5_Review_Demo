#region NON MODULAR
//class Program
//{
//    static void Main()
//    {
//        List<string> books = new List<string>();
//        bool running = true;

//        while (running)
//        {
//            Console.Clear();
//            Console.WriteLine("=== Simple Library (all-in-Main) ===");
//            Console.WriteLine("1) Add Book");
//            Console.WriteLine("2) Remove Book");
//            Console.WriteLine("3) View Books");
//            Console.WriteLine("4) Exit");
//            Console.Write("Choose: ");
//            string choice = Console.ReadLine();

//            if (choice == "1")
//            {
//                Console.Write("Title to add: ");
//                string title = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(title))
//                {
//                    books.Add(title.Trim());
//                    Console.WriteLine("Added.");
//                }
//                else
//                {
//                    Console.WriteLine("Nothing added.");
//                }
//                Console.WriteLine("Press ENTER...");
//                Console.ReadLine();
//            }
//            else if (choice == "2")
//            {
//                Console.Write("Title to remove: ");
//                string title = Console.ReadLine();
//                if (books.Remove(title.Trim()))
//                    Console.WriteLine("Removed.");
//                else
//                    Console.WriteLine("Not found.");
//                Console.WriteLine("Press ENTER...");
//                Console.ReadLine();
//            }
//            else if (choice == "3")
//            {
//                Console.WriteLine("Current books:");
//                if (books.Count == 0) Console.WriteLine("(none)");
//                for (int i = 0; i < books.Count; i++)
//                {
//                    Console.WriteLine($" - {books[i]}");
//                }
//                Console.WriteLine("Press ENTER...");
//                Console.ReadLine();
//            }
//            else if (choice == "4")
//            {
//                running = false;
//            }
//            else
//            {
//                Console.WriteLine("Invalid choice. Press ENTER...");
//                Console.ReadLine();
//            }
//        }
//    }
//}

#endregion


#region MODULAR

class Program
{
    static List<string> books = new List<string>();

    static void Main()
    {
        bool running = true;
        while (running)
        {
            switch (ShowMenu())
            {
                case "1": AddBook(); break;
                case "2": RemoveBook(); break;
                case "3": ViewBooks(); break;
                case "4": running = false; break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    static string ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Simple Library (modular) ===");
        Console.WriteLine("1) Add Book");
        Console.WriteLine("2) Remove Book");
        Console.WriteLine("3) View Books");
        Console.WriteLine("4) Exit");
        Console.Write("Choose: ");
        return Console.ReadLine();
    }

    static void AddBook()
    {
        Console.Write("Title to add: ");
        string title = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(title))
        {
            books.Add(title.Trim());
            Console.WriteLine("Added.");
        }
        else Console.WriteLine("Nothing added.");
        Pause();
    }

    static void RemoveBook()
    {
        Console.Write("Title to remove: ");
        string title = Console.ReadLine();
        if (books.Remove(title.Trim())) Console.WriteLine("Removed.");
        else Console.WriteLine("Not found.");
        Pause();
    }

    static void ViewBooks()
    {
        Console.WriteLine("Current books:");
        if (books.Count == 0) Console.WriteLine("(none)");
        for (int i = 0; i < books.Count; i++)
            Console.WriteLine($"{i + 1}. {books[i]}");
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("Press ENTER...");
        Console.ReadLine();
    }
}

#endregion